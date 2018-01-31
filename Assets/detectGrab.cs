using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectGrab : MonoBehaviour {

    public  SteamVR_TrackedController leftdevice;
    public  SteamVR_TrackedController rightdevice;
    public HoverCarControl HCC;
    public bool onLeft;
    public bool onRight;

    void Start()
    {
      //  leftdevice = GameObject.Find("Controller(left)").GetComponent<SteamVR_TrackedController>();
      //  rightdevice = GameObject.Find("Controller(right)").GetComponent<SteamVR_TrackedController>();
    }
  

       void ViveGripInteractionStart(ViveGrip_GripPoint gp)
        {
            if (this.name == "lefthandles")
            {
                HCC.leftGripped = true;
            onLeft = true;
            }
            if (this.name == "righthandles")
            {
                HCC.rightGripped = true;
            onRight = true;
            }
        }

    void Update()
    {
            if (this.name == "lefthandles"&& leftdevice.gripped==false && onLeft == true)
            {
                HCC.leftGripped = false;
            onLeft = false;
            }
            if (this.name == "righthandles" && rightdevice.gripped == false&& onRight == true)
            {
                HCC.rightGripped = false;
            onRight = false;
            }
        }
    
   
}
