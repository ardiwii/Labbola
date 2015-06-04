using UnityEngine;
using System.Collections;

public class LineDrawingScript : MonoBehaviour {

	public Camera camera;
	public GameObject TrailHead;
	public GameObject activeTrail;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startDrag(){
		Vector2 inputCoordinate = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x,camera.ScreenToWorldPoint(Input.mousePosition).y);
		activeTrail = (GameObject) Instantiate(TrailHead,inputCoordinate,new Quaternion());
		activeTrail.transform.SetParent(transform);
	}
	
	public void dragging(){
		if(activeTrail!=null){
			AutoDestruct ad = activeTrail.GetComponent<AutoDestruct>();
			if(ad!=null){
				ad.time = 5f;
			}
			activeTrail.transform.position = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x,camera.ScreenToWorldPoint(Input.mousePosition).y);
		}
	}

	public void Clear(){
		for (int i = transform.childCount-1; i>=0; i--){
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
