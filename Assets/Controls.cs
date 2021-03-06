﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public SteamVR_TrackedController TC;
    public float triggerAxis;
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    
	// Use this for initialization
	void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
	}
	
	// Update is called once per frame
	void Update () {
        triggerAxis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
    }
}
