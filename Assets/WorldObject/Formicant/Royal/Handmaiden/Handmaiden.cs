using UnityEngine;
using System.Collections;

public class Handmaiden : Royal {

	protected override void Start () {
		base.Start();
	}
	
	protected override void Update () {
		base.Update();
	}

	protected override IEnumerator StIdle() {
		yield return 0;
	}
}
