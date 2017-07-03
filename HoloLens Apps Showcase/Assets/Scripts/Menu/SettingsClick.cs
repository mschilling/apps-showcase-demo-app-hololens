using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class SettingsClick : MonoBehaviour, IInputClickHandler
{

    public GameObject settingsMenu;

    // Use this for initialization
    void Start()
    {

    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Settings tapped");
      //  gameObject.transform.parent.gameObject.GetComponent<Renderer>().enabled = false;

        Instantiate(settingsMenu);
        // Go on
    }

    // Update is called once per frame
    void Update()
    {

    }
}
