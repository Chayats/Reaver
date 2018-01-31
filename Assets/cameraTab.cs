using UnityEngine;
using System.Collections;

public class cameraTab : MonoBehaviour {
    public Camera cam;

    public bool big = false;

	// Use this for initialization
	void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space"))
        {
            big = !big;
        }

            if (big)
        {
            cam.rect = new Rect(0f, 0f, 1f, 1f);
        }
        else
        {
            cam.rect = new Rect(0f, 0f, .2f, .2f);
        }
	}
}
