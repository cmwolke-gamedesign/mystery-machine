using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    private AudioSource _as;

    public static SoundsManager Instance;

    private void Start() {
        Instance = this;
        _as = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip) {
        _as.clip = clip;
        _as.Play();
    }
}
