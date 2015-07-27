using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusIconController : MonoBehaviour {

	public GameObject GoalIcon;
	public GameObject YellowIcon;
	public GameObject RedIcon;
	public GameObject SubIcon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reset(){
		GameObject status = transform.FindChild("StatusIcons").gameObject;
		for(int i=status.transform.childCount-1;i>=0;i--){
			Destroy(status.transform.GetChild(i).gameObject);
		}
	}

	public void Goal(){
		GameObject Goal_Icon = (GameObject) Instantiate(GoalIcon);
		Goal_Icon.transform.SetParent(transform.FindChild("StatusIcons"));
		Goal_Icon.transform.localScale = new Vector3(1f,1f,1f);
	}

	public void YellowCard(){
		GameObject Yellowcard_Icon = (GameObject) Instantiate(YellowIcon);
		Yellowcard_Icon.transform.SetParent(transform.FindChild("StatusIcons"));
		Yellowcard_Icon.transform.localScale = new Vector3(1f,1f,1f);
	}

	public void RedCard(){
		GameObject Redcard_Icon = (GameObject) Instantiate(RedIcon);
		Redcard_Icon.transform.SetParent(transform.FindChild("StatusIcons"));
		Redcard_Icon.transform.localScale = new Vector3(1f,1f,1f);
	}

	public void Subs(){
		GameObject Subs_Icon = (GameObject) Instantiate(SubIcon);
		Subs_Icon.transform.SetParent(transform.FindChild("StatusIcons"));
		Subs_Icon.transform.localScale = new Vector3(1f,1f,1f);
	}

}
