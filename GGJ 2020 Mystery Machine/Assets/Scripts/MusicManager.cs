using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; 
    
    public AudioClip[] startMusic;
    public AudioClip[] progressMusic;
    public AudioClip[] endMusic;

    private AudioSource audio;

    private int currentMusicIdx;

    private int progressCounter;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Instance = this;
        audio = GetComponent<AudioSource>();
        audio.playOnAwake = false;
        progressCounter = Random.Range(0, 2);
        audio.clip = getNextClip();
        audio.Play();
    }

    private void OnEnable() {
        GameEvents.MMItemAdded += MMItemAdded;
    }

    private void OnDisable() {
        GameEvents.MMItemAdded -= MMItemAdded;
    }

    private void Update() {
        if (!audio.isPlaying) {
            audio.clip = getNextClip();
            audio.Play();
        }
    }

    public void MMItemAdded() {
        progressCounter++;
        if (progressCounter == 2 || progressCounter == 4) {
            StartCoroutine(TransitionMusic());
            // transition to progress / end music
        }
    }

    private IEnumerator TransitionMusic() {
        float timer = 0f;
        float volumeDownTime = 0.5f;
        float startVolume = audio.volume;
        while (timer < volumeDownTime) {
            audio.volume = 1 - timer / volumeDownTime;
            yield return null;
        }
        audio.Stop();
        audio.volume = startVolume;
        yield return new WaitForSeconds(0.5f);
        audio.clip = getNextClip();
        audio.Play();
    }
    
    private AudioClip[] getClipArrayByProgress(int progress) {
        if (progressCounter < 2) {
            return startMusic;
        } else if (progressCounter < 4) {
            return progressMusic;
        } else {
            return endMusic;
        }
    }

    private AudioClip getNextClip() {
        if (currentMusicIdx == getClipArrayByProgress(progressCounter).Length - 1) {
            currentMusicIdx = 0;
        } else {
            currentMusicIdx++;
        }
        return getClipArrayByProgress(progressCounter)[currentMusicIdx];
    }
}
