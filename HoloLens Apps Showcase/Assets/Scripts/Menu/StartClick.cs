using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class StartClick : MonoBehaviour, IInputClickHandler
{

    public GameObject spatial;

	// Use this for initialization
	void Start () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Start tapped");
        // Go on
        Instantiate(spatial);
        Destroy(gameObject.transform.parent.gameObject);

        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        ApplicationService applicationService = mainCamera.GetComponent<ApplicationService>();
        applicationService.getLocos();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
