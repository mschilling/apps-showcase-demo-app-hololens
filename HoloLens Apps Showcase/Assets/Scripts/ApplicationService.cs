using System.Collections;
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

    private Customer[] customers = new Customer[3];                         // Customer array with initial size 3: Move4Mobile (12), Widlands (46), Rabobank (20)

    public JsonScript jsonScript;
    public GazeManager gazeManager;

   // public GameObject gazeMenu;

    // Use this for initialization
    void Start () {
        // Coders: 60 / 103
        // MyDial Ionic: 12 / 100
        // Bite:    12 / 92
        fillCustomers();
        getData();
    }

    // Update is called once per frame
    void Update () {
        if (isLoaded)
        {
           foreach(Customer c in customers)
            {
                foreach (Project p in c.projects)
                {
                    p.update();
                }
            }
        }
     }

    void getData()
    {
        jsonScript = new JsonScript();
        requests[0] = jsonScript.GET(Config.generateAppsurl(12, 92), processData);
        requests[1] = jsonScript.GET(Config.generateAppsurl(12, 100), processSecond);

        StartCoroutine(WaitForRequest(requests[0], processData));
        StartCoroutine(WaitForRequest(requests[1], processSecond));
    }

    void fillCustomers()
    {
        Customer m4m = new Customer();
        m4m.name = "Move4Mobile";
        Customer wildlands = new Customer();
        wildlands.name = "Wildlands";
        Customer rabo = new Customer();
        rabo.name = "Rabobank";

        customers[0] = m4m;
        customers[1] = wildlands;
        customers[2] = rabo;
    }

    void fillProjects()
    {
        // M4M Projects
        Project[] m4mprojects = new Project[3];

        Project bite = new Project();
        bite.name = "Bite";
        Project coders = new Project();
        coders.name = "Coders";
        Project mydialionic = new Project();
        mydialionic.name = "MyDialogues Ionic";

        m4mprojects[0] = bite;
        m4mprojects[1] = coders;
        m4mprojects[2] = mydialionic;

        customers[0].projects = m4mprojects;

        // Rabo Projects
        Project[] raboProjects = new Project[1];

        Project smartpin = new Project();
        smartpin.name = "Rabo SmartPin";
        raboProjects[0] = smartpin;

        customers[1].projects = raboProjects;

        // Wildlands 
        Project[] wildlandProjects = new Project[1];
        Project wildlands = new Project();
        wildlands.name = "Wildlands";
        wildlandProjects[0] = wildlands;
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
            treintjes[i + 1].SetActive(true);
        }
        isLoaded = true;
    }

    private IEnumerator WaitForRequest(WWW www, System.Action onComplete)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("Success");
            results = www.text;
            //applications = getJsonArray<Application>(www.text);

            //foreach(Application application in applications)
            //{
            //    Debug.Log(application.filename);
            //}

            onComplete();
        }
        else
        {

            Debug.Log("Failure");
            Debug.Log(www.error);
        }
    }

}
