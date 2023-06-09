using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeats : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    public EventReference fmodEvent;

    [SerializeField] [Range(0, 100)]
    private float intensity;
    // Start is called before the first frame update
    void Start()
    {
        instance = RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        // instance.setVolume(intensity);
        // instance.setParameterByName("intensity", intensity);
    }
}
