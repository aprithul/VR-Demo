using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PartialDestructionController : MonoBehaviour
{
    Rigidbody m_rigidbody;
    public List<PartialDestructionController> connected_to;
    [HideInInspector] public bool is_connected = true;
    [HideInInspector] public bool check_flag = false;

    private Vector3 m_starting_pos;
    private Quaternion m_starting_orientation;

    private bool m_configured = false;
    private bool m_waiting_complete = false;
    private Vector3 m_original_scale;
    // Start is called before the first frame update
    void Awake()
    {
        connected_to = new List<PartialDestructionController>();
        m_starting_pos = transform.position;
        m_starting_orientation = transform.rotation;

        m_rigidbody = GetComponent<Rigidbody>();
        m_original_scale = transform.localScale;
        transform.localScale = m_original_scale;// * 1.15f;
        //m_rigidbody.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_configured && collision.gameObject.CompareTag("Floor"))
        {
            DestructionManager.Instance.spawn_dust_cloud(transform.position);
        }

        if (!m_configured)
        {
            var pdc = collision.gameObject.GetComponent<PartialDestructionController>();
            if (pdc)
            {
                connected_to.Add(pdc);
                StartCoroutine(wait_and_configure());
            }
        }

        /*        if(!m_waiting_complete && !m_configured)
                {
                    //Debug.Log("Connected to: " + collision.gameObject.name);
                    if(!connected_to.Contains(collision.gameObject))
                        connected_to.Add(collision.gameObject);

                    var _other = collision.gameObject.GetComponent<PartialDestructionController>().connected_to;
                    if (!_other.Contains(gameObject))
                        _other.Add(gameObject);

                    StartCoroutine(wait_and_configure());
                }
        if (collision.gameObject.CompareTag("projectile"))
        {
            detach(collision.relativeVelocity);
        }
                */


    }

    public void detach(Vector3 force)
    {
        is_connected = false;
        m_rigidbody.isKinematic = false;
        foreach(var neighbour in connected_to)
        {
            if(neighbour.is_connected)
            {
                //neighbour.drop();
            }
        }

        //m_rigidbody.AddForce(force, ForceMode.Impulse);
        DestructionManager.update_state();
        DestructionManager.Instance.spawn_dust_cloud(transform.position);
        //Destroy(gameObject);
    }

    private IEnumerator wait_and_configure()
    {
        for (int i = 10; i <= 0; i++)
            yield return new WaitForFixedUpdate();

        m_waiting_complete = true;
    }

    private void FixedUpdate()
    {
        if (!m_configured && m_waiting_complete)
        {
            Debug.Log(name + " connected to : " + connected_to.Count + " other pieces");
            m_configured = true;
            m_rigidbody.isKinematic = true;
            m_rigidbody.useGravity = true;

            transform.localScale = m_original_scale;
            transform.position = m_starting_pos;
            transform.rotation = m_starting_orientation;
        }
    }

    public void drop()
    {
        is_connected = false;
        m_rigidbody.isKinematic = false;
    }

}
