using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Manager;

public class Node :IEnumerable<Node> , IEquatable<Node>
{

	protected int _locID;
	public int LocID 
	{
		get
		{ 
			return _locID; 
		}
	}

	protected Vector2 _location;
	public Vector2 Location
	{
		get
		{
			return _location;
		}
	}

	protected List<Node> _connectedNodes;

	public Node(Vector2 location) 
	{
		_connectedNodes = new List<Node>();
		_locID = NetworkManager.GetNextLocID();
		_location = location;
	}

	public void AddConnection(Node node)
	{
		if(!_connectedNodes.Contains(node))
		{
			_connectedNodes.Add(node);
		}
	}

	public void RemoveConnection(Node node)
	{
		if(_connectedNodes.Contains(node))
		{
			_connectedNodes.Remove(node);
		}
	}

	public Node this[int index]  
	{  
		get { return _connectedNodes[index]; }  
		set { _connectedNodes.Insert(index, value); }  
	} 

	public IEnumerator<Node> GetEnumerator()
	{
		return _connectedNodes.GetEnumerator();
	}
	
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	public bool Equals(Node node)
	{
		if (node == null) return false;
		return (this.LocID.Equals(node.LocID));
	}
}
