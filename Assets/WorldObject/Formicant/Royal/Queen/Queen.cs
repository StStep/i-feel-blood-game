using UnityEngine;
using System.Collections;
using Manager;

public class Queen : Royal {

	private static float EggLayingInterval = 1.5F;

	private int _eggCount;
	private HUD _hud;

	public int EggCount
	{
		get
		{
			return _eggCount;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		_eggCount = 0;
	}

	protected override void Start () {
		base.Start();
		_hud = Manager.InterfaceManager.GetHUD();
		SwitchState(StLayingEggs, "Laying Eggs");
	}
	
	protected override void Update () {
		base.Update();
	}

	public int TakeEggs(int amount)
	{
		if(_eggCount >= amount)
		{
			_eggCount -= amount;
			return amount;
		}
		else
		{
			return 0;
		}

	}

	#region Coroutine States
	protected override IEnumerator StIdle() {

		yield return 0;
	}

	protected IEnumerator StLayingEggs() {

		_eggCount++;
		_hud.UpdateEggCount(_eggCount);
		//How do I get this to HUD? Through dispatcher or order??
		//TODO work this in with the uddate interval?
		yield return new WaitForSeconds(EggLayingInterval);
	}

	#endregion
	
	#region Order Called Functions
	
	#endregion
}
