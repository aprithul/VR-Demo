using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceTest : MonoBehaviour
{

    public GameObject bamboo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            var sliced = EzySlice.SlicerExtensions.SliceInstantiate(bamboo, transform.position, transform.up);

            if (sliced != null)
            {
                foreach (var s in sliced)
                {
                    var rigid = s.AddComponent<Rigidbody>();
                    var col = s.AddComponent<MeshCollider>();
                    col.convex = true;
                    rigid.AddForce(transform.up);


                }

                Destroy(bamboo);
            }
        }
    }
}
