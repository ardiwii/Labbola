﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SimpleDragableScript : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public Camera camera;
	public bool swappable;
	float dragtime;
	SwapperScript swapscr;
	
	// Use this for initialization
	void Start () {
		camera = Camera.main;
		dragtime = 0f;
		swapscr = GameObject.FindGameObjectWithTag("Pitch").GetComponent<SwapperScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		if(gameObject.tag=="PaletteItem"){
			gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Pitch").transform);
			PlayerAttribute plattrib = GetComponent<PlayerAttribute>();
			if(plattrib!=null){
				plattrib.updateNameView();
			}
		}
		dragtime = 0f;
		Debug.Log("begin drag, "+dragtime);
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		transform.position = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x,camera.ScreenToWorldPoint(Input.mousePosition).y);
		//		dragtime+=Time.deltaTime;
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		swapscr.Cancel(gameObject);
		Debug.Log("end drag, "+dragtime);
		//		if(dragtime<0.6f){
		//			swapscr.Select(gameObject);
		//		}
	}
	
	public void OnDrop(PointerEventData eventData)
	{
		
	}
}