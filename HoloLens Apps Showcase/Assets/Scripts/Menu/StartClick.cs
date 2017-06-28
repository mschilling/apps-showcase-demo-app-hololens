using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class StartClick : MonoBehaviour, IInputClickHandler
{

	// Use this for initialization
	void Start () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Start tapped");
        // Go on
    }

    // Update is called once per frame
    void Update () {
		
	}
}
