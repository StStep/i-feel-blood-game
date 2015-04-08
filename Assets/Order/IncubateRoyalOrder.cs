using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Manager;

//Order given to Queen, target delegation is where the new royal will go to
public class IncubateRoyalOrder : Order {
	
	string _royalName;
	Delegation _targetDelegation;
	
	public IncubateRoyalOrder(string orderName, string royalName, Delegation targetDelegation) : base(orderName)
	{
		_royalName = royalName;
		_targetDelegation = targetDelegation;
	}
	
	/// <summary>
	/// Start coroutine?
	/// </summary>
	public override void StartOrder(Delegation delegation) 
	{
		base.StartOrder(delegation);

		_orderRoutine = _delegation.OrdIncubateForm(_orderName, _orderID);
		_delegation.StartOrderRoutine(_orderRoutine);
	}
	
	public override void UpdateStatus() 
	{
		base.UpdateStatus();
	}
	
	public override void FinishOrder()
	{
		base.FinishOrder();
		
		Formicant newFormicant = ObjectManager.CreateFormicant(_royalName, _delegation.DelegationRoyal.Position);
		// TODO Put in royal check??
		_targetDelegation.AcceptRoyal((Royal) newFormicant);
	}
	
}
