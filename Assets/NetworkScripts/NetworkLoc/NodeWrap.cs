using UnityEngine;
using System;
using System.Collections;

public class NodeWrap : IComparable<NodeWrap> , IEquatable<Node> 
{

	public Node node;
	public NodeWrap parentNodeWrap;

	/// <summary>
	/// G
	/// </summary>
	public float costToNode;

	/// <summary>
	/// H, heuristic cost, distance to goal, as the crow flies
	/// </summary>
	public float estCostToGoal;

	public float F
	{
		get
		{
			return (costToNode + estCostToGoal);
		}
	}

	public NodeWrap(Node node, NodeWrap parentNodeWrap, float costToNode, float estCostToGoal )
	{
		this.node = node;
		this.parentNodeWrap = parentNodeWrap;
		this.costToNode = costToNode;
		this.estCostToGoal = estCostToGoal;
	}

	public int CompareTo(NodeWrap otherNodeWrap) {
		if (otherNodeWrap == null) 
			return 1;

		return this.F.CompareTo(otherNodeWrap.F);

	}

	public bool Equals(Node node)
	{
		if (node == null) return false;
		return (this.node.LocID.Equals(node.LocID));
	}
}
