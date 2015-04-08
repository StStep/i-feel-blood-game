using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Manager;

public class WorldObject : MonoBehaviour {

	public string name;
	public Texture2D objectImage;
	public int ObjectId { get; set; }

	protected virtual void Awake() {
		this.useGUILayout = false; //  Should make run faster
	}
	
	protected virtual void Start () {

	}
	
	protected virtual void Update () {
		
	}

	public void SetColliders(bool enabled) {
		Collider[] colliders = GetComponentsInChildren< Collider >();
		foreach(Collider collider in colliders) collider.enabled = enabled;
	}
}
