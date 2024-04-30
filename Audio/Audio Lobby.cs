using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioLobby : MonoBehaviour
{
    
    [SerializeField] private AudioSource musicSource;

    public AudioClip background;

    void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}