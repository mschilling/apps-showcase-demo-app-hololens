using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ApplicationService : MonoBehaviour {

    private WWW[] requests = new WWW[5];
    public GameObject coupe;                                                // Used to generate the projects from code
    public GameObject train;                                                // Public so object can be added from Unity Scene and used to generate objects here

    private GameObject[] locos = new GameObject[3];                          // First car of each train

    private GameObject[][] treintjes = new GameObject[3][];
    private bool isLoaded = false;                                          // When isLoaded animation starts

    private Customer[] customers = new Customer[3];                         // Customer array with initial size 3: Move4Mobile (12), Widlands (46), Rabobank (20)

    public JsonScript jsonScript;                                           // Script used to retrieve data via REST

    private GameObject gazedObject = null;                                  // Hold reference to gazedObject to use for devicewall placement

    private GameObject currentOverlay;


    // Use this for initialization
    void Start () {
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

    public GameObject getTrain()
    {
        return train;
    }

    void getData()
    {
        // Coders: 60 / 103
        // MyDial Ionic: 12 / 100
        // Bite:    12 / 92
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


        // Create 3 trains
        GameObject spatial = GameObject.FindGameObjectWithTag("Spatial");
        SpaceCollectionManager manager = spatial.GetComponent<SpaceCollectionManager>();
        locos = manager.getLocos().ToArray();

        fillProjects();
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

        customers[2].projects = wildlandProjects;
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
        switch (index)
        {
            case 0:
                customers[0].projects[0].apps = apps;
                treintjes[0] = new GameObject[apps.Length]; // +1 for loco

                break;
            case 1:
                customers[0].projects[2].apps = apps;
                break;

            default:
                break;
        }

    }

    public void spatialMappingCompleted()
    {
        GameObject spatial = GameObject.FindGameObjectWithTag("Spatial");
        SpaceCollectionManager manager = spatial.GetComponent<SpaceCollectionManager>();
        locos = manager.getLocos().ToArray();

        Vector3 posLoco = locos[0].transform.position;

        Quaternion quat = locos[0].transform.rotation;
        for (int i = 0; i < treintjes[0].Length; i++)
        {
            Vector3 pos = new Vector3(-0.5f * i - 0.5f + posLoco.x, 0.04f + posLoco.y, 0 + posLoco.z);
            treintjes[0][i] = Instantiate(coupe, pos, quat);
        }
        isLoaded = true;

    }
    public void setGazedObject(GameObject gazeObject)
    {
        Debug.Log("GazeObject changed at ApplicationService");
        gazedObject = gazeObject;
    }

    public Project getProjectByGameObject(GameObject gameObject)
    {
        foreach (Customer c in customers)
        {
            foreach (Project p in c.projects)
            {
                if (p.coupe == gazedObject)
                {
                    return p;
                }
            }
        }
        return null;
    }

    public void placeApp()
    {
        Project p = getProjectByGameObject(gazedObject);
                if(p !=null)
                {
                    // place app
                    requests[2] = jsonScript.GET(Config.deviceApiUrl, placeAppCallBack);
                    StartCoroutine(WaitForRequest(requests[2], placeAppCallBack));
                }
         
    }

    void placeAppCallBack()
    {
        Application.OpenURL("http://192.168.13.61/7100");
        // feedback to user app is placed;
    }


    public void changeOverlay(GameObject overlay)
    {
        if (currentOverlay != null)
        {
            Destroy(currentOverlay);
        }
        currentOverlay = overlay;
    }

    private IEnumerator WaitForRequest(WWW www, System.Action onComplete)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log("Success");
            onComplete();
        }
        else
        {
            Debug.Log("Failure");
            Debug.Log(www.error);
        }
    }

}
