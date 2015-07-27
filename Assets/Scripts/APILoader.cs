using UnityEngine;
using System.Collections;

public class APILoader : MonoBehaviour {

	//WWW connect;
	public string Token;
	public int match_id;
	public PlayerGenerator plgen;
	public MatchStatGenerator msgen;
	public HeaderScript header;
	public HeaderScript splashscreen;

	string url;
	string players_stats_JSON;
	string match_stats_JSON;
	string players_bio_JSON;

	public string getPlayerJSON(){
		return players_stats_JSON;
	}

	public string getMatchJSON(){
		return match_stats_JSON;
	}

	// Use this for initialization
	void Start () {
		match_id = 1567;
		Token = "ycmqtnhqpiKMig2nRsjW";

		StartCoroutine("LoadMatchStats");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitMatch(){
		StartCoroutine("LoadPlayers");
	}

	public IEnumerator LoadPlayers(){
		Debug.Log("Loading Players");
		WWW connect = new WWW("http://labbola-new.wiradipa.com/api/player-stats?auth_token="+Token+"&match_id="+match_id);
		yield return connect;
		if(connect.isDone){
			Debug.Log("Players statistics loaded");
			players_stats_JSON = connect.text;
			plgen.GeneratePlayers(players_stats_JSON);
		}
	}
	
	public IEnumerator LoadMatchStats(){
		WWW connect = new WWW("http://labbola-new.wiradipa.com/api/match-stats?auth_token="+Token+"&match_id="+match_id);
		yield return connect;
		if(connect.isDone){
			match_stats_JSON = connect.text;
			msgen.LoadStat(connect.text);
			header.initHeader(connect.text);
			splashscreen.initHeader(connect.text);
		}
	}

	public IEnumerator FetchPlayerBio(int player_id, System.Action<string> result){
		WWW connect = new WWW("http://labbola-new.wiradipa.com/api/player-infos?auth_token="+Token+"&player_id="+player_id);
		yield return connect;
		if(connect.isDone){
			result(connect.text);
		}
	}

	public IEnumerator LoadTimeline(System.Action<string> result){
		Debug.Log("Loading Players");
		WWW connect = new WWW("http://labbola-new.wiradipa.com/api/match-timeline?auth_token="+Token+"&match_id="+match_id);
		yield return connect;
		if(connect.isDone){
			result(connect.text);
		}
	}

	//timeline:
	//http://labbola-new.wiradipa.com/api/match-timeline?auth_token="+token+"&match_id="+match_id
	//
	//contribution/heat map
	//http://labbola-new.wiradipa.com/api/match-contributions?auth_token="+token+"&match_id="+match_id


}
