using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainStatusBar : MonoBehaviour {

	public Text eggCountText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateEggCount(int eggCount)
	{
		eggCountText.text = string.Format("Egg Count: {0}", eggCount);
	}
}
