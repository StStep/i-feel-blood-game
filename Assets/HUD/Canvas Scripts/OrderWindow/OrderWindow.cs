using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrderWindow : MonoBehaviour {

	public OrderCard orderCardPrefab;

	protected int _numOfOrderCards;
	protected List<OrderCard> _orderCardList;

	void Awake()
	{
		_numOfOrderCards = 0;
		_orderCardList = new List<OrderCard>();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private OrderCard CreateOrderCard()
	{
		OrderCard newOrderCard = Instantiate(orderCardPrefab) as OrderCard;
		if(newOrderCard == null)
		{
			Debug.LogError("OrderWindow: Failed to instantiate new OrderCard");
			return null;
		}
		
		return newOrderCard;
	}

	public void AddOrderCard(Order order)
	{
		OrderCard newOrderCard = CreateOrderCard();
		newOrderCard.transform.SetParent(this.transform, false);
		newOrderCard.InitCard(order, _numOfOrderCards);
		_orderCardList.Add(newOrderCard);
		_numOfOrderCards++;
	}

	public void UpdateOrderCard(Order order)
	{
		int i;
		for( i = 0; i < _orderCardList.Count; i++)
		{
			if(_orderCardList[i].Equals(order))
			{
				_orderCardList[i].UpdateCard(order);
				break;
			}
		}
	}

	public void RemoveOrderCard(Order order)
	{
		int i;
		for( i = 0; i < _orderCardList.Count; i++)
		{
			if(_orderCardList[i].Equals(order))
			{
				OrderCard removingCard = _orderCardList[i];
				_orderCardList.RemoveAt(i);
				removingCard.RemoveCard();
				_numOfOrderCards--;
				//Destroy(removingCard.gameObject);
				break;
			}
		}

		// Recheck OrderCard stored indexes from that point on
		for( int k = i; k < _orderCardList.Count; k++)
		{
			_orderCardList[k].UpdateIndex(k);
		}
	}
}
