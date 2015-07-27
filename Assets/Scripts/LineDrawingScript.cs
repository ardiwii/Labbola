using UnityEngine;
using System.Collections;

public class LineDrawingScript : MonoBehaviour {

	public Camera camera;
	public GameObject TrailHead;
	public GameObject activeTrail;
	public bool active;
	float rotatespeed;

	// Use this for initialization
	void Start () {
		rotatespeed = 10f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startDrag(){
		if(active){
			Vector2 inputCoordinate = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x,camera.ScreenToWorldPoint(Input.mousePosition).y);
			activeTrail = (GameObject) Instantiate(TrailHead,inputCoordinate,new Quaternion());
			activeTrail.transform.SetParent(transform);
		}
	}
	
	public void dragging(){
		if(active){
			if(activeTrail!=null){
				AutoDestruct ad = activeTrail.GetComponent<AutoDestruct>();
				if(ad!=null){
					ad.time = 5f;
				}
				activeTrail.transform.position = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x,camera.ScreenToWorldPoint(Input.mousePosition).y);
				activeTrail.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0), Time.deltaTime*rotatespeed);
			}
		}
	}

	public void toggle_activation(bool activation){
		active = activation;
	}

	public void Clear(){
		for (int i = transform.childCount-1; i>=0; i--){
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
