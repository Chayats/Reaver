using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hardpoint : MonoBehaviour {

    public bool IsColliding;
    public hardpoint Collidewith;
    public Vector3 hardpointOffset;


    void Start() {
        hardpointOffset = transform.localPosition;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hardpoint")
        {
            IsColliding = true;
            Collidewith = other.GetComponent<hardpoint>();
        }

    }
    void OnTriggerExit() {
        IsColliding = false;
        Collidewith = null;
    }

}
