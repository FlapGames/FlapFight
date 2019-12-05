using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip jumpsound, hitsound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        jumpsound = Resources.Load<AudioClip>("Jumpsound");
        hitsound = Resources.Load<AudioClip>("Hitsound");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Jumpsound":
                audioSrc.PlayOneShot(jumpsound);
                break;
            case "Melee":
                audioSrc.PlayOneShot(hitsound);
                break;
        }
    }
}
