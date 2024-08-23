using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MeteorPistol : MonoBehaviour
{
    public ParticleSystem particles;

    public LayerMask layerMask;
    public Transform shootSource;
    public float raycastDistance = 10;

    private bool rayActivate = false;
    
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(x => StartShoot());
        grabInteractable.deactivated.AddListener(x => StopShoot());
    }

    void Update()
    {
        if(rayActivate)
        {
            RaycastCheck();
        }
        
    }

    public void StartShoot()
    {
        particles.Play();
        rayActivate = true;

        AudioManager.instance.Play("Pistol");
    }

    public void StopShoot()
    {
        particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        rayActivate = false;

        AudioManager.instance.Stop("Pistol");
    }

    void RaycastCheck()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(shootSource.position, shootSource.forward,
            out hit, raycastDistance, layerMask);

            if(hasHit)
            {
                hit.transform.gameObject.SendMessage("Break", SendMessageOptions.DontRequireReceiver);
            }
    }
}
