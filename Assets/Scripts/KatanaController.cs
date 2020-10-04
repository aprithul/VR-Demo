using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaController : MonoBehaviour
{
    public float minimum_cutting_speed;
    public Rigidbody rb;
    public int sliding_window_size;
    public Transform velocity_transform;
    private bool can_slice = true;
    private Material _material;
    private Vector3 _velocity;
    private Vector3 last_pos;
    List<Vector3> sliding_window = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        var pos_delta = velocity_transform.position - last_pos;
        var vel_delta = pos_delta / Time.deltaTime;
        sliding_window.Add(vel_delta);
        while(sliding_window.Count > sliding_window_size)
        {
            sliding_window.RemoveAt(0);
        }

        _velocity = Vector3.zero;
        foreach(var p in sliding_window)
        {
            _velocity += p;
        }
        _velocity /= sliding_window.Count;
        last_pos = velocity_transform.position;


    }

    private void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision speed: " + collision.relativeVelocity.magnitude);
        var right_side = false;
        var _contact = collision.GetContact(0);
        if (velocity_transform.InverseTransformPoint(_contact.point).z < 0)
            right_side = true;

        if (can_slice  && collision.gameObject.CompareTag("Slicable") && _velocity.magnitude >= minimum_cutting_speed)
        {
            var object_to_slice = collision.gameObject;
            var sliced_pieces = EzySlice.SlicerExtensions.SliceInstantiate(object_to_slice, transform.position, transform.right, object_to_slice.GetComponent<MeshRenderer>().sharedMaterial);
            if (sliced_pieces != null)
            {
                foreach (var piece in sliced_pieces)
                {
                    if (piece)
                    {
                        var rigid = piece.AddComponent<Rigidbody>();
                        var col = piece.AddComponent<MeshCollider>();
                        col.convex = true;
                        piece.tag = "Slicable";
                    }
                }
            }
            Destroy(object_to_slice);
            can_slice = false;
            StartCoroutine(enable_slicing());
        }
    }

    IEnumerator enable_slicing()
    {
        yield return new WaitForSeconds(1f);
        can_slice = true;
    }
}
