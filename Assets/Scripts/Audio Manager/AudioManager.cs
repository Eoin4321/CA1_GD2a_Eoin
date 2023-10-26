using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Added a header to the fields in the unity inspector.
    [Header("---------- Audio Source -----------")]
    //Creating my variables [SerializeField] lets me edit the variable inside the unity app
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio clips")]
    public AudioClip background;
    public AudioClip shrink;
    public AudioClip jump;

    //Video I learned this from How to Add MUSIC and SOUND EFFECTS to a Game in Unity | Unity 2D Platformer Tutorial #16 By Rehope Games
    //Link to video: https://www.youtube.com/watch?v=N8whM1GjH4w&t=170s
    //Start is called right before any update methods.
    public void Start()
    {
        //Sets the musicSource to background
        musicSource.clip = background;
        //Play MusicSource which is background music.
        musicSource.Play();
    }

    //Takes in an audio clip and plays it.
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
