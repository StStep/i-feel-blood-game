using UnityEngine;
using System.Collections;
using Manager;

/// <summary>
/// Takes in information from player UI and sends orders to Delegations, and makes delegations
/// </summary>
public class Dispatcher : MonoBehaviour {
	
	private Delegation queenDelegation;
	private Queen queen;
	private HUD hud;

	private Transform delagations;
	private Network onNetwork;
	private Network offNetwork;

	// Use this for initialization
	private void Start () {

		Node queenNode = new Node(Vector2.zero);
		onNetwork = new Network(queenNode);
		offNetwork = new Network(null);
	
		hud = InterfaceManager.GetHUD();

		delagations = transform.GetChild(0);
		if(delagations == null) {
			Debug.LogError("Error in Dispatcher object structure");
			Destroy(this);
		}

		// MakeForm Queen Delegation
		queen = (Queen) ObjectManager.CreateFormicant("Queen", queenNode.Location);
		if(queen == null)
		{
			Debug.LogError("Dispatcher: Failed to create queen");
			Destroy(this);
		}
		hud.UpdateEggCount(queen.EggCount);
		queenDelegation = CreateDelegation(queen);
		if(queenDelegation == null)
		{
			Debug.LogError("Dispatcher: Failed to create queen delegation");
			Destroy(this);
		}
		queenDelegation.name = "Queen's Delegation";
		queenDelegation.Deploy(queenNode);
	}
	
	// Update is called once per frame
	private void Update () {
	
	}

	public Delegation CreateDelegation(Royal royal)
	{
		Delegation newDelegation = ObjectManager.CreateDelegation();
		if(newDelegation == null)
		{
			Debug.LogError("Dispatcher: Failed to instantiate new delegation");
			return null;
		}
		newDelegation.transform.parent = delagations;
		newDelegation.AcceptRoyal(royal);
		
		return newDelegation;
	}

	/// <summary>
	/// Delgation exists around royal
	/// </summary>
	/// <param name="royal">Royal.</param>
	/// Later order Queen to make royal and give to new delegation?
	public Delegation CreateHandmaidenDelegation()
	{
		Delegation newDelegation = ObjectManager.CreateDelegation();
		if(newDelegation == null)
		{
			Debug.LogError("Dispatcher: Failed to instantiate new delegation");
			return null;
		}
		newDelegation.transform.parent = delagations;
		newDelegation.Location = queenDelegation.Location;

		//Order Queen to make and give the Royal
		IncubateRoyalOrder incubateRoyalOrder = new IncubateRoyalOrder("Incubate handmaiden", "Handmaiden", newDelegation);
		GiveOrder(incubateRoyalOrder, queenDelegation);

		return newDelegation;
	}

	public void MoveWorkerFromQueen(Delegation delTo)
	{
		/*GiveSubjectOrder giveSubjectOrder = new GiveSubjectOrder("Giving Worker", SubjectMorph.Worker, delTo);
		GiveOrder(giveSubjectOrder, queenDelegation);*/

		// Get path, determine validitry, both dels need nodes, means they are not moving
		Node startLocation = queenDelegation.Location;
		Node endLocation = delTo.Location;
		Path path = onNetwork.GetPath(startLocation, endLocation);
		if(path == null)
		{
			return;
		}

		// Get worker
		Worker worker = (Worker) queenDelegation.GetSubject(SubjectMorph.Worker);
		if(worker == null)
		{
			return;
		}

		// Order worker
		StartCoroutine(worker.OrdTranferToDelOnPath(path, delTo, "Transfer From Quuen"));

	}

	public void DeployDelegation(Delegation delegation, Vector2 location)
	{
		Node newNode = new Node(location);
		onNetwork.ConnectNodes(queenDelegation.Location, newNode);

		DeployOrder deployOrder = new DeployOrder("Deploy", newNode);
		GiveOrder(deployOrder, delegation);
	}

	public void DeplyDelegationOnPath(Delegation delegation, Path path)
	{
		Node priorNode = queenDelegation.Location;
		foreach(Node node in path)
		{
			onNetwork.ConnectNodes(priorNode, node);
			priorNode = node;
		}
		
		DeployOrder deployOrder = new DeployOrder("Deploy", priorNode);
		GiveOrder(deployOrder, delegation);
	}

	public void IncubateWorker()
	{
		//Dispathcer should be making order
		IncubateSubjectOrder incubateSubjectOrder = new IncubateSubjectOrder("Incubate Worker", "Worker");
		GiveOrder(incubateSubjectOrder, queenDelegation);
	}

	//Maybe reference delegation in some other manner?
	public void GiveOrder(Order order, Delegation delegation) {
		delegation.AcceptOrder(order);
	}

	public void TransferSubject(SubjectMorph morph, Delegation delFrom, Delegation delTo)
	{
		//Get path from network from one del to the other on the network
	}
}
