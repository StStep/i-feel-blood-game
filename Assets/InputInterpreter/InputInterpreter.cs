using UnityEngine;
using System.Collections;
using Manager;

public class InputInterpreter : MonoBehaviour {


	private Formicant selectedFormicant;
	private Delegation selectedDelegation;

	private HUD _hud;

	// Use this for initialization
	void Awake () {
		InterfaceManager.SetInputInterpreter(this);
	}

	void Start()
	{
		_hud = InterfaceManager.GetHUD();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) OpenPauseMenu();
		MoveCamera();
	}

	#region Selection of Units

	public void SelectedFormicant(Formicant formicant)
	{
		if(selectedFormicant != null)
			selectedFormicant.Unselected();
		selectedFormicant = formicant;
		_hud.UpdateUnitWindow(formicant);
	}

	public void SelectedDelegation(Delegation delegation)
	{
		if(selectedDelegation != null)
			selectedDelegation.Unselected();
		selectedDelegation = delegation;
		_hud.UpdateDelegationWindow(delegation);
	}

/*	public void SelectedFormicant(Formicant formicant)
	{
		
	}*/

	#endregion


	private void MoveCamera() {

		float xpos = Input.mousePosition.x;
		float ypos = Input.mousePosition.y;
		Vector3 movement = new Vector3(0,0,0);
		
//		//horizontal camera movement
//		if(Input.GetKey(KeyCode.A)) {
//			movement.x -= ResourceManager.ScrollSpeed;
//		} else if(Input.GetKey(KeyCode.D)) {
//			movement.x += ResourceManager.ScrollSpeed;
//		}
//
//		//vertical camera movement
//		if(Input.GetKey(KeyCode.S)) {
//			movement.y -= ResourceManager.ScrollSpeed;
//		} else if(Input.GetKey(KeyCode.W)) {
//			movement.y += ResourceManager.ScrollSpeed;
//		}

		
		//horizontal camera movement
		if(Input.GetKey(KeyCode.A)) {
			movement.x -= 6;
			//player.hud.SetCursorState(CursorState.PanLeft);
		} else if(Input.GetKey(KeyCode.D)) {
			movement.x += 6;
			//player.hud.SetCursorState(CursorState.PanRight);
		}
		
		//vertical camera movement
		if(Input.GetKey(KeyCode.S)) {
			movement.y -= 6;
			//player.hud.SetCursorState(CursorState.PanDown);
		} else if(Input.GetKey(KeyCode.W)) {
			movement.y += 6;
			//player.hud.SetCursorState(CursorState.PanUp);
		}

		//make sure movement is in the direction the camera is pointing
		//but ignore the vertical tilt of the camera to get sensible scrolling
		movement = Camera.main.transform.TransformDirection(movement);

		//calculate desired camera position based on received input
		Vector3 origin = Camera.main.transform.position;
		Vector3 destination = origin;
		destination.x += movement.x;
		destination.y += movement.y;
		destination.z += movement.z;

		//if a change in position is detected perform the necessary update
		if(destination != origin) {
			Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * 6);
		}
	}

	private void OpenPauseMenu() {
		Time.timeScale = 0.0f;
/*		GetComponentInChildren< PauseMenu >().enabled = true;
		GetComponent< UserInput >().enabled = false;
		Screen.showCursor = true;
		ResourceManager.MenuOpen = true;*/
	}
}
