using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PooledFX : MonoBehaviour, IPooledObject
{
    public ParticleSystem particle;

    //using pool start
    //Must have, even left blank. Also, put everything in Start() function here
    public void OnObjectSpawn()
    {
        particle.Play();
        RestoreValues(); 
    }
    //Must have, even left blank.
    public void OnObjectDespawn()
    {
    }
    //specifically for restoring some values, like health
    public void RestoreValues()
    {
        
    }

    //private void OnDisable()
    //{
        
    //}
    //using pool end

    private void Awake()
    {
        particle = gameObject.GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        var main = particle.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped()
    {
        GetComponent<PooledObjectAttachment>().PutBackToPool();
    }
}
