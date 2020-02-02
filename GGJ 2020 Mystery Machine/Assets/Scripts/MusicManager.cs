using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; 
    
    public AudioClip[] startMusic;
    public AudioClip[] progressMusic;
    public AudioClip[] endMusic;

    private AudioSource ac;

    private int currentMusicIdx;

    private int progressCounter;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        ac = GetComponent<AudioSource>();
        ac.playOnAwake = false;
        progressCounter = Random.Range(0, 2);
        ac.clip = getNextClip();
        ac.Play();
    }

    private void OnEnable() {
        GameEvents.MMItemAdded += MMItemAdded;
    }

    private void OnDisable() {
        GameEvents.MMItemAdded -= MMItemAdded;
    }

    private void Update() {
        if (!ac.isPlaying) {
            ac.clip = getNextClip();
            ac.Play();
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
        float volumeDownTime = 1f;
        float startVolume = ac.volume;
        while (timer < volumeDownTime) {
            timer += Time.deltaTime;
            ac.volume = (1 - timer / volumeDownTime) * startVolume;
            yield return null;
        }
        ac.Stop();
        ac.volume = startVolume;
        yield return new WaitForSeconds(0.5f);
        ac.clip = getNextClip();
        ac.Play();
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
