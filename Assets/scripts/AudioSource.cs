using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaSonido : MonoBehaviour
{
    public AudioClip sonido; 
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(sonido); 
        }
    }
}
