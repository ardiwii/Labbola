using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatScript : MonoBehaviour {

	//public GameObject playerSelected;
	private bool leftpanel;
	public bool playerpanel; //false: teampanel

	public Text paramNameText;
	public Text paramValueText;

	// Use this for initialization
	void Start () {
		leftpanel = gameObject.tag.Equals("PlayerPanelLeft");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void LoadPlayerStat(GameObject playerSelected){	
		GameObject StatTab = transform.FindChild("Stat Tab").gameObject;
		PlayerAttribute plattrib = playerSelected.GetComponent<PlayerAttribute>();
		if(plattrib!=null){
			GameObject nameDisplay = StatTab.transform.FindChild("Name").gameObject;
			if(nameDisplay!=null){
				nameDisplay.GetComponent<Text>().text = plattrib.pl_name;
			}
			GameObject statChart = StatTab.transform.FindChild("ScrollView").FindChild("StatChart").gameObject;
			GameObject statChartName = statChart.transform.FindChild("StatChart_Name").gameObject;
			//statChart.GetComponent<HorizontalLayoutGroup>().enabled = false;
			for(int i = statChartName.transform.childCount-1;i>=0;i--){
				statChartName.transform.GetChild(i).GetComponent<LayoutElement>().enabled = false;
				Destroy(statChartName.transform.GetChild(i).gameObject);
			}
			GameObject statChartValue = statChart.transform.FindChild("StatChart_Value").gameObject;
			for(int i = statChartValue.transform.childCount-1;i>=0;i--){
				statChartName.transform.GetChild(i).GetComponent<LayoutElement>().enabled = false;
				Destroy(statChartValue.transform.GetChild(i).gameObject);
			}
			if(statChartName!=null && statChartValue!=null){
				foreach (string parameter_name in plattrib.parameters.Keys){
					Debug.Log ("key: " + parameter_name);
					Text paramName = (Text) Instantiate(paramNameText);
					Text paramValue = (Text) Instantiate(paramValueText);
					paramName.gameObject.transform.SetParent(statChartName.transform,false);
					paramName.text = parameter_name;
					paramValue.gameObject.transform.SetParent(statChartValue.transform,false);
					paramValue.text = plattrib.parameters[parameter_name].ToString();
				}
			}
		}
	}

	public void LoadPlayerBio(GameObject playerSelected){
		GameObject BioTab = transform.FindChild("Bio Tab").gameObject;
		PlayerAttribute plattrib = playerSelected.GetComponent<PlayerAttribute>();
		GameObject nameDisplay = BioTab.transform.FindChild("Name").gameObject;
		if(nameDisplay!=null){
			nameDisplay.GetComponent<Text>().text = plattrib.pl_name;
		}
		GameObject statChart = BioTab.transform.FindChild("StatChart").gameObject;
		GameObject statChartName = statChart.transform.FindChild("StatChart_Name").gameObject;
//		for(int i = statChartName.transform.childCount-1;i>=0;i--){
//			Destroy(statChartName.transform.GetChild(i));
//		}
		GameObject statChartValue = statChart.transform.FindChild("StatChart_Value").gameObject;
//		for(int i = statChartValue.transform.childCount-1;i>=0;i--){
//			Destroy(statChartValue.transform.GetChild(i));
//		}
		if(statChartName!=null && statChartValue!=null){
			foreach (string parameter_name in plattrib.bio.Keys){
				//Debug.Log ("key: " + parameter_name);
				Text paramName = (Text) Instantiate(paramNameText);
				Text paramValue = (Text) Instantiate(paramValueText);
				paramName.gameObject.transform.SetParent(statChartName.transform,false);
				paramName.text = parameter_name;
				paramValue.gameObject.transform.SetParent(statChartValue.transform,false);
				paramValue.text = plattrib.bio[parameter_name].ToString();
			}
		}
	}

	public void Drag(){
		transform.position = Input.mousePosition;
	}
}
