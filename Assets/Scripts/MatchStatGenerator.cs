using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;

public class MatchStatGenerator : MonoBehaviour {

	bool finished;
	APILoader apiloader;
	public int homepos;
	public int awaypos;
	public Text homeText;
	public Text statText;
	public Text awayText;

	// Use this for initialization
	void Start () {
		apiloader = GameObject.Find("APILoader").GetComponent<APILoader>();
		finished = false;
	}
	
	// Update is called once per frame
	void Update () {
//		if(apiloader.getMatchJSON()!=null && !finished){
//			Debug.Log(apiloader.getMatchJSON());
//			LoadStat(apiloader.getMatchJSON());
//			finished = true;
//		}
	}

	public void LoadStat(string JSON_message){
		JSONArray parameters = JSON.Parse(JSON_message)["home_stat"].AsArray;
		homepos = JSON.Parse(parameters[1].ToString())["value"].AsInt;
		for(int i = 0; i< parameters.Count;i++){
			JSONNode param = JSON.Parse(parameters[i].ToString());
			Text statName = (Text) Instantiate(statText);
			Text homeParam = (Text) Instantiate(homeText);
			statName.gameObject.transform.SetParent(transform.FindChild("Stat Names").transform,false);
			statName.text = param["parameter"];
			homeParam.gameObject.transform.SetParent(transform.FindChild("Home Stat").transform,false);
			homeParam.text = param["value"];
		}
		JSONArray away_parameters = JSON.Parse(JSON_message)["away_stat"].AsArray;
		awaypos = JSON.Parse(away_parameters[1].ToString())["value"].AsInt;
		for(int i = 0; i< away_parameters.Count;i++){
			JSONNode param = JSON.Parse(away_parameters[i].ToString());
			Text awayParam = (Text) Instantiate(awayText);
			awayParam.gameObject.transform.SetParent(transform.FindChild("Away Stat").transform,false);
			awayParam.text = param["value"];
		}
	}
}
