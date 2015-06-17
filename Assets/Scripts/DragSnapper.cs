using UnityEngine;
using System.Collections;

public class DragSnapper : MonoBehaviour {

	Camera camera;
	public bool leftside;

	// Use this for initialization
	void Start () {
		camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Snap(GameObject panelMoving){
		RectTransform rt = GetComponent<RectTransform>();
		Debug.Log(name + " " + rt.anchoredPosition);
		Rect rect;
		if(leftside){
			rect = new Rect(-8.0f,-5.0f,8f,10f)		;	
		}
		else{
			rect = new Rect(0f,-5.0f,8f,10f)	;
		}
		Vector2 panelPos = panelMoving.transform.position;
		Debug.Log(panelPos);
		if(rect.Contains(panelPos)){
			panelMoving.transform.position = transform.position;
			PlayerStatScript psscr = panelMoving.GetComponent<PlayerStatScript>();
			if(leftside){
				psscr.inLeft = true;
			}
			else{
				psscr.inLeft = false;
			}
		}
	}
}
