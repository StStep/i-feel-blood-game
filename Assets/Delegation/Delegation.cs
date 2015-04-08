using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Manager;

/// <summary>
/// A Delegation is the main logic unit for the game. 
/// </summary>
public class Delegation : MonoBehaviour 
{

	public string name;

	protected bool _deployed;
	public bool Deployed
	{
		get
		{
			return _deployed;
		}
	}

	protected Node _location;
	public Node Location
	{
		get
		{
			return _location;
		}

		set
		{
			_location = value;
		}
	}
	
	protected Royal _delRoyal;
	protected MorphTracker _retinue;
	protected Queue<Order> _orders;
	protected bool currentlySelected;
	protected InputInterpreter inputInterp;
	protected Order currentOrder;

	public Royal DelegationRoyal
	{
		get
		{
			return _delRoyal;
		}
	}

	public int RetinueSize
	{
		get
		{
			return _retinue.GetSize();
		}
	}

	public int OrderCount
	{
		get
		{
			return _orders.Count;
		}
	}

	// Use this for initialization
	protected void Awake () 
	{
		_delRoyal = null;
		_orders = new Queue<Order>();
		_location = null;
		currentlySelected = false;
		inputInterp = InterfaceManager.GetInputInterpreter();
		_deployed = false;
		currentOrder = null;

		_retinue = GetComponentInChildren<MorphTracker>();
		if(_retinue == null) 
		{
			Debug.LogError("Error in Delegation object structure");
			Destroy(this);
		}
	}
	
	// Update is called once per frame
	protected void Update () 
	{
	
	}

	#region UI Called Functions

	public void Selected()
	{
		_delRoyal.Selected();
		currentlySelected = true;

		_retinue.SelectAll();
	}

	public void Unselected()
	{
		currentlySelected = false;
		_delRoyal.Unselected();

		_retinue.UnselectAll();
	}

	public void Deploy(Node location)
	{
		_location = location;
		_deployed = true;
		_delRoyal.Deploy();
	}

	#endregion

	#region Dispatcher Called Functions

	//Thie functions are called by the disapther
	public void AcceptSubject(Subject subject) 
	{
		if(subject == null)
		{
			Debug.LogError("Delegation: Given null subject");
			return;
		}

		// Set subject transform parent, put in tracker, and tell it who to follow
		subject.transform.parent = _retinue.transform;
		subject.DelParent = this;
		_retinue.AddFormicant(subject, subject.MorphNum);
		subject.JoinDelegation();

	}

	public void AcceptRoyal(Royal royal) 
	{
		if(royal == null)
		{
			Debug.LogError("Delegation: Given null royal");
			return;
		}

		// Set royal transform parent, and starting informing it
		_delRoyal = royal;
		_delRoyal.transform.parent = transform;
		_delRoyal.DelParent = this;

	}

	public Subject GetSubject(SubjectMorph morph)
	{
		Subject subject = (Subject) _retinue.GetFormicant((int) morph);

		if(subject == null)
		{
			return null;
		}

		subject.transform.parent = null;
		subject.DelParent = null;

		return subject;
	}

	public void AcceptOrder(Order order)
	{
		if(order == null)
		{
			Debug.LogError("Delegation: Given null order");
			return;
		}

		if(currentOrder == null)
		{
			currentOrder = order;
			order.StartOrder(this);
		}
		else
		{
			_orders.Enqueue(order);
			currentOrder.UpdateStatus();
		}
	}

	public void CancelOrder()
	{
		if(currentOrder != null) {
			currentOrder.CancelOrder();
			currentOrder = null;
			_orders.Clear();
		}
	}

	public void NextOrder()
	{
		if(currentOrder != null || _orders.Count == 0)
		{
			return;
		}
		currentOrder = _orders.Dequeue();
		currentOrder.StartOrder(this);
	}

	public void StartOrderRoutine(IEnumerator orderRoutine) 
	{
		StartCoroutine(orderRoutine);
	}
	
	public void StopOrderRoutine(IEnumerator orderRoutine)
	{
		StopCoroutine(orderRoutine);
	}

