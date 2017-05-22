using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class OverlayGaze : MonoBehaviour {

    private GazeManager gazeManager;
    public GameObject inputManager;
    public GameObject mainCamera;

    public GameObject gazeMenu;
    public ApplicationService applicationService;

	// Use this for initialization
	void Start ()
    {
        Debug.Log("Gazemanager: " + (gazeManager != null));
        inputManager = GameObject.FindGameObjectWithTag("InputManager");
        gazeManager = inputManager.GetComponent<GazeManager>();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        applicationService = mainCamera.GetComponent<ApplicationService>();

        Debug.Log("Gazemanager: " + (gazeManager != null));
	}
	
	// Update is called once per frame
	void Update () {
        if (gazeManager.IsGazingAtObject)
        {
            GameObject focussed = gazeManager.HitObject;
            if (focussed == gameObject)
            {
                Debug.Log("Focussed");
                Vector3 pos = gameObject.transform.position;
                pos.z = pos.z + 0.01f;
                Quaternion q = gameObject.transform.rotation;
                Instantiate(gazeMenu, pos,q);

                applicationService.setGazedObject(gameObject);
            }
        }
    }
}
