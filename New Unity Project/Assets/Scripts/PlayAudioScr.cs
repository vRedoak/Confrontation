using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudioScr : MonoBehaviour {

    public GameObject Sound;
    new AudioSource audio;
    public AudioClip corect;
    public void Play()
    {

        //GetComponent<AudioSource>().Play();
        //DontDestroyOnLoad(GetComponent<AudioSource>());

        audio = GetComponent<AudioSource>();
        DontDestroyOnLoad(Sound);
        audio.PlayOneShot(corect);
       // Destroy(Sound, 4f);
    }

    
    public void Pause()
    {
         GameObject.Find("BackgroundAudio").GetComponent<AudioSource>().Stop();
      //  Sound.Stop();
    }
    public void Pl()
    {
        GameObject.Find("BackgroundAudio").GetComponent<AudioSource>().Play();
    }

}
