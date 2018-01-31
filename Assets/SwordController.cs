using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {
    public GameObject home;
    Vector3 grabbedPosition;
    Vector3 grabbedRotation;
    Transform homeParent;
    bool isHeld;
    ViveGrip_GripPoint gp;
    SteamVR_TrackedObject hand;
    SteamVR_Controller.Device device;
    GameObject bob;


    // Use this for initialization
    void Start () {
       
        homeParent = this.gameObject.transform.parent;

        grabbedPosition = new Vector3(-0f, -0.03f, -0.03f);
        grabbedRotation = new Vector3(91.1f, 0f, 90f);
    }

    void ViveGripInteractionStart(ViveGrip_GripPoint _gp)
    {
        gp = _gp;
        hand = gp.GetComponentInParent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)hand.index);
        device.TriggerHapticPulse(500);
        this.gameObject.transform.parent = gp.transform;
        this.gameObject.transform.localPosition =  grabbedPosition;
        this.gameObject.transform.localEulerAngles = grabbedRotation;
        bob = gp.transform.parent.Find("Model").gameObject;
        bob.SetActive(false) ;
        isHeld = true;

    }
    void ViveGripInteractionStop(ViveGrip_GripPoint _gp)
    {
        bob.SetActive(true);
        this.gameObject.transform.parent = homeParent;
        this.gameObject.transform.position = home.transform.position;
        this.gameObject.transform.rotation = home.transform.rotation;
        isHeld = false;

    }

   
}
