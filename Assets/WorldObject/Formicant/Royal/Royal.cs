using UnityEngine;
using System.Collections;
using Manager;

public class Royal : Formicant {

	public float controlRange;
	public SpriteRenderer deployedSpriteRender;

	public RoyalMorph morph;

	public override int MorphNum {
		get {
			return (int) morph;
		}
	}

	protected bool _deployed;
	public bool Deployed
	{
		get
		{
			return _deployed;
		}
	}

	protected override void Awake () {
		base.Awake();
		_deployed = false;
		deployedSpriteRender.enabled = false;

	}
	
	protected override void Update () {
		base.Update();
	}

	public void Deploy()
	{
		moveSpeed = 0;
		_deployed = true;

		//Do a model change
		mainSpriteRender.enabled = false;
		deployedSpriteRender.enabled = true;

	}
}
