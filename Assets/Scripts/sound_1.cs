using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_1 : MonoBehaviour
{
    public AudioClip calungClip;  // Assign the sound clip in the Inspector
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        // Play sound when the bamboo is tapped
        if (calungClip != null)
        {
            audioSource.PlayOneShot(calungClip);
        }
    }
}
