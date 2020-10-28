using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorAnimation : MonoBehaviour
{
    public ParticleSystem fireR;
    public ParticleSystem fireL;
    // Start is called before the first frame update
    void Start()
    {
        fireR.Stop();
        fireL.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ActivateFire();
    }

    void ActivateFire()
    {
        if(Input.GetKey(KeyCode.W))
        {
            fireR.Play();
            fireL.Play();
        }
        fireR.Stop();
        fireL.Stop();
    }
}
