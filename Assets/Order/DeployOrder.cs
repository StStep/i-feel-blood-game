using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DeployOrder : Order {

	Node _location;
	
	public DeployOrder(string orderName, Node location) : base(orderName)
	{
		_location = location;
	}
	
	/// <summary>
	/// Start coroutine?
	/// </summary>
	public override void StartOrder(Delegation delegation) 
	{
		base.StartOrder(delegation);

		_orderRoutine = _delegation.OrdDeploy(_location, _orderName, _orderID);
		_delegation.StartOrderRoutine(_orderRoutine);
	}
	
	public override void UpdateStatus() 
	{
		base.UpdateStatus();
	}
	
}
