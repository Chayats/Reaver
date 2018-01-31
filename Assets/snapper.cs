using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapper : MonoBehaviour {
    // to be attached to objects that snap to a hardpoint upon being released

    /* psudo code:
    get the child hardpoint
    work out where that hardpoint is in relation to the local origin
if the local hardpoint overlapping another hardpoint we can snap to
 if it is make that hardpoint blink or somthing
 if I'm let go now make my position to be such that my hardpoint and my connected hardpoint are in the same location
 additionaly make me a child of the parent of the hardpoint I'm attaching to
 */
    public hardpoint childHardpoint;
    public hardpoint anchor;
    public bool isheld;
    public Vector3 hardpointOffset;
    public bool issnapped;


    void Start()
    {
        childHardpoint = GetComponentInChildren<hardpoint>();
        hardpointOffset = childHardpoint.transform.localPosition;
       
}
    void ViveGripGrabStart(ViveGrip_GripPoint gripPoint)
    {
        isheld = true;
    }
    void ViveGripGrabStop(ViveGrip_GripPoint gripPoint)
    {
        isheld = false;
    }
    void FixedUpdate() {
        if (childHardpoint.IsColliding&& !isheld)
        {
            transform.parent = childHardpoint.Collidewith.gameObject.transform;
            transform.localPosition = -hardpointOffset;
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            issnapped = true;
        }
        if (issnapped) {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        if (!issnapped)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
        if (isheld)
        {
            transform.parent = null;
            issnapped = false;
        }
    }


}
