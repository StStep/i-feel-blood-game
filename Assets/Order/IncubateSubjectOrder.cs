using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Manager;

//Order given to queen
public class IncubateSubjectOrder : Order {

	string _subjectName;
	
	public IncubateSubjectOrder(string orderName, string subjectName) : base(orderName)
	{
		_subjectName = subjectName;
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

		Formicant newFormicant = ObjectManager.CreateFormicant(_subjectName, _delegation.DelegationRoyal.Position);
		// TODO Put in subject check??
		_delegation.AcceptSubject((Subject) newFormicant);
	}
	
}
