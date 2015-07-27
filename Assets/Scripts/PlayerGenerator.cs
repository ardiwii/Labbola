using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;


//seharusnya namanya PlayerController
public class PlayerGenerator : MonoBehaviour {

	public GameObject playerPrefab;
	bool finished;
	public APILoader apiloader;
	public int[] HomeFormation;
	public int[] AwayFormation;
	Color home_color;
	Color away_color;
	public HideableUIScript clearDrawButton;
	public HideableUIScript homescroll;
	public HideableUIScript awayscroll;
	GameObject home_formation;
	GameObject away_formation;


	// Use this for initialization
	void Start () {
		home_formation = transform.FindChild("Home Formation").gameObject;
		away_formation = transform.FindChild("Away Formation").gameObject;
		//finished = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void GeneratePlayers(string JSON_message){
		home_color = GameObject.FindGameObjectWithTag("HomeColor").GetComponent<Image>().color;
		away_color = GameObject.FindGameObjectWithTag("AwayColor").GetComponent<Image>().color;
		JSONNode home = JSON.Parse(JSON_message)["home"];
		JSONNode away = JSON.Parse(JSON_message)["away"];

		//homeplayers
		JSONArray players = home["players_stats"].AsArray;
		//Debug.Log(players.ToString());
		int index = 0;
		for(int i=0;i<HomeFormation[0];i++){
			GenerateAPlayer(players[index].ToString(),0,true);
			index++;
		}
		for(int i=0;i<HomeFormation[1];i++){
			GenerateAPlayer(players[index].ToString(),1,true);
			index++;
		}
		for(int i=0;i<HomeFormation[2];i++){
			GenerateAPlayer(players[index].ToString(),2,true);
			index++;
		}
		for(int i=0;i<HomeFormation[3];i++){
			GenerateAPlayer(players[index].ToString(),3,true);
			index++;
		}
		for(int i=index;i<players.Count;i++){
			GenerateAPlayer(players[index].ToString(),4,true);
			index++;
		}

		//awayplayers
		JSONArray away_players = away["players_stats"].AsArray;
		Debug.Log(away_players.ToString());
		index = 0;
		for(int i=0;i<AwayFormation[0];i++){
			GenerateAPlayer(away_players[index].ToString(),0,false);
			index++;
		}
		for(int i=0;i<AwayFormation[1];i++){
			GenerateAPlayer(away_players[index].ToString(),1,false);
			index++;
		}
		for(int i=0;i<AwayFormation[2];i++){
			GenerateAPlayer(away_players[index].ToString(),2,false);
			index++;
		}
		for(int i=0;i<AwayFormation[3];i++){
			GenerateAPlayer(away_players[index].ToString(),3,false);
			index++;
		}
		for(int i=index;i<players.Count;i++){
			GenerateAPlayer(away_players[index].ToString(),4,false);
			index++;
		}

		//GameObject.Find("PlayerStatPanelLeft").SetActive(false);
		Debug.Log(home.ToString());
		Debug.Log(away.ToString());
	}

	public void GenerateAPlayer(string JSON_message, int poscode, bool hometeam){
		GameObject newplayer = (GameObject) Instantiate(playerPrefab);
		GameObject team;
		GameObject position = new GameObject();
		if(hometeam){
			newplayer.GetComponent<PlayerAttribute>().leftteam = true;
			team = transform.FindChild("Home Formation").gameObject;
			newplayer.GetComponent<PlayerAttribute>().setColor(home_color);

		}
		else{
			newplayer.GetComponent<PlayerAttribute>().leftteam = false;
			team = transform.FindChild("Away Formation").gameObject;
			newplayer.GetComponent<PlayerAttribute>().setColor(away_color);
		}
		switch(poscode){
		case 0: position = team.transform.FindChild("Goalkeeper").gameObject; break;
		case 1: position = team.transform.FindChild("Defender").gameObject;break;
		case 2: position = team.transform.FindChild("Midfielder").gameObject;break;
		case 3: position = team.transform.FindChild("Forward").gameObject;break;
		case 4: position = team.transform.FindChild("SubstitutionPanel").FindChild("Substitution").gameObject;
				newplayer.GetComponentInChildren<PlayerDragScript>().enabled = false;
				break;
		}

		JSONNode player = JSON.Parse(JSON_message);
		//Debug.Log(player.ToString());
		StartCoroutine(LoadBio(newplayer,player["id"].AsInt));
		newplayer.transform.SetParent(position.transform,false);
		newplayer.GetComponent<PlayerAttribute>().InitParams(player.ToString());

		GameObject playerpallete = (GameObject) Instantiate(newplayer,gameObject.transform.position,new Quaternion());
		if(hometeam){
			playerpallete.transform.SetParent(transform.FindChild("Home Palette").FindChild("Scrollable").FindChild("Content"),false);
		} 
		else {
			playerpallete.transform.SetParent(transform.FindChild("Away Palette").FindChild("Scrollable").FindChild("Content"),false);
		}
		playerpallete.tag = "PaletteItem";
		playerpallete.GetComponentInChildren<PlayerDragScript>().enabled = true;
		playerpallete.GetComponentInChildren<Button>().enabled = false;
		playerpallete.GetComponent<PlayerAttribute>().updateNameView();
	}

