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
				if(go.GetComponent<PlayerAttribute>().leftteam==selectedObject.GetComponent<PlayerAttribute>().leftteam){
					Color color = selectedObject.GetComponentInChildren<Image>().color;
					color.a = 1f;
					selectedObject.GetComponentInChildren<Image>().color = color;
					Color color2 = go.GetComponentInChildren<Image>().color;
					color2.a = 1f;
					go.GetComponentInChildren<Image>().color = color2;
					Swap(selectedObject,go);
					selectedObject = null;
				}
				else{
					Color color = selectedObject.GetComponentInChildren<Image>().color;
					color.a = 1f;
					selectedObject.GetComponentInChildren<Image>().color = color;
					selectedObject = go;
					color = selectedObject.GetComponentInChildren<Image>().color;
					color.a -= 0.25f;
					selectedObject.GetComponentInChildren<Image>().color = color;
				}
			}
			else{
				Color color = selectedObject.GetComponentInChildren<Image>().color;
				color.a = 1f;
				selectedObject.GetComponentInChildren<Image>().color = color;
				selectedObject = null;
			}
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

		if(from.transform.parent.name.Contains("Substitution")){
			from.GetComponentInChildren<PlayerDragScript>().enabled = false;
		}
		else{
			from.GetComponentInChildren<PlayerDragScript>().enabled = true;
		}
		if(to.transform.parent.name.Contains("Substitution")){
			to.GetComponentInChildren<PlayerDragScript>().enabled = false;
		}
		else{
			to.GetComponentInChildren<PlayerDragScript>().enabled = true;
		}
		
		from.GetComponent<PlayerAttribute>().updateNameView();
		to.GetComponent<PlayerAttribute>().updateNameView();
	}
}
