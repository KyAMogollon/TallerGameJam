using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip footstepClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayFootstepSound()
    {
        if (!audioSource.isPlaying) // Asegúrate de que no se superpongan los sonidos
        {
            audioSource.PlayOneShot(footstepClip);
        }
    }
}