	public IEnumerator LoadBio(GameObject player, int player_id){
		string retval = "{}";
		yield return apiloader.StartCoroutine(apiloader.FetchPlayerBio(player_id,value => retval = value));
		JSONNode json = JSON.Parse(retval);
		string biojson = json["player"].ToString();
		player.GetComponent<PlayerAttribute>().InitBio(biojson);
	}


	//---------------DRAG----------------------
	public void enableDrag(){
		foreach (VerticalLayoutGroup vlg in home_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(PlayerDragScript ds in vlg.gameObject.transform.GetComponentsInChildren<PlayerDragScript>()){
				if(!ds.transform.parent.parent.name.Contains("Substitution")){
					ds.enabled = true;
				}
				//ds.gameObject.GetComponent<Button>().enabled=false;
			}
		}
		foreach (VerticalLayoutGroup vlg in away_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(PlayerDragScript ds in vlg.gameObject.transform.GetComponentsInChildren<PlayerDragScript>()){
				if(!ds.transform.parent.parent.name.Contains("Substitution")){
					ds.enabled = true;
				}
				//ds.gameObject.GetComponent<Button>().enabled=false;
			}
		}
	}

	public void disableDrag(){
		foreach (VerticalLayoutGroup vlg in home_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(PlayerDragScript ds in vlg.gameObject.transform.GetComponentsInChildren<PlayerDragScript>()){
				ds.enabled = false;
				//ds.gameObject.GetComponent<Button>().enabled=true;
			}
		}
		foreach (VerticalLayoutGroup vlg in away_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(PlayerDragScript ds in vlg.gameObject.transform.GetComponentsInChildren<PlayerDragScript>()){
				ds.enabled = false;
				//ds.gameObject.GetComponent<Button>().enabled=true;
			}
		}
	}


	//-------------------BUTTON------------------------------
	public void enableButton(){
		foreach (VerticalLayoutGroup vlg in home_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(Button btn in vlg.gameObject.transform.GetComponentsInChildren<Button>()){
				btn.enabled = true;
			}
		}
		foreach (VerticalLayoutGroup vlg in away_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(Button btn in vlg.gameObject.transform.GetComponentsInChildren<Button>()){
				btn.enabled = true;
			}
		}
	}

	public void disableButton(){
		foreach (VerticalLayoutGroup vlg in home_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(Button btn in vlg.gameObject.transform.GetComponentsInChildren<Button>()){
				btn.enabled = false;
			}
		}
		foreach (VerticalLayoutGroup vlg in away_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(Button btn in vlg.gameObject.transform.GetComponentsInChildren<Button>()){
				btn.enabled = false;
			}
		}
	}

	public void swapOn(){
		foreach (VerticalLayoutGroup vlg in home_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(ButtonInitializerScript buttonScript in vlg.gameObject.transform.GetComponentsInChildren<ButtonInitializerScript>()){
				buttonScript.swapEnabled();
			}
		}
		foreach (VerticalLayoutGroup vlg in away_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(ButtonInitializerScript buttonScript in vlg.gameObject.transform.GetComponentsInChildren<ButtonInitializerScript>()){
				buttonScript.swapEnabled();
			}
		}
	}

	public void swapOff(){
		foreach (VerticalLayoutGroup vlg in home_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(ButtonInitializerScript buttonScript in vlg.gameObject.transform.GetComponentsInChildren<ButtonInitializerScript>()){
				buttonScript.swapDisabled();
			}
		}
		foreach (VerticalLayoutGroup vlg in away_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(ButtonInitializerScript buttonScript in vlg.gameObject.transform.GetComponentsInChildren<ButtonInitializerScript>()){
				buttonScript.swapDisabled();
			}
		}
	}

	//------------------FORMATIONLOCK------------------------------
	public void resetFormation(){
		foreach (VerticalLayoutGroup vlg in home_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			vlg.enabled = true;
		}
		foreach (VerticalLayoutGroup vlg in away_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			vlg.enabled = true;
		}
	}

