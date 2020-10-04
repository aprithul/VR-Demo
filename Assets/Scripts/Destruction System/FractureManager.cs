using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class FractureManager : MonoBehaviour
{
    public GameObject object_to_fracture;
    // Start is called before the first frame update
    void Start()
    {
        var _mat = object_to_fracture.GetComponent<MeshRenderer>().sharedMaterial;
        SlicerExtensions.SliceInstantiate(object_to_fracture, Vector3.up*1.5f, Vector3.up, _mat);
        Destroy(object_to_fracture);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
