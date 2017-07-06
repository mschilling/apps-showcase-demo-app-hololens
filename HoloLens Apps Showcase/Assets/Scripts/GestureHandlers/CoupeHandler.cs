using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;


public class CoupeHandler : MonoBehaviour, IInputClickHandler
{

    private bool isDriving = false;
    public GameObject[] allOverlays;

    public GameObject wallOverlay;

    private SpaceCollectionManager manager;

    private void Start()
    {
        GameObject spatial = GameObject.FindGameObjectWithTag("Spatial");
        manager = spatial.GetComponent<SpaceCollectionManager>();
    }

    private void Update()
    {
        if (isDriving)
        {
            gameObject.transform.Translate(0.050f, 0, 0);
            findPath(gameObject);
        }
    }


    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Coupe tapped");
        Debug.Log(allOverlays.Length);
        List<GameObject> overlays = new List<GameObject>();
        foreach(GameObject overlay in allOverlays)
        {
            overlays.Add(overlay);
        }
        //List<GameObject> overlays = new List<GameObject>();
        //overlays.Add(wallOverlay);
        //overlays.Add(wallOverlay);
        //overlays.Add(wallOverlay);

        manager.GenerateWallScreen(overlays);

        TextToSpeechManager textToSpeechManager = GameObject.FindGameObjectWithTag("Speech").GetComponent<TextToSpeechManager>();
        textToSpeechManager.SpeakText("Apps placed");

        // Show all overlays
    }

    void findPath(GameObject coupe)
    {
        var pos = coupe.transform.position;
        if (pos.x > 4 || pos.x < -4)
        {
            coupe.transform.Rotate(0, 2f, 0);
        }
    }



}