	public void breakFormation(){
		foreach (VerticalLayoutGroup vlg in home_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			if(!vlg.gameObject.name.Equals("Substitution")){
				vlg.enabled = false;
			}
		}
		foreach (VerticalLayoutGroup vlg in away_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			if(!vlg.gameObject.name.Equals("Substitution")){
				vlg.enabled = false;
			}
		}
	}

	//----------------------DRAW MODE PALETTE------------------
	public void formationToPalette(){
		GameObject home_palette = transform.FindChild("Home Palette").FindChild("Content").gameObject;
		GameObject away_palette = transform.FindChild("Away Palette").FindChild("Content").gameObject;
		foreach (VerticalLayoutGroup vlg in home_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(Button btn in vlg.gameObject.transform.GetComponentsInChildren<Button>()){
				btn.transform.SetParent(home_palette.transform);
			}
		}
		foreach (VerticalLayoutGroup vlg in away_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(Button btn in vlg.gameObject.transform.GetComponentsInChildren<Button>()){
				btn.transform.SetParent(away_palette.transform);
			}
		}
	}

	public void paletteOpen(){
		clearDrawButton.Show();
		homescroll.Show();
		awayscroll.Show();
		//paletteBreak();
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Formation")){
			go.GetComponent<HideableUIScript>().Close();
		}
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Palette")){
			go.GetComponent<HideableUIScript>().Show();
		}
	}

	public void paletteClose(){
		clearDrawButton.Close();
		homescroll.Close();
		awayscroll.Close();
		paletteReset();
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Formation")){
			go.GetComponent<HideableUIScript>().Show();
		}
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Palette")){
			go.GetComponent<HideableUIScript>().Close();
		}
	}

	public void paletteBreak(){
		GameObject home_palette = transform.FindChild("Home Palette").FindChild("Scrollable").FindChild("Content").gameObject;
		GameObject away_palette = transform.FindChild("Away Palette").FindChild("Scrollable").FindChild("Content").gameObject;
		home_palette.GetComponent<VerticalLayoutGroup>().enabled = false;
		away_palette.GetComponent<VerticalLayoutGroup>().enabled = false;
	}

	public void paletteReset(){
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("PaletteItem")){
			PlayerAttribute pa = go.GetComponent<PlayerAttribute>();
			if(pa!=null){
				if(pa.leftteam){
					go.transform.SetParent(GameObject.Find("Home Palette").transform.FindChild("Scrollable").FindChild("Content"));
				}
				else{
					go.transform.SetParent(GameObject.Find("Away Palette").transform.FindChild("Scrollable").FindChild("Content"));
				}
			}
		}

		GameObject home_palette = transform.FindChild("Home Palette").FindChild("Scrollable").FindChild("Content").gameObject;
		GameObject away_palette = transform.FindChild("Away Palette").FindChild("Scrollable").FindChild("Content").gameObject;
		home_palette.GetComponent<VerticalLayoutGroup>().enabled = true;
		away_palette.GetComponent<VerticalLayoutGroup>().enabled = true;
	}

	//------------------HT/FT SIMULATION--------------------------
	public void newFormation(int[] homefor, int[] awayfor){
		GameObject homebench = home_formation.transform.FindChild("Bench").gameObject;
		GameObject awaybench = home_formation.transform.FindChild("Bench").gameObject;
		foreach(PlayerAttribute pa in home_formation.transform.FindChild("Defender").GetComponentsInChildren<PlayerAttribute>()){
			pa.transform.SetParent(homebench.transform);
		}
		foreach(PlayerAttribute pa in home_formation.transform.FindChild("Midfielder").GetComponentsInChildren<PlayerAttribute>()){
			pa.transform.SetParent(homebench.transform);
		}
		foreach(PlayerAttribute pa in home_formation.transform.FindChild("Forward").GetComponentsInChildren<PlayerAttribute>()){
			pa.transform.SetParent(homebench.transform);
		}
		for(int i=0;i<homefor[0];i++){
			homebench.transform.GetChild(0).SetParent(home_formation.transform.FindChild("Defender"));
		}
		for(int i=0;i<homefor[1];i++){
			homebench.transform.GetChild(0).SetParent(home_formation.transform.FindChild("Midfielder"));
		}
		for(int i=0;i<homefor[2];i++){
			homebench.transform.GetChild(0).SetParent(home_formation.transform.FindChild("Forward"));
		}

		foreach(PlayerAttribute pa in away_formation.transform.FindChild("Defender").GetComponentsInChildren<PlayerAttribute>()){
			pa.transform.SetParent(awaybench.transform);
		}
		foreach(PlayerAttribute pa in away_formation.transform.FindChild("Midfielder").GetComponentsInChildren<PlayerAttribute>()){
			pa.transform.SetParent(awaybench.transform);
		}
		foreach(PlayerAttribute pa in away_formation.transform.FindChild("Forward").GetComponentsInChildren<PlayerAttribute>()){
			pa.transform.SetParent(awaybench.transform);
		}
		for(int i=0;i<awayfor[0];i++){
			awaybench.transform.GetChild(0).SetParent(away_formation.transform.FindChild("Defender"));
		}
		for(int i=0;i<awayfor[1];i++){
			awaybench.transform.GetChild(0).SetParent(away_formation.transform.FindChild("Midfielder"));
		}
		for(int i=0;i<awayfor[2];i++){
			awaybench.transform.GetChild(0).SetParent(away_formation.transform.FindChild("Forward"));
		}
	}
	
	public void preMatchSim(){
		int[] homepmformation = new int[3] {HomeFormation[1], HomeFormation[2], HomeFormation[3]};
		int[] awaypmformation = new int[3] {AwayFormation[1], AwayFormation[2], AwayFormation[3]};
		newFormation(homepmformation,awaypmformation);
		ResetStatus();
	}

	public void halfTimeSim(){
		int[] homehtformation = new int[3] {4, 3, 3};
		int[] awayhtformation = new int[3] {3,5,2};
		newFormation(homehtformation,awayhtformation);
		ResetStatus();
		FindPlayer("SERGIO VAN DIJK").GetComponent<StatusIconController>().Goal();
		FindPlayer("IMANUEL WANGGAI").GetComponent<StatusIconController>().YellowCard();
		FindPlayer("MOUAIAD ALAJAAN").GetComponent<StatusIconController>().YellowCard();
		
		PermaSwap(FindPlayer("OUDAY ABDULJAFFAL"), FindPlayer("KHALED ALBRIJAWI"));
	}

	public void fullTimeSim(){
		int[] homeftformation = new int[3] {5, 3, 2};
		int[] awayftformation = new int[3] {3,4,3};
		newFormation(homeftformation,awayftformation);
		ResetStatus();
		FindPlayer("SERGIO VAN DIJK").GetComponent<StatusIconController>().Goal();
		FindPlayer("IMANUEL WANGGAI").GetComponent<StatusIconController>().YellowCard();
		FindPlayer("MOUAIAD ALAJAAN").GetComponent<StatusIconController>().YellowCard();

		PermaSwap(FindPlayer("BOAZ T. ERWIN SOLOSSA"), FindPlayer("CRISTIAN GONZALES"));
		PermaSwap(FindPlayer("IMANUEL WANGGAI"), FindPlayer("FIRMAN UTINA"));
		PermaSwap(FindPlayer("OUDAY ABDULJAFFAL"), FindPlayer("KHALED ALBRIJAWI"));
		PermaSwap(FindPlayer("MOUAIAD ALAJAAN"), FindPlayer("MAHMOUD ALYOUSSEF"));

		FindPlayer("KHALED ALBRIJAWI").GetComponent<StatusIconController>().YellowCard();
		FindPlayer("HAMDI ALMASSRI").GetComponent<StatusIconController>().RedCard();

	}

	
	public void ResetStatus(){
		foreach (VerticalLayoutGroup vlg in home_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(StatusIconController sic in vlg.gameObject.transform.GetComponentsInChildren<StatusIconController>()){
				sic.Reset();
			}
		}
		foreach (VerticalLayoutGroup vlg in away_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(StatusIconController sic in vlg.gameObject.transform.GetComponentsInChildren<StatusIconController>()){
				sic.Reset();
			}
		}
	}

	GameObject FindPlayer(string pl_name){
		foreach (VerticalLayoutGroup vlg in home_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(StatusIconController sic in vlg.gameObject.transform.GetComponentsInChildren<StatusIconController>()){
				if(sic.gameObject.name==pl_name){
					return sic.gameObject;
				}
			}
		}
		foreach (VerticalLayoutGroup vlg in away_formation.GetComponentsInChildren<VerticalLayoutGroup>()){
			foreach(StatusIconController sic in vlg.gameObject.transform.GetComponentsInChildren<StatusIconController>()){
				if(sic.gameObject.name==pl_name){
					return sic.gameObject;
				}
			}
		}
		return null;
	}

	void PermaSwap(GameObject subin, GameObject subout){
		GetComponent<SwapperScript>().Swap(subin,subout);
		subin.GetComponent<StatusIconController>().Subs();
		subout.GetComponent<StatusIconController>().Subs();
	}

}
