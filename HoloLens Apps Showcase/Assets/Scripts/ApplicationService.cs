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

    private Customer[] customers = new Customer[0];                         // Customer array with initial size 0

    // Use this for initialization
    void Start () {
        // Coders: 60 / 103
        // MyDial Ionic: 12 / 100
        // Bite:    12 / 92
    
         requests[0] = GET(Config.generateAppsurl(12,92), processData);
         requests[1] = GET(Config.generateAppsurl(12,100), processSecond);
      //  WWW y = GET(deviceApiUrl, processData);
    }

    // Update is called once per frame
    void Update () {
        if (isLoaded)
        {
            foreach (GameObject coupe in treintjes)
            {
                coupe.transform.Translate(0.050f, 0, 0);
                findPath(coupe);
            }
        }
     }

    void findPath(GameObject coupe)
    {
        var pos = coupe.transform.position;
        if(pos.x > 4 || pos.x < -4)
        {
            coupe.transform.Rotate(0, 2f, 0);
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
        AppObject[] apps = getJsonArray(requests[index].text);

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

  

    public static AppObject[] getJsonArray(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        try
        {
            AppWrapper wrapper = JsonUtility.FromJson<AppWrapper>(newJson);
            return wrapper.array;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    public class Wrapper<T>
    {
        public T[] array;
    }

    [Serializable]
    public class AppWrapper : Wrapper<AppObject>
    {

    }



   
    public WWW GET(string url, System.Action onComplete)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "los4kss5kl3b73pn8evhiieg2c");
        WWW www = new WWW(url,null,headers);
        StartCoroutine(WaitForRequest(www, onComplete));
        return www;
    }

    public WWW POST(string url, Dictionary<string, string> post, System.Action onComplete)
    {
        WWWForm form = new WWWForm();
       

        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www, onComplete));
        return www;
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
