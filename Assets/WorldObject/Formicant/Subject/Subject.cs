using UnityEngine;
using System.Collections;
using Manager;

public class Subject : Formicant {

	public SubjectMorph _morph;

	public override int MorphNum {
		get {
			return (int) _morph;
		}
	}

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}

	public void JoinDelegation() 
	{
		if(_delegation == null || _delegation.DelegationRoyal == null)
		{
			Debug.LogError("Subject: No royal set before starting Follow Royal State");
			return;
		}
		SwitchState(StFollowRoyal, "Following royal");
	}

	#region Coroutine States
	protected virtual IEnumerator StFollowRoyal() {
		yield return StartCoroutine(ActWander( _delegation.DelegationRoyal.Position, _delegation.DelegationRoyal.controlRange));
	}
	#endregion


	#region Order Called Functions

	public IEnumerator OrdTranferToDelOnPath(Path path, Delegation toDel, string orderName)
	{
		PauseStateForOrder(orderName);
		foreach(Node node in path)
		{
			// Move to node
			yield return StartCoroutine(ActMoveToPosition(node.Location));
		}
		toDel.AcceptSubject(this);
	}

	#endregion
}
