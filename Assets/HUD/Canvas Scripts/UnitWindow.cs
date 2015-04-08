using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitWindow : MonoBehaviour {

	public Text name;
	public Text state;
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

	public void UpdateWindow(Formicant formicant)
	{
		if(formicant != null)
		{
			name.text = "Name: " + formicant.name;
			state.text = "State: " + formicant.State;
			canvasGroup.alpha = .75f;
		}
		else
		{
			canvasGroup.alpha = 0;
			name.text = "Name: ";
			state.text = "State: ";
		}
	}
}
