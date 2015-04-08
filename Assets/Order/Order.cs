using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Manager;

public delegate void Callback();

/// <summary>
/// Stores order details, such as objective details, status information about order, callback to call when done, and coroutine to start
/// </summary>
/// 
/// THe ides is that the Delegation call most of the Order functions, the disp[ather only makes it and gives it to a delegation.
public abstract class Order {
	
	protected Delegation _delegation;
	protected int _orderID;
	protected string _orderName;
	protected IEnumerator _orderRoutine;
	protected float _progress;
	protected HUD _hud;
	
	public Order(string orderName) 
	{
		_orderID = OrderManager.GetNextOrderID();
		_orderName = orderName;
		_progress = 0;
		_hud = Manager.InterfaceManager.GetHUD();
	}
	
	public int OrderID 
	{
		get
		{
			return _orderID;
		}
	}

	public string OrderName 
	{
		get
		{
			return _orderName;
		}
	}

	public Delegation Delegation
	{
		get
		{
			return _delegation;
		}
	}

	public float Progress
	{
		get
		{
			return _progress;
		}
		set
		{
			_progress = value;
		}
	}


	public int OrderCount
	{
		get
		{
			return _delegation.OrderCount;
		}
	}

	/// <summary>
	/// Start coroutine?
	/// </summary>
	public virtual void StartOrder(Delegation delegation)
	{
		_delegation = delegation;
		_hud.AddOrder(this);
	}
	
	/// <summary>
	/// Stop coroutine and reverse junk?
	/// </summary>
	/// <returns><c>true</c> if this instance cancel order; otherwise, <c>false</c>.</returns>
	public virtual void CancelOrder() 
	{
		//Calculate path back from current point, and add that order,maybe?
		if(_orderRoutine == null) 
		{
			return;
		}
		_delegation.StopOrderRoutine(_orderRoutine);
		Debug.Log(String.Format("Order {0} canceled", OrderName));
		_hud.RemoveOrder(this);
	}
	
	/// <summary>
	/// Potentially ennqueue more commands
	/// </summary>
	public virtual void FinishOrder() 
	{
		//Don't really need to do much
		//Debug.Log(String.Format("Order {0} finished", OrderName));

		//Do cleanup
		_hud.RemoveOrder(this);

	}
	
	public virtual void UpdateStatus()
	{
		_hud.UpdateOrder(this);
	}
	
}

