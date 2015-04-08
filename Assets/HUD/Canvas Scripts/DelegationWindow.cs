using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DelegationWindow : MonoBehaviour {

	public Text name;
	public Text royalName;
	public Text retinueSize;
	public CanvasGroup canvasGroup;

	void Awake()
	{
		canvasGroup.alpha = 0;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateWindow(Delegation delegtaion) 
	{
		if(delegtaion != null)
		{
			name.text = "Name: " + delegtaion.name;
			royalName.text = "Royal Name: " + delegtaion.DelegationRoyal.name;
			retinueSize.text = "Retinue Size: " + delegtaion.RetinueSize;
			canvasGroup.alpha = .75f;
		}
		else
		{
			canvasGroup.alpha = 0;
			name.text = "Name: ";
			royalName.text = "Royal Name: ";
			retinueSize.text = "Retinue Size: ";
		}

	}
}
