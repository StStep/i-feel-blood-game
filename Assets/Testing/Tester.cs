using UnityEngine;
using System.Collections;

public class Tester : MonoBehaviour {

	public Dispatcher GameDispather;

	private Path _testPath;
	private Delegation _testDelegation;
	private bool _gaveMoveOrder;
	private bool _madeDelegation;

	// Use this for initialization
	void Start () {
	
		_testPath = new Path();

		//Create Path
		Node node1 = new Node(new Vector2(0,0));
		_testPath.AddNode(node1);
		Node node2 = new Node(new Vector2(4,2));
		_testPath.AddNode(node2);
		Node node3 = new Node(new Vector2(3.5f,4.5f));
		_testPath.AddNode(node3);
		Node node4 = new Node(new Vector2(6,6));
		_testPath.AddNode(node4);

		_gaveMoveOrder = false;
		_madeDelegation = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TestMoveOrder()
	{
		if(_testDelegation == null)
		{
			//Debug.LogError("Tester: No delegation to give order");
			return;
		}
		
		//Dispathcer should be maing order
		FollowPathOrder moveOrder = new FollowPathOrder("Test move", _testPath);

		GameDispather.GiveOrder(moveOrder, _testDelegation);

		_gaveMoveOrder = true;
	}

	public void TestMakeOrder()
	{
		GameDispather.IncubateWorker();
	}

	public void TestHandmaidenCreationOrder()
	{
		if(_madeDelegation)
		{
			return;
		}
		_madeDelegation = true;
		_testDelegation = GameDispather.CreateHandmaidenDelegation();
	}

	public void GiveNewDelegationSubjects()
	{
		if(_testDelegation == null)
		{
			//Debug.LogError("Tester: No delegation to give order");
			return;
		}
		GameDispather.MoveWorkerFromQueen(_testDelegation);
	}

	public void TestDeployOrder()
	{
		if(_testDelegation == null)
		{
			//Debug.LogError("Tester: No delegation to give order");
			return;
		}

		if(!_gaveMoveOrder)
		{
			return;
		}

		GameDispather.DeplyDelegationOnPath(_testDelegation, _testPath);
	}
}
