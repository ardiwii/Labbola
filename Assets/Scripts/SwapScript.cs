using UnityEngine;
using System.Collections;

public class SwapScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Swap(GameObject go){
		Transform thisposition = transform;
		Transform targetposition = go.transform;
		transform.SetParent(targetposition.parent,false);
		transform.position = targetposition.position;
		go.transform.SetParent(thisposition.parent,false);
		go.transform.position = thisposition.position;
	}
}
