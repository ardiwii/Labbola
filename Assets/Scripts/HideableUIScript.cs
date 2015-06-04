using UnityEngine;
using System.Collections;

public class HideableUIScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show(){
		GetComponent<CanvasGroup>().alpha = 1f;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}
	
	public void Close(){
		GetComponent<CanvasGroup>().alpha = 0f;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
}
