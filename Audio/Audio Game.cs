using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioGame : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    public Toggle toggleSwitch;
    public AudioClip background;
    public float musicVolume;

    void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the toggle is on
        if (toggleSwitch.isOn)
        {
            // Set the music volume to full
            musicSource.volume = musicVolume;
        }
        else
        {
            // Mute the music
            musicSource.volume = 0f;
        }
    }
}
