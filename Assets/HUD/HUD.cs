using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Manager;

public class HUD : MonoBehaviour {

	public UnitWindow unitwindow;
	public DelegationWindow delegationWindow;
	public MainStatusBar mainStatusBar;
	public OrderWindow orderWindow;

	private void Awake()
	{
		InterfaceManager.SetHUD(this);
	}

	public void UpdateUnitWindow(Formicant formicant)
	{
		unitwindow.UpdateWindow(formicant);
	}

	public void UpdateDelegationWindow(Delegation delegation)
	{
		delegationWindow.UpdateWindow(delegation);
	}

	public void UpdateEggCount(int eggCount)
	{
		mainStatusBar.UpdateEggCount(eggCount);
	}

	public void AddOrder(Order order)
	{
		orderWindow.AddOrderCard(order);
	}

	public void UpdateOrder(Order order)
	{
		orderWindow.UpdateOrderCard(order);
	}

	public void RemoveOrder(Order order)
	{
		orderWindow.RemoveOrderCard(order);
	}
	
}
