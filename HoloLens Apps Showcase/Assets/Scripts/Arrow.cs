using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private SpaceCollectionManager manager;
    private GameObject spatial;

    private GameObject train;

	// Use this for initialization
	void Start () {

        spatial = GameObject.FindGameObjectWithTag("Spatial");
        manager = spatial.GetComponent<SpaceCollectionManager>();

        gameObject.GetComponent<Renderer>().enabled = false;
        


    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.2f, 0.8f);
        pos.y = Mathf.Clamp(pos.y, 0.2f, 0.8f);


        //gameObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position);
        gameObject.transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (train != null)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.transform.LookAt(train.transform);
            gameObject.transform.Rotate(0, 180, 0);
        }
        else
        {
            if (manager.getLocos() != null && manager.getLocos().Count > 0)
            { 
            train = manager.getLocos().ToArray()[0];
        }
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
