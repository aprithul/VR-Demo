using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAudioEnd : MonoBehaviour
{
    AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_audio.time>0 && !_audio.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
