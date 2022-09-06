using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuidoSystem : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] backgroundClips;
    public AudioClip[] clips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A");
            audioSource.PlayOneShot(clips[0]);
        }
    }
}
