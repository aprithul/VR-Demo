using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DestructionManager : MonoBehaviour
{
    public GameObject dust_cloud;
    public GameObject root;
    [HideInInspector]public PartialDestructionController pdc_root;
    private List<PartialDestructionController> _destruction_pieces = new List<PartialDestructionController>();

    private static DestructionManager _instance;
    public static DestructionManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<DestructionManager>();
            return _instance;
        }

    }

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var _pdc = child.gameObject.AddComponent<PartialDestructionController>();
            child.gameObject.AddComponent<Rigidbody>();
            var _mc = child.gameObject.AddComponent<MeshCollider>();
            _mc.convex = true;
            child.gameObject.tag = "Slicable";
            _destruction_pieces.Add(_pdc);
        }

        pdc_root = root.GetComponent<PartialDestructionController>();
    }

    public static void update_state()
    {
        foreach (var pdc in DestructionManager.Instance._destruction_pieces)
        {
            pdc.check_flag = false;
        }

        if(!DestructionManager.Instance.pdc_root)
        {
            //Debug.LogError("root can't be null");
            return;
        }
        // recursively find all pieces reachable from root
        recursive_update(DestructionManager.Instance.pdc_root);

        // drop all pieces not reachable from root
        foreach(var pdc in DestructionManager.Instance._destruction_pieces)
        {
            if(pdc && !pdc.check_flag)
            {
                pdc.drop();
            }
        }
    }

    private static void recursive_update(PartialDestructionController pdc)
    {

        if (!pdc.check_flag)
        {
            if (!pdc.is_connected)
                return;
            pdc.check_flag = true;

            foreach (var _pdc in pdc.connected_to)
            {
                recursive_update(_pdc);
            }
        }
        else
            return;
    }

    private void Update()
    {
    }

    VisualEffect vfx;
    public void spawn_dust_cloud(Vector3 position)
    {
        Instantiate(dust_cloud, position, Quaternion.identity);
    }

}
