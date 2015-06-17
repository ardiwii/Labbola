using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;

public class HeaderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initHeader(string json_message){
		Debug.Log("initating header: " + json_message);
		JSONNode json = JSON.Parse (json_message);
		transform.FindChild("Home Team Name").gameObject.transform.GetComponentInChildren<Text>().text = json["home_info"]["name"];
		transform.FindChild("Away Team Name").gameObject.transform.GetComponentInChildren<Text>().text = json["away_info"]["name"];
		transform.FindChild("Match Info").gameObject.transform.GetComponentInChildren<Text>().text =  json["stadium"] + "\n" + json["match_date"];
	}
}
