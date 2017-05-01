using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ApplicationService : MonoBehaviour {

    private WWW x;
    private string results;
    //private Application[] applications;

    public GameObject coupe;

    public String Results
    {
        get
        {
            return results;
        }
    }

    // Use this for initialization
    void Start () {
         x = GET("http://singleuseapps.com:8080/apps/api/v1/customers/12/projects/92/applications", processData);
        // x = GET("https://jsonplaceholder.typicode.com/users", processData);

        Quaternion quat = new Quaternion();

        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(-0.5f*i-0.5f, 0.04f, 0);
            Instantiate(coupe, pos,quat);
        }        
    }

    // Update is called once per frame
    void Update () {
        Vector3 r = new Vector3(-0.01f, 0, 0);
        coupe.transform.TransformVector(r);
	}


    void processData()
    {
        Debug.Log(x.text);
        AppObject[] apps = getJsonArray(x.text);
       
    }

    [Serializable]
    public class AppObject
    {
        public long id;
        public long projectid;
        public long uploaddate;
        public string version;
        public string platform;
        public string environment;
        public string filename;
        public string path;


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
