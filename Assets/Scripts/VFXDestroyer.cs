using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXDestroyer : MonoBehaviour
{
    VisualEffect vfx;
    private bool started;
    // Start is called before the first frame update
    void Awake()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vfx.aliveParticleCount > 0)
            started = true;

        if(started && vfx.aliveParticleCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
