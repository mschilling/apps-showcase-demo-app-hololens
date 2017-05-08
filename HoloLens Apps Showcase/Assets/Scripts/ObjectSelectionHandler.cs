using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ObjectSelectionHandler : MonoBehaviour,IInputClickHandler {

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }


    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Train tapped");
    }


	
}
