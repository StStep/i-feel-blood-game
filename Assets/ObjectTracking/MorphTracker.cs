using UnityEngine;
using System.Collections.Generic;

public class MorphTracker : MonoBehaviour {

	//List<List<Formicant>> trackedFormicants;
	Dictionary<int, Queue<Formicant>> trackedMorphs;

	// Use this for initialization
	void Awake () {
	
		//trackedFormicants = new List<List<Formicant>>();
		trackedMorphs = new Dictionary<int, Queue<Formicant>>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddFormicant(Formicant form, int morph) {

		Queue<Formicant> morphQ;

		if(!trackedMorphs.TryGetValue(morph, out morphQ)) {
			// Queue doesn't exist yet
			morphQ = new Queue<Formicant>();
			trackedMorphs.Add(morph, morphQ);

		}

		// Add to both queue and object heirachy
		morphQ.Enqueue(form);
		form.transform.parent = transform;

	}

	public Formicant GetFormicant(int morph) {

		Queue<Formicant> morphQ;
		Formicant removedFormicant;
		
		if(trackedMorphs.TryGetValue(morph, out morphQ)) {

/*			if(morphQ == null)
			{
				Debug.LogError("ERROR - MorphTracker: Attemp to use unitialized morphQ");
				removedFormicant = null;
			}*/

			// Queue exists alrady
			if(morphQ.Count != 0)
			{
				removedFormicant = morphQ.Dequeue();
			}
			else
			{
				removedFormicant = null;
			}
			
		}
		else {
			// Queue doesn't exist yet
			Debug.Log("WARNING - MorphTracker: Attempt to get a formicant that doesn't exist");
			removedFormicant = null;
		}
		return removedFormicant;
	}

	public int GetMorphCount(int morph) {

		Queue<Formicant> morphQ;
		
		if(trackedMorphs.TryGetValue(morph, out morphQ)) {

/*			if(morphQ == null)
			{
				Debug.LogError("ERROR - MorphTracker: Attemp to use unitialized morphQ");
				return 0;
			}*/

			// Queue exists alrady
			return morphQ.Count;
			
		}
		else {
			// Queue doesn't exist
			return 0;
		}
	}

	public int GetSize() {

		int size = 0;
		
		foreach(Queue<Formicant> queue in trackedMorphs.Values)
		{
			size += queue.Count;
		}

		return size;
	}

	public void SelectAll()
	{
		foreach(Queue<Formicant> queue in trackedMorphs.Values)
		{
			foreach(Formicant Formicant in queue )
			{
				Formicant.Selected();
			}
		}
	}

	public void UnselectAll()
	{
		foreach(Queue<Formicant> queue in trackedMorphs.Values)
		{
			foreach(Formicant Formicant in queue )
			{
				Formicant.Unselected();
			}
		}
	}

}
