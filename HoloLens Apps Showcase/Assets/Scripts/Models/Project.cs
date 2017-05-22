using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Project{

    public string name;
    public float originalX;
    public float x, y, z;
    public GameObject coupe;
    public AppObject[] apps;


    public void update()
    {

        coupe.transform.Translate(0.050f, 0, 0);
        findPath();
    }

    public void findPath()
    {
        var pos = coupe.transform.position;
        if (pos.x > 4 + originalX || pos.x < -4 + originalX)
        {
            coupe.transform.Rotate(0, 2f, 0);
        }
    }

}
