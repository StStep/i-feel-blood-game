using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FollowPathOrder : Order {
	
	Path _path;

	public FollowPathOrder(string orderName, Path path) : base(orderName)
	{
		_path = path;
	}

	/// <summary>
	/// Start coroutine?
	/// </summary>
	public override void StartOrder(Delegation delegation) 
	{
		base.StartOrder(delegation);

		_orderRoutine = _delegation.OrdFollowPath(_path, _orderName, _orderID);
		_delegation.StartOrderRoutine(_orderRoutine);
	}

	public override void UpdateStatus() 
	{
		base.UpdateStatus();
	}

}
