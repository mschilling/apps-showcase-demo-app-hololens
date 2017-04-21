using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ApplicationService : MonoBehaviour {

    private WWW x;
    private string results;
    //private Application[] applications;

    public GameObject train;

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
            Vector3 pos = new Vector3(0, 0.25f, i*0.5f);
            Instantiate(train, pos,quat);
        }


        Instantiate(train);
        
    }

    // Update is called once per frame
    void Update () {
		
	}


    void processData()
    {
        Debug.Log(x.text);
        AppObj[] data = getJsonArray<AppObj>(x.text);


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
        public T[] array { get; set; }
    }

    public struct AppObj
    {
        public long id { get; set; }
        public long projectid { get; set; }
        public long uploaddate { get; set; }
        public string version { get; set; }
        public string platform { get; set; }
        public string environment { get; set; }
        public string filename { get; set; }
        public string path { get; set; }



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
