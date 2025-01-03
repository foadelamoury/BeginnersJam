using UnityEngine;
using TMPro;
using System.Collections;

public class dialogue2 : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    private string fullText = "Look closely. Do you recognize this face? It is yours. You’ve been chasing the remnants of your own story, a story that ended long ago. This is your body, left behind in the chaos of war. You are not among the living… you never were.";
    [SerializeField] private float typingSpeed = 0.05f;
    private Coroutine typingCoroutine;

    void OnEnable()
    {
        // Start typing when object becomes active
        if (dialogueText != null)
        {
            // Stop any existing typing coroutine
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);
                
            typingCoroutine = StartCoroutine(TypeText());
        }
    }

    void OnDisable()
    {
        // Clean up when object becomes inactive
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        
        // Clear the text when disabled
        if (dialogueText != null)
            dialogueText.text = "";
    }

    IEnumerator TypeText()
    {
        dialogueText.text = "";
        foreach (char letter in fullText.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}