	#endregion

	#region Coroutine Actions

	#endregion

	#region Order Called Functions

	
	public IEnumerator OrdFollowPath(Path path, string orderName, int orderID) 
	{
		if(_delRoyal == null)
		{
			Debug.Log("WARNING - Delegation: Royal order given without royal");
			currentOrder.CancelOrder();
			currentOrder =  null;
			NextOrder();
			yield break;
		}
		if(Deployed)
		{
			Debug.Log("WARNING - Delegation: Ordered to move when deployed");
			currentOrder.CancelOrder();
			currentOrder =  null;
			NextOrder();
			yield break;
		}

		_location = null;
		Node stepNode = null;
		float nodeIndex = 0;
		foreach(Node node in path)
		{
			// Move to node
			stepNode = node;
			yield return StartCoroutine(_delRoyal.OrdMoveToPosition(stepNode.Location, orderName));
			currentOrder.Progress = nodeIndex / ((float) path.Count);
			currentOrder.UpdateStatus();
			nodeIndex++;
		}
		_location = stepNode;
		
		//Called Order Finished when done
		currentOrder.FinishOrder();
		currentOrder =  null;
		NextOrder();
		
	}

	public IEnumerator OrdIncubateForm(string orderName, int orderID)
	{
		if(_delRoyal == null)
		{
			Debug.Log("WARNING - Delegation: Royal order given without royal");
			currentOrder.CancelOrder();
			currentOrder =  null;
			NextOrder();
			yield break;
		}
		if(_delRoyal.morph != RoyalMorph.Queen)
		{
			Debug.Log("WARNING - Delegation: Queen order given to a non-queen delegation");
			currentOrder.CancelOrder();
			currentOrder =  null;
			NextOrder();
			yield break;
		}
		
		Queen queenCast = (Queen) _delRoyal;

		// Wait for eggs
		while (queenCast.TakeEggs(1) == 0)
		{
			yield return new WaitForSeconds(.5f);
		}

		// At this point, the helpers need to take care of junk, check prefab for details, such as build time and general requirements
		yield return new WaitForSeconds(.5f); // TODO TEMP TIME
		currentOrder.Progress = 0.25F;
		currentOrder.UpdateStatus();

		yield return new WaitForSeconds(.5f);
		currentOrder.Progress = 0.50F;
		currentOrder.UpdateStatus();

		yield return new WaitForSeconds(.5f);
		currentOrder.Progress = 0.75F;
		currentOrder.UpdateStatus();

		yield return new WaitForSeconds(.5f);
		currentOrder.Progress = 1.00F;
		currentOrder.UpdateStatus();

		//Called Order Finished when done
		currentOrder.FinishOrder();
		currentOrder =  null;
		NextOrder();
	}

	public IEnumerator OrdDeploy(Node location, string orderName, int orderID) 
	{
		if(_delRoyal == null)
		{
			Debug.Log("WARNING - Delegation: Royal order given without royal");
			currentOrder.CancelOrder();
			currentOrder =  null;
			NextOrder();
			yield break;
		}
		if(Deployed)
		{
			Debug.Log("WARNING - Delegation: Ordered to deploy when already deployed");
			currentOrder.CancelOrder();
			currentOrder =  null;
			NextOrder();
			yield break;
		}

		//Do deplyment junk
		yield return new WaitForSeconds(.5f); // TODO TEMP TIME
		currentOrder.Progress = 0.25F;
		currentOrder.UpdateStatus();
		
		yield return new WaitForSeconds(.5f);
		currentOrder.Progress = 0.50F;
		currentOrder.UpdateStatus();
		
		yield return new WaitForSeconds(.5f);
		currentOrder.Progress = 0.75F;
		currentOrder.UpdateStatus();
		
		yield return new WaitForSeconds(.5f);
		currentOrder.Progress = 1.00F;
		currentOrder.UpdateStatus();

		Deploy(location);
		
		//Called Order Finished when done
		currentOrder.FinishOrder();
		currentOrder =  null;
		NextOrder();
		
	}

	#endregion
}
