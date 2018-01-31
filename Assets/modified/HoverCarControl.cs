using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class HoverCarControl : MonoBehaviour
{
  Rigidbody m_body;
  float m_deadZone = 0.2f;

  public float m_hoverForce = 9.0f;
  public float m_hoverHeight = 2.0f;
  public GameObject[] m_hoverPoints;
    public bool VR=true;
  public float m_forwardAcl = 100.0f;
  public float m_backwardAcl = 25.0f;
    public float speed;
  public float m_currThrust = 0.0f;

  public float m_turnStrength = 10f;
  public float m_currTurn = 0.0f;

  public GameObject m_leftAirBrake;
  public GameObject m_rightAirBrake;
  public float bankLimit = 45.0f;
    float yCoord = 1;
    public float noise;
    public float turn;
    public float thrust;
    public Controls leftControl;
    public Controls rightControl;
    int m_layerMask;
    
    public float currentVelocity;
    public float lastFrameVelocity;
    public float Gforce;
    public float aclAxis;
    public float turnAxis;
    public Slider SliderSpeed;
    public Slider TurnSpeed;
    public bool leftGripped = false;
    public bool rightGripped = false;
    public float lastleftinput;
    public float lastrightinput;




    void Start()
  {
    m_body = GetComponent<Rigidbody>();

    m_layerMask = 1 << LayerMask.NameToLayer("car");
    m_layerMask = ~m_layerMask;
  }

  void OnDrawGizmos()
  {

    //  Hover Force
    RaycastHit hit;
    for (int i = 0; i < m_hoverPoints.Length; i++)
    {
      var hoverPoint = m_hoverPoints [i];
      if (Physics.Raycast(hoverPoint.transform.position, 
                          -Vector3.up, out hit,
                          m_hoverHeight, 
                          m_layerMask))
      {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(hoverPoint.transform.position, hit.point);
        Gizmos.DrawSphere(hit.point, 0.5f);
      } else
      {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(hoverPoint.transform.position, 
                       hoverPoint.transform.position - Vector3.up * m_hoverHeight);
      }
    }
  }
	
  void Update()
  {
        m_forwardAcl = SliderSpeed.value;
        m_turnStrength = TurnSpeed.value;



        if (!VR)
        {
            // Main Thrust
            m_currThrust = 0.0f;
            aclAxis = Input.GetAxis("Vertical");
            if (aclAxis > m_deadZone)
                m_currThrust = aclAxis * m_forwardAcl;
            else if (aclAxis < -m_deadZone)
                m_currThrust = aclAxis * m_backwardAcl;

            //  Turning
            m_currTurn = 0.0f;
            turnAxis = Input.GetAxis("Horizontal");
            if (Mathf.Abs(turnAxis) > m_deadZone)
                m_currTurn = turnAxis;

        }
        else
        {
            if (leftGripped)

            {
                lastleftinput = -leftControl.triggerAxis;

            }

            if (rightGripped)

            {
                lastrightinput = rightControl.triggerAxis;

            }
            m_currTurn = (lastleftinput + lastrightinput);
            m_currThrust = ((-lastleftinput* .75f)+ (lastrightinput * .75f)) * m_forwardAcl;


        }


        currentVelocity = m_body.velocity.magnitude;
        Gforce = (currentVelocity - lastFrameVelocity) / (Time.deltaTime * Physics.gravity.magnitude);
        lastFrameVelocity = currentVelocity;

    }

  void FixedUpdate()
  {
        speed = m_body.velocity.magnitude * 2.237f;
        yCoord = yCoord + 0.01f;
        noise = (Mathf.PerlinNoise(5, yCoord) * .2f) + .6f;
    //  Hover Force
    RaycastHit hit;
    for (int i = 0; i < m_hoverPoints.Length; i++)
    {
      var hoverPoint = m_hoverPoints [i];
      if (Physics.Raycast(hoverPoint.transform.position, 
                          -Vector3.up, out hit,
                          m_hoverHeight,
                          m_layerMask))
        m_body.AddForceAtPosition(Vector3.up 
          * m_hoverForce
          *noise
          * (1.0f - (hit.distance / m_hoverHeight)), 
                                  hoverPoint.transform.position);
      else
      {
        if (transform.position.y > hoverPoint.transform.position.y)
          m_body.AddForceAtPosition(
            hoverPoint.transform.up * m_hoverForce,
            hoverPoint.transform.position);
        else
          m_body.AddForceAtPosition(
            hoverPoint.transform.up * -m_hoverForce,
            hoverPoint.transform.position);
      }
    }

    // Forward
    if (Mathf.Abs(m_currThrust) > 0)
      m_body.AddForce(transform.forward * m_currThrust);

    // Turn
    if (!(m_currTurn == 0))
    {
      m_body.AddRelativeTorque(Vector3.up * m_currTurn * m_turnStrength);
        }
        

   

    }
   
    
}
