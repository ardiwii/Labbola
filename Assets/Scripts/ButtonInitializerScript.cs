using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonInitializerScript : MonoBehaviour {

	public GameObject StatPanel;
	private PlayerAttribute plattrib;
	private bool leftTeam;

	// Use this for initialization
	void Start () {
		plattrib = GetComponent<PlayerAttribute>();
		if(plattrib!=null){
			leftTeam = plattrib.leftteam;
			if(leftTeam){
				StatPanel = GameObject.FindGameObjectWithTag("PlayerPanelLeft");
			}
			else{
				StatPanel = GameObject.FindGameObjectWithTag("PlayerPanelRight");
			}
		}

		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(() => { showPanel();  });
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showPanel(){
		//StatPanel.SetActive(true);
		//StatPanel.transform.Translate(new Vector3(300f,0,0));
		StatPanel.GetComponent<HideableUIScript>().Show();
		Debug.Log(GetComponent<PlayerAttribute>().parameters.Count);
		StatPanel.GetComponent<PlayerStatScript>().LoadPlayerStat(gameObject);
		StatPanel.GetComponent<PlayerStatScript>().LoadPlayerBio(gameObject);
	}
}
