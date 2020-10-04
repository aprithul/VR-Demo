using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GunController : MonoBehaviour
{
    public GameObject bullet_prefab;
    public Transform spawn_point;
    public Transform muzzle_point;
    public GameObject muzzle_flash;
    public SteamVR_Action_Boolean shoot_action;
    private AudioSource audio_source;
    // Start is called before the first frame update
    void Awake()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || shoot_action.stateDown)
        {
            Instantiate(bullet_prefab, spawn_point.position, spawn_point.rotation, spawn_point);
            Instantiate(muzzle_flash, muzzle_point.position, muzzle_point.rotation, muzzle_point);
            if(!audio_source.isPlaying)
            {
                audio_source.Play();
            }
        }
    }
}