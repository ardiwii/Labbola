using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PosessionZoneScript : MonoBehaviour {

	bool isShowing;

	// Use this for initialization
	void Start () {
		isShowing = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setZone(){
		GameObject ms = GameObject.FindGameObjectWithTag("MatchStat");
		float home = ms.GetComponent<MatchStatGenerator>().homepos;
		float away = ms.GetComponent<MatchStatGenerator>().awaypos;
		GameObject homeZone = transform.GetChild (0).gameObject;
		homeZone.GetComponent<Image>().enabled = true;
		homeZone.GetComponent<RectTransform>().localScale = new Vector3 (home/100,1f,1f);
		GameObject awayZone = transform.GetChild (1).gameObject;
		awayZone.GetComponent<Image>().enabled = true;
		awayZone.GetComponent<RectTransform>().localScale = new Vector3 (away/100,1f,1f);
	}

	public void show(){
		if(!isShowing){
			isShowing = true;
			setZone();
			foreach (Image img in transform.GetComponentsInChildren<Image>()){
				img.enabled = true;
			}
		}
		else{
			isShowing = false;
			foreach (Image img in transform.GetComponentsInChildren<Image>()){
				img.enabled = false;
			}
		}
	}
}
