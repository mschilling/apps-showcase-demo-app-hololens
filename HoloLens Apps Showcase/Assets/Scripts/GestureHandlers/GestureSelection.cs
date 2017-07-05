using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using System;

public class GestureSelection : MonoBehaviour, IInputClickHandler {

    private PlaySpaceManager playSpaceManager;
    public InputManager input;
    
    // Use this for initialization
    void Start () {
        input.AddGlobalListener(gameObject);
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        ApplicationService applicationService = mainCamera.GetComponent<ApplicationService>();
        if(applicationService.currentScene == ApplicationService.SCENE.Overview)
        {
            playSpaceManager = GameObject.FindGameObjectWithTag("Spatial").GetComponent<PlaySpaceManager>();
            Debug.Log("Clicked!");

            if (!playSpaceManager.finishedScanning())
            {
                playSpaceManager.finishScanning();
            }
            TextToSpeechManager textToSpeechManager = GameObject.FindGameObjectWithTag("Speech").GetComponent<TextToSpeechManager>();
            textToSpeechManager.SpeakText(new TextUtil().scanningCompleted);
        }

       
    }
}
