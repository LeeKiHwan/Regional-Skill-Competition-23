using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SFXPlay(string name, AudioClip clip)
    {
        GameObject go = new GameObject(name);
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();

        Destroy(go, clip.length);
    }
}
