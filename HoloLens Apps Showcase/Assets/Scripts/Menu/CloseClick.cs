using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;



public class CloseClick : MonoBehaviour, IInputClickHandler
{

    // Use this for initialization
    void Start()
    {

    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Close tapped");

      

        gameObject.transform.parent.gameObject.SetActive(false);

        // Go on
    }

    // Update is called once per frame
    void Update()
    {

    }
}

