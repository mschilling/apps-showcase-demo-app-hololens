using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

public class OverlayGaze : MonoBehaviour{

    private GazeManager gazeManager;
    public GameObject inputManager;
    public GameObject mainCamera;

    public GameObject gazeMenu;
    public ApplicationService applicationService;

    private GameObject currentOverlay;

	// Use this for initialization
	void Start ()
    {
        inputManager = GameObject.FindGameObjectWithTag("InputManager");
        gazeManager = inputManager.GetComponent<GazeManager>();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        applicationService = mainCamera.GetComponent<ApplicationService>();

	}
	
	// Update is called once per frame
	void Update () {
        if (gazeManager.IsGazingAtObject)
        {
            GameObject focussed = gazeManager.HitObject;
            if (focussed == gameObject && applicationService.getGaze() != focussed)
            {
                Vector3 pos = gameObject.transform.position;
                Quaternion q = Quaternion.LookRotation(gameObject.transform.forward, Vector3.up);
                GameObject gaze = Instantiate(gazeMenu, pos, q);


                // Change overlay and gazed object
                applicationService.changeOverlay(gaze);
                applicationService.setGazedObject(gameObject);


                Project project = applicationService.getProjectByGameObject(gameObject);
                TextMesh[] texts = gaze.GetComponentsInChildren<TextMesh>();
                if (texts != null && texts.Length > 0 && project != null)
                {
                    texts[0].text = project.name;
                }

            }
        }
    }

}
