using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldStateScript : MonoBehaviour {

	static WorldStateScript WorldState;
	public enum mode {Preview,Grab,Draw};
	public mode curMode;
	public enum gameTime {PreMatch,HalfTime,FullTime};
	//public List<GameObject> Players;

	public void start(){
		curMode = mode.Preview;
	}

	public void setMode(int modenum){
		switch(modenum){
		case 1: curMode = mode.Preview; break;
		case 2: curMode = mode.Grab; break;
		case 3: curMode = mode.Draw; break;
		}
	}
}
