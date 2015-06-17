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
		plattrib = transform.parent.GetComponent<PlayerAttribute>();
		if(plattrib!=null){
			leftTeam = plattrib.leftteam;
			StatPanel = GameObject.FindGameObjectWithTag("PlayerPanels");
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
		swapper.Select(transform.parent.gameObject);
	}

	public void showPanel(){
		//StatPanel.SetActive(true);
		//StatPanel.transform.Translate(new Vector3(300f,0,0));
//		StatPanel.GetComponent<PlayerStatScript>().LoadPlayerStat(gameObject);
//		StatPanel.GetComponent<PlayerStatScript>().LoadPlayerBio(gameObject);
		StatPanel.GetComponent<PlayerPanelController>().ShowPanel(transform.parent.gameObject);
	}
}
