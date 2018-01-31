using UnityEngine;
using System.Collections;

public class speedo : MonoBehaviour {
    public HoverCarControl car;
    public float speed;
    public float needleturn;
    public float maxSpeed =300;
    public float maxDegrees = 210;
    public float zeroPosition = 24;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        speed = car.speed;
        needleturn = ((speed/maxSpeed)*maxDegrees)+ zeroPosition; 

        transform.localEulerAngles = new Vector3(0, 0, -needleturn);

    }
}
