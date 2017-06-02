using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class OverlayManager : Singleton<OverlayManager> {

    private GameObject currentOverlay;

    public void changeOverlay(GameObject overlay)
    {
        if(currentOverlay != null)
        {
            Destroy(currentOverlay);
        }
        currentOverlay = overlay;
    }
	
}
