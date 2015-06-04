using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectColor : MonoBehaviour {

	void OnSetColor(Color color)
	{
		//Material mt = new Material(GetComponent<Renderer>().sharedMaterial);
		GetComponent<Image>().color = color;
		//GetComponent<Renderer>().material = mt;
	}

	void OnGetColor(ColorPicker picker)
	{
		picker.NotifyColor(GetComponent<Image>().color);
	}
}
