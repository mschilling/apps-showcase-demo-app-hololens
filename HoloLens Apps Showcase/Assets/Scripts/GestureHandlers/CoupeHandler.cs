using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class CoupeHandler : MonoBehaviour, IInputClickHandler
{

    private bool isDriving = false;
    public GameObject overlay;

    private void Start()
    {
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
        isDriving = !isDriving;
        overlay.SetActive(!isDriving);

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
