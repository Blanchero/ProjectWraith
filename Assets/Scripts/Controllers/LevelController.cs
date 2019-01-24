using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private AudioSource levelMusic;

    private void Start()
    {
        levelMusic = GetComponent<AudioSource>();
        levelMusic.Play();
    }

    private void Update()
    {
        
    }
}
