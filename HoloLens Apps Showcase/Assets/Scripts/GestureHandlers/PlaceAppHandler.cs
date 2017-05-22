﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;



public class PlaceAppHandler : MonoBehaviour, IInputClickHandler
{

    public ApplicationService applicationService;
   
    public void OnInputClicked(InputClickedEventData eventData)
    {
       Debug.Log("Place app tapped");
       applicationService.placeApp();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
