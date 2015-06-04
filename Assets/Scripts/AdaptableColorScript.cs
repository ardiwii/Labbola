using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdaptableColorScript : MonoBehaviour {
	
	Color home_color;
	Color away_color;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initColor(){
		home_color = GameObject.FindGameObjectWithTag("HomeColor").GetComponent<Image>().color;
		away_color = GameObject.FindGameObjectWithTag("AwayColor").GetComponent<Image>().color;
		setColor();
	}

	public void setColor(){
		if(gameObject.name.Contains("home")||gameObject.name.Contains("Home")||transform.parent.name.Contains("Home")){
			GetComponent<Image>().color = new Color(home_color.r,home_color.g,home_color.b,GetComponent<Image>().color.a);
		}
		else if(gameObject.name.Contains("away")||gameObject.name.Contains("Away")||transform.parent.name.Contains("Away")) {
			GetComponent<Image>().color = new Color(away_color.r,away_color.g,away_color.b,GetComponent<Image>().color.a);
		}
		else{
			GetComponent<Image>().color = Color.white;
		}
	}
}
