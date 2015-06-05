using UnityEngine;
using System.Collections;

public class SwapperScript : MonoBehaviour {

	public GameObject selectedObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Select(GameObject go){
		if(selectedObject==null){
			selectedObject = go;
		}
		else{
			if(selectedObject!=go){
				Swap(selectedObject,go);
			}
			selectedObject = null;
		}
	}

	public void Swap(GameObject from, GameObject to){
		Transform thisposition = from.transform;
		Vector2 temp_position = thisposition.position;
		GameObject temp_parent = thisposition.parent.gameObject;

		Transform targetposition = to.transform;

		from.transform.SetParent(targetposition.parent,false);
		from.transform.position = targetposition.position;

		to.transform.SetParent(temp_parent.transform,false);
		to.transform.position = temp_position;
	}
}
