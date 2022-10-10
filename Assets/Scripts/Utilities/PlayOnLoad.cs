using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnLoad : MonoBehaviour
{
    public AudioSource source;
    public AudioSystem sys;

    public void Start()
    {
        sys = AudioSystem.instance;
        sys.PlayMusicLooping(source.clip);
    }
}
