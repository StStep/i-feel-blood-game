using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class OrderCard : MonoBehaviour, IEquatable<Order> {

	public Text OrderName;
	public Text DelegationName;
	public Text StackSize;
	public Slider Progress;
	public RectTransform RectTrans;

	public Order _order;

	protected int _cardIndex;

	public int OrderID
	{
		get
		{
			return _order.OrderID;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitCard(Order order, int cardIndex)
	{
		Progress.value = 0;
		_order = order;
		OrderName.text = _order.OrderName;
		DelegationName.text = _order.Delegation.name;
		StackSize.text = order.OrderCount.ToString();
		_cardIndex = cardIndex;
		//RectTrans.position = Vector3.zero;
		RectTrans.localPosition = new Vector3(((100.0F * (- _cardIndex)) - 50.0F), 0, 0);
		//RectTrans.SetPositionX( - 50.0F);
		// RectTrans.SetPositionX;
	}

	public void UpdateCard(Order order)
	{
		Progress.value = order.Progress;
		StackSize.text = order.OrderCount.ToString();
	}

	public void UpdateIndex(int cardIndex)
	{
		_cardIndex = cardIndex;
		RectTrans.SetPositionX((100.0F * (- _cardIndex)) - 50.0F);
	}

	public void RemoveCard()
	{
		Destroy(gameObject);
	}

	public bool Equals(Order order)
	{
		if (order == null) return false;
		return (this._order.OrderID.Equals(order.OrderID));
	}
}
