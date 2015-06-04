using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerAttribute : MonoBehaviour {

	public string id;
	public string pl_name;
	public string number;
	public Hashtable parameters;
	public Hashtable bio;
	public bool leftteam;

	// Use this for initialization
	void Awake () {
		parameters = new Hashtable();
		bio = new Hashtable();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(name + parameters.Count);
	}

	public void InitParams(string JSON_message){
		JSONNode json = JSON.Parse(JSON_message);
		id = json["id"];
		pl_name = json["name"];
		number = json["number"];
		transform.GetChild(0).GetComponent<Text>().text = pl_name;
		JSONArray stats = json["statistics"].AsArray;
		for(int i = 0; i< stats.Count; i++){
			JSONNode stat = JSON.Parse(stats[i].ToString());
			string param = stat["parameter"];
			string value = stat["value"];
			parameters.Add(param,value);
		}
	}

	public void InitBio(string JSON_message){
		//Debug.Log(JSON_message);
		JSONNode json = JSON.Parse(JSON_message);
		bio.Add("national",json["national"]);
		bio.Add("height",json["height"]);
		bio.Add("weight",json["weight"]);
		bio.Add("birthdate",json["birthdate"]);
		bio.Add ("club",json["club"]["name"]);
	}

	public void setColor(Color color){
		GetComponent<Image>().color = color;
	}
}
