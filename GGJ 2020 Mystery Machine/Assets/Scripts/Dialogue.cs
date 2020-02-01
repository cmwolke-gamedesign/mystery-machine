using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dialogue : MonoBehaviour {

  public static Dialogue Instance;
  public TMPro.TextMeshProUGUI characterDialogue;
  public float dialogueDisplayTime = 3f;
  public float dialogueAppearTime = 0.025f;
  public bool talking { private set; get;}

  private Queue<string[]> dialogueQueue;

  private void Start() 
  {
    if (Instance != null) 
    {
      Destroy(Instance);
    }
    Instance = this;
    dialogueQueue = new Queue<string[]>();
    characterDialogue.text = "";
  }


  private void Update() 
  {
    if (!talking && dialogueQueue.Count > 0) 
    {
      StartCoroutine(Say(dialogueQueue.Dequeue()));
    }
  }

  private IEnumerator Say(string[] textSnippets) 
  {
    this.talking = true;
      this.characterDialogue.text = "";
    foreach(string nextSnippet in textSnippets) 
    {
      foreach(char c in nextSnippet) 
      {
        yield return new WaitForSeconds(dialogueAppearTime);
        this.characterDialogue.text += c;
      }
      yield return new WaitForSeconds(dialogueDisplayTime);
      this.characterDialogue.text = "";
    }
    yield return new WaitForSeconds(0.5f);
    this.characterDialogue.text = "";
    this.talking = false;

  }

  public void SaySomething(string[] bla) 
  {
     this.dialogueQueue.Enqueue(bla);
  }

  public void SaySomethingNow(string[] bla) 
  {
    // override current text ? 
  }
}