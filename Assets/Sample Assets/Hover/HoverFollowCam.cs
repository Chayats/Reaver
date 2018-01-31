using UnityEngine;
using System.Collections;

public class HoverFollowCam : MonoBehaviour
{
  public float m_camHeight;
  public float m_camDist;
  GameObject m_player;
  int m_layerMask;
    public Vector3 offsetCam;
    public bool locked;
    public float pitch;

  void Start()
  {
    m_player = GameObject.FindGameObjectWithTag("reaver") as GameObject;

    offsetCam = transform.position - m_player.transform.position;
    m_camHeight = offsetCam.y;
    m_camDist = Mathf.Sqrt(
      offsetCam.x * offsetCam.x + 
      offsetCam.z * offsetCam.z);

    m_layerMask = 1 << LayerMask.NameToLayer("car");
    m_layerMask = ~m_layerMask;
  }
	
  void Update()
  {
    Vector3 camOffset = -m_player.transform.forward;
    camOffset = new Vector3(camOffset.x, 0.0f, camOffset.z) * m_camDist
      + Vector3.up * m_camHeight;

    RaycastHit hitInfo;
    if (Physics.Raycast(m_player.transform.position, camOffset, 
                       out hitInfo, m_camDist, 
                       m_layerMask))
    {
      transform.position = hitInfo.point;
    } else
    {
      transform.position = m_player.transform.position + camOffset;
    }

    transform.LookAt(m_player.transform.position);

        if (locked)
            transform.localEulerAngles = new Vector3(pitch, transform.localEulerAngles.y, m_player.transform.localEulerAngles.z);
    }
}
