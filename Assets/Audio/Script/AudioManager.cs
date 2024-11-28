using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;
    private Dictionary<string, AudioClip> dictAudio;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        dictAudio = new Dictionary<string, AudioClip>();
    }


    public AudioClip LoadAudio(string path)
    {
        return (AudioClip)Resources.Load(path);
    }


    private AudioClip GetAudio(string path)
    {
        if (!dictAudio.ContainsKey(path))
        {
            dictAudio[path]=LoadAudio(name);
        }
        return dictAudio[path];
    }


    public void PlayerBGM(string name, float volume = 1f)
    {
        audioSource.Stop();
        audioSource.clip = GetAudio(name);
        audioSource.Play();
    }

    public void StopBGM()
    {

        audioSource.Stop();
    }
    
    public void PlaySFX(string path,float volume = 1f)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(GetAudio(path),volume);
        audioSource.volume = volume;
    }

    public void PlaySFX(AudioSource audioSource,string path,float volume = 1f)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(GetAudio(path), volume);
        audioSource.volume = volume;
    }
    

}
