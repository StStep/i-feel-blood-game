using UnityEngine;
using System.Collections;
using Manager;

public class GameObjectList : MonoBehaviour {

	private static bool created = false;
	
	public Formicant[] formicantPrefabs;
	public WorldObject[] worldObjectPrefabs;
	public Delegation delegationPrefab;

	void Awake() {
		if(!created) {
			DontDestroyOnLoad(transform.gameObject);
			ObjectManager.SetGameObjectList(this);
			created = true;
		} else {
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Formicant GetFormicantPrefab(string name) {
		foreach(Formicant unit in formicantPrefabs) {
			if(unit.name == name) return unit;
		}
		return null;
	}
	
	public WorldObject GetWorldObjectPrefab(string name) {
		foreach(WorldObject worldObject in worldObjectPrefabs) {
			if(worldObject.name == name) return worldObject;
		}
		return null;
	}

	public Delegation GetDelegationPrefab()
	{
		return delegationPrefab;
	}

	public Formicant CreateFormicant(string formicantName, Vector2 location)
	{
		
		Formicant newFormicant = Instantiate(GetFormicantPrefab(formicantName), location.ToVec3(-1), Quaternion.identity) as Formicant;
		if(newFormicant == null)
		{
			Debug.LogError("GameObjectList: Failed to instantiate new formicant");
			return null;
		}
		
		return newFormicant;
	}

	public Delegation CreateDelegation()
	{
		Delegation newDelegation = Instantiate(delegationPrefab) as Delegation;
		if(newDelegation == null)
		{
			Debug.LogError("GameObjectList: Failed to instantiate new delegation");
			return null;
		}
		
		return newDelegation;
	}

}
