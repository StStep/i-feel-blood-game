using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path :IEnumerable<Node>
{
	List<Node> _nodelist = new List<Node>();
	
	public Node this[int index]  
	{  
		get { return _nodelist[index]; }  
		set { _nodelist.Insert(index, value); }  
	}

	public int Count
	{
		get
		{
			return _nodelist.Count;
		}
	}

	public void AddNode(Node node)
	{
		_nodelist.Add(node);
	}
	
	public IEnumerator<Node> GetEnumerator()
	{
		return _nodelist.GetEnumerator();
	}
	
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	public void Reverse()
	{
		_nodelist.Reverse();
	}
}
