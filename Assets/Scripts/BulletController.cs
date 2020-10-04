using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    private Rigidbody m_rigidbody;
    public GameObject spark;
    public GameObject bullet_impact;
    public GameObject bullet_impact_concrete;

    // Start is called before the first frame update
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(spark, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
        Instantiate(bullet_impact, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
        Instantiate(bullet_impact_concrete, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));

        if (collision.gameObject.CompareTag("Slicable"))
        {
            var object_to_slice = collision.gameObject;
            
            var pdc = object_to_slice.GetComponent<PartialDestructionController>();
            if (pdc) pdc.detach(collision.relativeVelocity);
            
            collision.transform.parent = null;
            object_to_slice.GetComponent<Rigidbody>().isKinematic = true;
            var sliced_pieces = EzySlice.SlicerExtensions.SliceInstantiate(object_to_slice, transform.position, object_to_slice.transform.up, object_to_slice.GetComponent<MeshRenderer>().sharedMaterial);
            if (sliced_pieces != null)
            {
                foreach (var piece in sliced_pieces)
                {
                    if (piece)
                    {
                       
                        try
                        {
                            var rigid = piece.AddComponent<Rigidbody>();
                            rigid.mass = 60;
                            var col = piece.AddComponent<MeshCollider>();
                            col.convex = true;

                            piece.tag = "Slicable";
                            rigid.AddForce(collision.relativeVelocity, ForceMode.Impulse);
                        }
                        catch(System.Exception e)
                        {
                            Destroy(piece);
                        }
                    }
                }
            }
            Destroy(object_to_slice);
        }
        Destroy(gameObject);
    }

}
