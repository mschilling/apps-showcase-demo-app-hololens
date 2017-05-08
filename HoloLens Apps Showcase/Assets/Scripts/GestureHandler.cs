using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHandler {
    private bool isActive = false;

    void Update()
    {
        if (isActive)
        {
            //this.transform.Rotate(0, 1, 0);
            Debug.Log("Clicked this object");
        }
    }

    public void OnAirTapped()
    {
        isActive = !isActive;
    }

}
