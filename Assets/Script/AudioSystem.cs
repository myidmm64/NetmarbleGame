using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public enum AudioName
    {
        jump = 0,
        Brandish = 3,
        None = 100
    }

    public static AudioSystem auidoSystem;
    private AudioSource audioSource;
    public AudioClip[] backgroundClips;
    public AudioClip[] clips;

    private void Awake()
    {
        auidoSystem = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
            
        }
    }
    public void AudioClipPlayOneShot(AudioName name)
    {
        audioSource.PlayOneShot(clips[(int)name]);
    }
}
