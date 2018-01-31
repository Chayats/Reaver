using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public GameObject home;
    Vector3 grabbedPosition;
    Vector3 grabbedRotation;
    Transform homeParent;
    bool isHeld;
    bool istriggerpull;
    ViveGrip_GripPoint gp;
    public AudioSource gunshot;
    public GameObject barrel;
    SteamVR_TrackedObject hand;
    SteamVR_Controller.Device device;
    GameObject bob;
    public Shoot shooter;

    // Use this for initialization
    void Start () {
       
        homeParent = this.gameObject.transform.parent;
        grabbedPosition = new Vector3(-0f, -0.2013f, -0.0526f);
        grabbedRotation = new Vector3(40.62f, 0f, -90f);
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

    void Update() {

        if(!(gp == null))
        {
            if (gp.GetComponentInParent<Controls>().triggerAxis == 1)
            {
                istriggerpull = true;
            }
            else
            {
                istriggerpull = false;
            }
        }



        if (isHeld && istriggerpull)
            shooter.Bang(device);


    }


}
