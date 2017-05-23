using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GestureSelection : MonoBehaviour, IInputClickHandler {

    public PlaySpaceManager playSpaceManager;
    public InputManager input;
    
    // Use this for initialization
    void Start () {
        input.AddGlobalListener(gameObject);
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Clicked!");

        if (!playSpaceManager.finishedScanning())
        {
            playSpaceManager.finishScanning();
        }
    }
}
