using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ApplicationService : MonoBehaviour {

    private WWW x;
    private String results;
    private Application[] applications;

    public String Results
    {
        get
        {
            return results;
        }
    }

    // Use this for initialization
    void Start () {
        x = GET("http://singleuseapps.com/apps/api/v1/customers/60/projects/103/applications", processData);
    }

    // Update is called once per frame
    void Update () {
		
	}


    void processData()
    {
        Debug.Log(x.text);
        for (int i = 0; i < 5; i++)
        {
            Quaternion q = new Quaternion();
            // Instantiate(Cube,new Vector3(0,i,i+2),q);   
        }
    }

    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        try
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    private class Wrapper<T>
    {
        public T[] array;
    }

    private class Application
    {
        public long id { get; set; }
        public string filename { get; set; }
    }

    public WWW GET(string url, System.Action onComplete)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, onComplete));
        return www;
    }

    private IEnumerator WaitForRequest(WWW www, System.Action onComplete)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            results = www.text;
            applications = getJsonArray<Application>(www.text);

            foreach(Application application in applications)
            {
                Debug.Log(application.filename);
            }

            onComplete();
        }
        else
        {
            Debug.Log(www.error);
        }
    }
}
