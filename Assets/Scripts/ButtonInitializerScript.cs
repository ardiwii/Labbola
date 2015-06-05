using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonInitializerScript : MonoBehaviour {

	public GameObject StatPanel;
	private PlayerAttribute plattrib;
	private bool leftTeam;
	Button btn;
	SwapperScript swapper;

	// Use this for initialization
	void Start () {
		swapper = GameObject.FindGameObjectWithTag("Pitch").GetComponent<SwapperScript>();
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

		btn = GetComponent<Button>();
		btn.onClick.AddListener(() => { showPanel();  });
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void swapEnabled(){
		btn.onClick.RemoveAllListeners();
		btn.onClick.AddListener(() => { selectForSwap();  });
	}

	public void swapDisabled(){
		btn.onClick.RemoveAllListeners();
		btn.onClick.AddListener(() => { showPanel();  });
	}

	public void selectForSwap(){
		swapper.Select(gameObject);
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
