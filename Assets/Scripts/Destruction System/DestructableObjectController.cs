using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjectController : MonoBehaviour
{
    public GameObject fractured_root;
    public float min_destroy_speed;

    private Rigidbody[] fractured_pieces;

    private void Awake()
    {
        fractured_pieces = fractured_root.GetComponentsInChildren<Rigidbody>();
        fractured_root.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("projectile"))
        {
            if (collision.relativeVelocity.magnitude >= min_destroy_speed)
            {
                fractured_root.transform.parent = null;
                fractured_root.gameObject.SetActive(true);
                var speed = collision.relativeVelocity.magnitude;
                Debug.Log("pieces: " + fractured_pieces.Length);
                foreach (var p in fractured_pieces)
                {
                    p.AddExplosionForce(speed * 100, collision.contacts[0].point, 10f);
                }
                Destroy(gameObject);
            }
        }
    }
}
