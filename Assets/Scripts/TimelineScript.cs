using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class TimelineScript : MonoBehaviour {

	public APILoader apiloader;
	public GameObject hometext;
	public GameObject awaytext;
	public Transform TextParent;

	public Text homename;
	public Text awayname;
	string hometeamname;
	string awayteamname;

	public struct TimelineEvent{
		string Player;
		string eventType;
		int minute;
		string stage;
		bool ishometeam;

		public TimelineEvent(string _playername,string _eventtype,bool _ishometeam,int _minute,string _stage){
			Player = _playername;
			eventType = _eventtype;
			ishometeam = _ishometeam;
			minute = _minute;
			stage = _stage;
		}

		public string Playername {
			get { return Player; }
			set { Player = value; }
		}

		public string eventname {
			get { return eventType; }	
			set { eventType = value; }
		}

		public bool isHome {
			get { return ishometeam; }	
			set { ishometeam = value; }
		}

		public int time {
			get { return minute; }	
			set { minute = value; }
		}

		public string match_stage {
			get { return stage; }	
			set { stage = value; }
		}
	}
	List<TimelineEvent> Timeline;

	// Use this for initialization
	void Start () {
		Timeline = new List<TimelineEvent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadTimeline(){
		StartCoroutine(LoadTimeline_Coroutine());
	}

	public IEnumerator LoadTimeline_Coroutine(){
		hometeamname = homename.text.ToString();
		awayteamname = awayname.text.ToString();
		Debug.Log(hometeamname);
		string retval = "";
		yield return StartCoroutine(apiloader.LoadTimeline(value => retval = value));
		Debug.Log(retval);
		JSONNode timelineJson = JSON.Parse(retval);
		JSONArray events = timelineJson["timeline"].AsArray;
		foreach(JSONNode eventJSON in events){
			string playername = eventJSON["player"]["name"];
			string eventtype = eventJSON["parameter"];
			//Debug.Log ("team: " + eventJSON["club"]["name"]);
			bool ishometeam = hometeamname.Contains(eventJSON["club"]["name"]);
			int minute = Random.Range(0,90);//eventJSON["minute"].AsInt;
			string stage = eventJSON["stage"];
			TimelineEvent timeline_event = new TimelineEvent(playername,eventtype,ishometeam,minute,stage);
			Timeline.Add(timeline_event);
		}
		Timeline = Timeline.OrderBy(TimelineEvent => TimelineEvent.time).ToList();
		foreach(TimelineEvent te in Timeline){
			GameObject eventText;
			if(te.isHome){
				eventText = Instantiate(hometext);
				eventText.transform.SetParent(TextParent);
				eventText.GetComponent<Text>().text = te.time + "'  " + te.Playername + " : " + te.eventname;
			}
			else{
				eventText = Instantiate(awaytext);
				eventText.transform.SetParent(TextParent);
				eventText.GetComponent<Text>().text = te.Playername + " : " + te.eventname + "  " + te.time + "'";
			}
			eventText.GetComponent<RectTransform>().localScale = Vector3.one;
			//Debug.Log(te.Playername + " : " + te.eventname);
		}
	}
}
