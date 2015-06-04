using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragParentScript : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler{
	
	public Camera camera;
	
	// Use this for initialization
	void Start () {
		camera = GameObject.Find("Main Camera").GetComponent<Camera>()	;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		//GetComponent<RectTransform>().pivot = Input.mousePosition;
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		transform.parent.parent.position = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x,camera.ScreenToWorldPoint(Input.mousePosition).y);
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		
	}
	
	public void OnDrop(PointerEventData eventData)
	{
		
	}
}
