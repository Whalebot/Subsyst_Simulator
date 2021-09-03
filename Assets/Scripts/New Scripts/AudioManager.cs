using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public GameObject soundPrefab;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySound(AudioClip c) {
        GameObject g = Instantiate(soundPrefab);
        AudioSource a = g.GetComponent<AudioSource>();
        a.clip = c;
        a.Play();
    }
}
