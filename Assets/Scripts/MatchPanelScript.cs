using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchPanelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		close ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void show(){
		GetComponent<CanvasGroup>().alpha = 1f;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void close(){
		GetComponent<CanvasGroup>().alpha = 0f;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
}
