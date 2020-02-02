using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject buttonsContainer;
    public CanvasGroup creditsContainer;
    public Button creditsBackButton;
    public Texture2D cursor;

    private void Start() 
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        StartCoroutine(LerpCanvasGroup(creditsContainer, 0, 1, 0));
        creditsBackButton.interactable = false;
        {
            
        }
    }
    public void StartGame() 
    {
        SceneManager.LoadScene("scn_Main");
    }

    public void ShowCredits() 
    {
        StartCoroutine(LerpCanvasGroup(creditsContainer, 1, 0));
        creditsBackButton.interactable = true;
        // creditsContainer.GetComponentInChildren<Button>().interactable = true;
        foreach(Transform child in buttonsContainer.transform) 
        {
            child.GetComponent<Button>().interactable = false;
        }
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void ToMainMenu() 
    {
        StartCoroutine(LerpCanvasGroup(creditsContainer, 0, 1));
        creditsBackButton.interactable = false;
        foreach(Transform child in buttonsContainer.transform) 
        {
            child.GetComponent<Button>().interactable = true;
        }
    }

    private IEnumerator LerpCanvasGroup(CanvasGroup gr, float toAlpha, float fromAlpha, float duration = 1f) 
    {
        float timer = 0f;
        while (timer < duration) 
        {
            timer += Time.deltaTime;
            gr.alpha = Mathf.Lerp(fromAlpha, toAlpha, timer / duration);
            yield return null;
        }
        gr.alpha = toAlpha;
    }
}
