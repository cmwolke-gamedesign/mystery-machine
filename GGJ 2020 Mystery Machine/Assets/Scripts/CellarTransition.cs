using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellarTransition : MonoBehaviour
{
    public Transform cellarEntryPosition;
    public Transform gardenEntryPosition;
    public GameObject cellarDoor;
    private bool _fellIntoCellar = false;
    public TransitionDirection direction = TransitionDirection.OutOfCellar;
    
    public AudioClip FallIntoCellarAudio, ClimbUpAudio, ClimbDownAudio;

    private CameraFollower _cam;

    private void Start() {

        _cam = FindObjectOfType<CameraFollower>();
    }

    public void TriggerTransition() {
        if (direction == TransitionDirection.IntoCellar) {
            if (!_fellIntoCellar) {
                StartCoroutine(FallDownIntoCellar());
                // firstTime, tumble down action
            } else {
                StartCoroutine(GoToCellar());
                // walk up normally
            }
        } else {
            StartCoroutine(GoToGarden());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            this.TriggerTransition();
        }
    }

    private IEnumerator FallDownIntoCellar() {
        Player.Instance.SetPlayerControls(false);
        ScreenEffects.Instance.FadeOutEffect(0.2f, 2f, 0.5f);
        SoundsManager.Instance.PlaySound(FallIntoCellarAudio);
        yield return new WaitForSeconds(0.5f);
        Player.Instance.transform.position = cellarEntryPosition.position;
        _cam.SetBounds(true);
        yield return new WaitForSeconds(2f);
        string[] fallDialogue = new string[] {
            "Ouch!",
            "Since when is there a hole in the floor?",
            "What is this room?",
            "And that ... thing?",
            "There are notes on the wall, maybe they can explain.",
        };
        cellarDoor.SetActive(false);
        Dialogue.Instance.SaySomething(fallDialogue);
        yield return new WaitForSeconds(Dialogue.Instance.GetDurationOfSnippets(fallDialogue) + 0.5f);
        Player.Instance.SetPlayerControls(true);
        _fellIntoCellar = true;
        yield return null;
    }

    private IEnumerator GoToCellar() {
        Player.Instance.SetPlayerControls(false);
        SoundsManager.Instance.PlaySound(ClimbDownAudio);
        ScreenEffects.Instance.FadeOutEffect(0.2f, 0.5f, 0.3f);
        yield return new WaitForSeconds(0.5f);
        Player.Instance.transform.position = gardenEntryPosition.position;
        _cam.SetBounds(true);
        yield return new WaitForSeconds(0.5f);
        Player.Instance.SetPlayerControls(true);
    }

    private IEnumerator GoToGarden() {
        Player.Instance.SetPlayerControls(false);
        SoundsManager.Instance.PlaySound(ClimbUpAudio);
        ScreenEffects.Instance.FadeOutEffect(0.2f, 0.5f, 0.3f);
        yield return new WaitForSeconds(0.5f);
        Player.Instance.transform.position = gardenEntryPosition.position;
        _cam.SetBounds(false);
        yield return new WaitForSeconds(0.5f);
        Player.Instance.SetPlayerControls(true);
    }

    public void Click(BaseEventData bed) {
        print(bed);
    }
}

public enum TransitionDirection {
    OutOfCellar, IntoCellar
}