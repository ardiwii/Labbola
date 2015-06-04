using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangingText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeText(string newText){
		GetComponent<Text>().text = newText;
	}
}
