﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ApplicationService : MonoBehaviour {

    private WWW[] requests = new WWW[5];
    private bool[] completed = new bool[5];
    private string results;

    public GameObject coupe;
    public GameObject train;

    private GameObject[] treintjes = new GameObject[6];
    private bool isLoaded = false;                                          // When isLoaded animation starts

    private Customer[] customers = new Customer[0];                         // Customer array with initial size 0
    private Project[] projects = new Project[0];

    public JsonScript jsonScript; 

    // Use this for initialization
    void Start () {
        // Coders: 60 / 103
        // MyDial Ionic: 12 / 100
        // Bite:    12 / 92
         requests[0] = jsonScript.GET(Config.generateAppsurl(12,92), processData);
         requests[1] = jsonScript.GET(Config.generateAppsurl(12,100), processSecond);
    }

    // Update is called once per frame
    void Update () {
        if (isLoaded)
        {
           foreach(Project p in projects)
            {
                p.update();
            }
        }
     }


    void processData()
    {
        startProcess(0);
    }

    void processSecond()
    {
        startProcess(1);
    }


    void startProcess(int index)
    {
        Debug.Log(requests[index].text);
        AppObject[] apps = jsonScript.getJsonArray(requests[index].text);

        treintjes = new GameObject[apps.Length + 1]; // +1 for loco

        Quaternion quat = new Quaternion();
        Vector3 posTrain = new Vector3(0, 0, 0);
        treintjes[0] = train;                       // Loco in the front

        for (int i = 0; i < apps.Length; i++)
        {
            Vector3 pos = new Vector3(-0.5f * i - 0.5f, 0.04f, 0);
            treintjes[i + 1] = Instantiate(coupe, pos, quat);
        }
        isLoaded = true;
    }

}
