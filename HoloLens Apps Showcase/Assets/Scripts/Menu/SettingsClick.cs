using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class SettingsClick : MonoBehaviour, IInputClickHandler
{

    // Use this for initialization
    void Start()
    {

    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Settings tapped");
        // Go on
    }

    // Update is called once per frame
    void Update()
    {

    }
}
