using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
			Color color = selectedObject.GetComponentInChildren<Image>().color;
			color.a -= 0.25f;
			selectedObject.GetComponentInChildren<Image>().color = color;
		}
		else{
			if(selectedObject!=go){
				Color color = selectedObject.GetComponentInChildren<Image>().color;
				color.a = 1f;
				selectedObject.GetComponentInChildren<Image>().color = color;
				Color color2 = go.GetComponentInChildren<Image>().color;
				color2.a = 1f;
				go.GetComponentInChildren<Image>().color = color2;
				Swap(selectedObject,go);
			}
			else{
				Color color = selectedObject.GetComponentInChildren<Image>().color;
				color.a = 1f;
				selectedObject.GetComponentInChildren<Image>().color = color;
			}
			selectedObject = null;
		}
	}

	public void Cancel(GameObject go){
		if(selectedObject!=null){
			Color color = selectedObject.GetComponentInChildren<Image>().color;
			color.a = 1f;
			selectedObject.GetComponentInChildren<Image>().color = color;
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

		from.GetComponent<PlayerAttribute>().updateNameView();
		to.GetComponent<PlayerAttribute>().updateNameView();
	}
}
