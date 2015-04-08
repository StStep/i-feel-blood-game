using UnityEngine;
using System.Collections;
using Manager;

public abstract class Formicant : WorldObject {

	public SpriteRenderer selectRender;
	public SpriteRenderer mainSpriteRender;

	private static float StateUpdateInterval =.3F;
	
	public float moveSpeed;
	public float rotateSpeed;
	public bool currentlySelected;
	
	protected InputInterpreter inputInterp;
	
	protected Delegation _delegation;
	protected bool onNetwork;
	
	protected string curState;
	protected string prevState;
	protected bool onOrder;

	protected delegate IEnumerator StateFunction();
	protected StateFunction curStateFunc;
	protected StateFunction prevStateFunc;

	public Delegation DelParent
	{
		get
		{
			return _delegation;
		}
		set
		{
			_delegation = value;
		}
	}

	public string State
	{
		get{
			return curState;
		}
	}

	public Vector2 Position
	{
		get
		{
			return transform.position.ToVec2();
		}
	}

	public abstract int MorphNum
	{
		get;
	}

	protected override void Awake() {
		base.Awake();

		//selectRender = GetComponentInChildren<SpriteRenderer>();
		mainSpriteRender.enabled = true;
		selectRender.enabled = false;
		currentlySelected = false;

		/* Start Idle, and start porcessing behavior, TODO move this somewhere else and stop coroutines when disabled? */
		curStateFunc = StIdle;
		curState = "Idle";
		onNetwork = true;
		onOrder = false;

		StartCoroutine(HandleState());
	}
	
	protected override void Start () {
		base.Start();

		inputInterp = InterfaceManager.GetInputInterpreter();

/*		if(this.useGUILayout)
			print ("Use GUI is tru");*/
	}

	protected override void Update () {
		base.Update();
	}

	#region UI Functions

	public virtual void MouseSelected()
	{
		inputInterp.SelectedFormicant(this);

		currentlySelected = true;
		selectRender.enabled = true;

		if(_delegation)
		{
			inputInterp.SelectedDelegation(_delegation);
			_delegation.Selected();
		}
		else
		{
			inputInterp.SelectedDelegation(null);
		}
	}

	public virtual void Selected()
	{
		currentlySelected = true;
		selectRender.enabled = true;
	}

	public virtual void Unselected()
	{
		currentlySelected = false;
		selectRender.enabled = false;
	}

	public virtual void MouseHoverEnter()
	{

	}

	public virtual void MouseHoverExit()
	{
		
	}

	#endregion

	/// <summary>
	/// Modifies the Formicant based on the current state, TODO turn off when deployed, maybe only for royals?
	/// </summary>
	protected IEnumerator HandleState() {

		for(;;) {
			if(curStateFunc != null) {
				yield return StartCoroutine(curStateFunc()); /* Start coroutine with string to allow it to be stopped */
			}
			yield return new WaitForSeconds(StateUpdateInterval);
		}
	}

	protected void SwitchState(StateFunction newStateFunc, string newState) {
		StopAllCoroutines();
		curStateFunc = newStateFunc;
		curState = newState;
		StartCoroutine(HandleState());
	}

	protected void InterruptState(StateFunction newStateFunc, string newState) {
		prevStateFunc = curStateFunc;
		prevState = curState;
		StopAllCoroutines();
		curStateFunc = newStateFunc;
		curState = newState;
		StartCoroutine(HandleState());
	}

	protected void PauseStateForOrder(string orderName) {
		prevStateFunc = curStateFunc;
		prevState = curState;
		if(onOrder == false)
			StopAllCoroutines();
		onOrder = true;
		curState = orderName;
	}

	protected void ResumeState() {
		StopAllCoroutines();
		onOrder = false;
		curStateFunc = prevStateFunc;
		curState = prevState;
		StartCoroutine(HandleState());
	}

	#region Coroutine States
	protected virtual IEnumerator StIdle() {
		//print ("Idle State");
		yield return StartCoroutine(ActWander(Position, 10));
	}
	#endregion

	#region Coroutine Actions

	protected IEnumerator ActMoveToPosition(Vector2 targetPosition)
	{
		Vector2 diff;
		diff.x = targetPosition.x - transform.GetPositionX();
		diff.y = targetPosition.y - transform.GetPositionY();
		float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		if(angle < 0) angle += 360;

		//print ("Starting to rotate");
/*		while (Mathf.Abs(transform.GetAngleZ() - targetAngle) > .1f)
		{
			float angle = Mathf.MoveTowardsAngle(transform.GetAngleZ(), targetAngle, rotateSpeed * Time.deltaTime);
			transform.SetAngleZ(angle);
			yield return 0;
		}*/

		transform.SetAngleZ(angle);

		Vector3 targetPosition3 = targetPosition.ToVec3(transform.position.z);

		//print ("Starting to move");
		while (transform.position != targetPosition3)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition3, moveSpeed * Time.deltaTime);
			//CalculateBounds();
			yield return 0;
		}
	}

	/// <summary>
	/// Moves the Formicant to a random location around a center
	/// </summary>
	protected IEnumerator ActWander(Vector2 wanderCenter, float wonderMaxRange) {
		//TODO could make this wander to a point in a safe zone
		Vector2 randomXY = Random.insideUnitCircle * (Random.value * wonderMaxRange);
		Vector2 newPosition = new Vector2(wanderCenter.x + randomXY.x, wanderCenter.y + randomXY.y);

		//print (string.Format("Starting to wander to position ({0}, {1}) with angle {2}", newPosition.x, newPosition.y, angle));


		yield return StartCoroutine(ActMoveToPosition(newPosition));
	}

	#endregion

	#region Order Called Functions

	//Actions private, orders public
	public IEnumerator OrdMoveToPosition(Vector2 position, string orderName) 
	{
		PauseStateForOrder(orderName);
		yield return StartCoroutine(ActMoveToPosition(position));
		ResumeState();
	}

	#endregion

}
