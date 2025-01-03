using UnityEngine;
using TMPro;
using System.Collections;

public class dialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    private string fullText = "To all remaining soldiers: recover the dog tags of the fallen. Deliver their names to their families as a final honor to their sacrifice. Leave nothing behind. Take what you can, and return to base immediately. This is not just an order—it's our duty";
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