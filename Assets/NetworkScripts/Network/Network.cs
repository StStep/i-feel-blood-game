using UnityEngine;
using System.Collections.Generic;

public class Network {

	protected Dictionary<int, Node> _netNodes;

	protected Node _primaryNode;
	public Node PrimaryNode
	{
		get
		{
			return _primaryNode;
		}
	}

	public Network(Node primaryNode) {
		_netNodes = new Dictionary<int, Node>();

		_primaryNode = primaryNode;
		if(_primaryNode != null)
		{
			_netNodes[_primaryNode.LocID] = _primaryNode;
		}
	}

	public void ConnectNodes(Node node1, Node node2)
	{
		_netNodes[node1.LocID] = node1;
		_netNodes[node2.LocID] = node2;

		node1.AddConnection(node2);
		node2.AddConnection(node1);
	}

	/// <summary>
	/// Calculates the distance squared.
	/// </summary>
	/// <returns>The distance squared.</returns>
	/// <param name="node1">Node1.</param>
	/// <param name="node2">Node2.</param>
	public static float calculateDistance(Node node1, Node node2)
	{
		return 	((node1.Location.x - node2.Location.x) * (node1.Location.x - node2.Location.x) ) + 
				((node1.Location.y - node2.Location.y) * (node1.Location.y - node2.Location.y) );
	}

	public Path GetPath(Node startNode, Node endNode)
	{
		if(startNode == null || endNode == null)
		{
			return null;
		}

		// Already at node
		Path retPath;
		if(startNode.LocID == endNode.LocID)
		{
			retPath = new Path();
			return retPath;
		}

		Node start;
		Node goal;

		_netNodes.TryGetValue(startNode.LocID, out start);//_netNodes[startNode.LocID];
		_netNodes.TryGetValue(endNode.LocID, out goal);//_netNodes[endNode.LocID];
		if(start == null || goal == null)
		{
			return null;
		}

		// Make Lists for search
		List<NodeWrap> openList = new List<NodeWrap>();
		List<NodeWrap> closedList = new List<NodeWrap>();

		// Place starting node in open list list
		openList.Add(new NodeWrap(start, null, 0, calculateDistance(start, goal)));

		NodeWrap curNodeWrap;
		NodeWrap goalNodeWrap = null;

		while(true)
		{
			//Exit upon empty openList
			if(openList.Count == 0)
			{
				break;
			}

			// Sort openList and choose lowest F
			openList.Sort();

			// Take lowest F in openList and move to closedList
			curNodeWrap = openList[0];
			openList.RemoveAt(0);
			closedList.Add(curNodeWrap);

			// Check for goal node
			if(curNodeWrap.Equals(goal))
			{
				goalNodeWrap = curNodeWrap;
				break;
			}


			// Add connected nodes to open list if not already listed in closedList, update if on openList
			foreach(Node node in curNodeWrap.node)
			{
				bool found = false;

				// Ignore node if on closed list
				foreach(NodeWrap nodeWrap in closedList)
				{
					if(nodeWrap.Equals(node))
					{
						found = true;
						break;
					}
				}

				if(found)
				{
					continue;
				}

				// Potentially update NodeWrap if it is on the openList and the costToNode is less
				foreach(NodeWrap nodeWrap in openList)
				{
					if(nodeWrap.Equals(node))
					{
						// Compare costToNode, update parent if node has lower cost
						float newCostToNode = curNodeWrap.costToNode + calculateDistance(curNodeWrap.node, node);
						if(newCostToNode < nodeWrap.costToNode)
						{
							nodeWrap.parentNodeWrap = curNodeWrap;
							nodeWrap.costToNode = newCostToNode;
						}

						found = true;
						break;
					}
				}
				
				if(found)
				{
					continue;
				}

				openList.Add(new NodeWrap(node, curNodeWrap, curNodeWrap.costToNode + calculateDistance(curNodeWrap.node, node), calculateDistance(node, goal)));
			}
		}

		if(goalNodeWrap == null)
		{
			return null;
		}

		// Make a path if the final NodeWrap, a path from start to goal, was found
		retPath = new Path();
		NodeWrap parent = goalNodeWrap;

		// Create path from goal to start
		while(parent != null)
		{
			retPath.AddNode(parent.node);
			parent = parent.parentNodeWrap;
		}

		// Reverse path becuase it was created from goal to start
		retPath.Reverse();

		return retPath;
	}
}
