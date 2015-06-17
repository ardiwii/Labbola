using UnityEngine;
using System.Collections;

public class PlayerPanelController : MonoBehaviour {

	private PlayerStatScript plscr1;
	private PlayerStatScript plscr2;
	Vector2 leftpanelpos;
	Vector2 rightpanelpos;


	// Use this for initialization
	void Start () {
		plscr1 = transform.FindChild("PlayerStatPanel1").GetComponent<PlayerStatScript>();
		plscr2 = transform.FindChild("PlayerStatPanel2").GetComponent<PlayerStatScript>();
		leftpanelpos = transform.FindChild("PlayerPanelPosLeft").position;
		rightpanelpos = transform.FindChild("PlayerPanelPosRight").position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowPanel(GameObject playerSelected){
		if(playerSelected.GetComponent<PlayerAttribute>().leftteam){
			if(plscr1.isShowing){
				if(plscr1.inLeft){
					plscr2.transform.position = rightpanelpos;
					plscr2.inLeft = false;
					plscr2.LoadAndShow(playerSelected);
				}
				else{
					plscr2.transform.position = leftpanelpos;
					plscr2.inLeft = true;
					plscr2.LoadAndShow(playerSelected);
				}
			}
			else{
				plscr1.transform.position = leftpanelpos;
				plscr1.inLeft = true;
				plscr1.LoadAndShow(playerSelected);
			}
		}
		else{
			if(plscr2.isShowing){
				if(plscr2.inLeft){
					plscr1.transform.position = rightpanelpos;
					plscr1.inLeft = false;
					plscr1.LoadAndShow(playerSelected);
				}
				else{
					plscr1.transform.position = leftpanelpos;
					plscr1.inLeft = true;
					plscr1.LoadAndShow(playerSelected);
				}
			}
			else{
				plscr2.transform.position = rightpanelpos;
				plscr2.inLeft = false;
				plscr2.LoadAndShow(playerSelected);
			}
		}
	}
}
