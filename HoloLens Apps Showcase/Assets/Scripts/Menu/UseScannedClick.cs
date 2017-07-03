using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;


public class UseScannedClick : MonoBehaviour, IInputClickHandler
{

    public GameObject fillBox;
    private bool isActive = false;

	// Use this for initialization
	void Start () {
        fillBox.SetActive(isActive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("UseScanned tapped");
        isActive = !isActive;

        fillBox.SetActive(isActive);

        
    }
}
