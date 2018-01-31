using UnityEngine;
using System.Collections;

public class reaverControler : MonoBehaviour {

    GameObject baseCart;
    Rigidbody rb;
    public Vector3 offset;
    public Vector3 basepos;
    public Vector3 baserotates;
    public Vector3 oldpos;
    public float currentVelocity;
    public float lastFrameVelocity;
    public float Gforce;
    public float turnamt;
    public float thrustamt;
    public HoverCarControl hcc;
    public float bankLimitRaw = .3f;
    public float bankLimit;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        baseCart = GameObject.FindGameObjectWithTag("Player") as GameObject;
    offset = transform.position - baseCart.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bankLimit = bankLimitRaw - Mathf.Abs(hcc.aclAxis);
        basepos = baseCart.transform.position;
        baserotates = baseCart.transform.rotation.eulerAngles;
        turnamt = hcc.m_currTurn;
        thrustamt = hcc.m_currThrust;

      

    }

	void FixedUpdate () {
        transform.position = basepos + offset;
        oldpos = transform.localEulerAngles;
        float tempZ = oldpos.z;
        if (tempZ > 180f)
        {
            float negs = 360 - tempZ;
            negs = negs * bankLimit;
            tempZ = tempZ + negs;
        }
        else
        {
            float negs = tempZ;
            negs = negs * bankLimit;
            tempZ = tempZ - negs;
        }


        transform.localEulerAngles = new Vector3(0, baserotates.y, tempZ);
        rb.AddRelativeTorque(Vector3.forward * -hcc.m_currTurn *200);
        Gforce = ((hcc.m_currTurn * hcc.aclAxis) * 13f );
    }
}
