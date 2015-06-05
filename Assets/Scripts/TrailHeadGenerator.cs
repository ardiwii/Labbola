using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TrailHeadGenerator : MonoBehaviour {

	public Camera camera;
	public GameObject TrailHead;
	public GameObject activeTrail;
	public EventSystem eventsystem;
	public bool drawingmode;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_ANDROID
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
				Vector2 inputCoordinate = new Vector2(camera.ScreenToWorldPoint(Input.GetTouch(0).position).x,camera.ScreenToWorldPoint(Input.GetTouch(0).position).y);
				activeTrail = (GameObject) Instantiate(TrailHead,inputCoordinate,new Quaternion());
				activeTrail.transform.SetParent(transform);
			}
			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && activeTrail != null){
				AutoDestruct ad = activeTrail.GetComponent<AutoDestruct>();
				if(ad!=null){
					ad.time = 5f;
				}
				activeTrail.transform.position = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x,camera.ScreenToWorldPoint(Input.mousePosition).y);
			}
		#else
		if (Input.GetMouseButtonDown (0)) {
			Vector2 inputCoordinate = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x,camera.ScreenToWorldPoint(Input.mousePosition).y);
				//RaycastHit2D hitInfo = Physics2D.Raycast(inputCoordinate,Vector2.zero);
				//if(eventsystem.lastSelectedGameObject.tag=="Pitch")
				//eventsystem.
//				Debug.Log(eventsystem.lastSelectedGameObject.name);
//				if(drawingmode){
//					RaycastHit2D hitInfo = Physics2D.Raycast(inputCoordinate,Vector2.zero);
//				}
			activeTrail = (GameObject) Instantiate(TrailHead,inputCoordinate,new Quaternion());
			activeTrail.transform.SetParent(transform);
		}
		if(Input.GetMouseButton(0) && activeTrail != null){
			AutoDestruct ad = activeTrail.GetComponent<AutoDestruct>();
			if(ad!=null){
				ad.time = 5f;
			}
			activeTrail.transform.position = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x,camera.ScreenToWorldPoint(Input.mousePosition).y);
		}
		//if(Input
		#endif
	}
	
	public void Clear(){
		for (int i = transform.childCount-1; i>=0; i--){
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
