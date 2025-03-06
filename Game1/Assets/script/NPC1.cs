using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogueLines; // Editable in Inspector
    public GameObject dialogueUI; // Assign a UI Panel for dialogue
    public TMP_Text dialogueText; // Changed from Text to TMP_Text for TextMeshPro
    public GameObject interactionPrompt; // UI prompt to show "Press E"
    public float interactionRange = 3f;
    private int currentLine = 0;
    private bool isTalking = false;
    private bool isPlayerInRange = false;

    void Start()
    {
        dialogueUI.SetActive(false); // Ensure dialogue UI is hidden at start
        interactionPrompt.SetActive(false); // Ensure interaction prompt is hidden at start
    }

    void Update()
    {
        CheckPlayerDistance();

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isTalking)
            {
                StartDialogue();
            }
            else
            {
                NextDialogue();
            }
        }
    }

    void CheckPlayerDistance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            isPlayerInRange = distance <= interactionRange;
            interactionPrompt.SetActive(isPlayerInRange && !isTalking); // Show prompt only when in range and not talking
        }
    }

    void StartDialogue()
    {
        if (dialogueText == null)
        {
            Debug.LogError("DialogueText is NOT assigned! Assign it in the Inspector.");
            return;
        }

        if (dialogueLines.Length > 0)
        {
            isTalking = true;
            dialogueUI.SetActive(true);
            interactionPrompt.SetActive(false); // Hide the prompt when dialogue starts
            dialogueText.text = dialogueLines[currentLine];
            Debug.Log("Dialogue Started: " + dialogueText.text);
        }
    }

    void NextDialogue()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
            Debug.Log("Updated Dialogue: " + dialogueText.text);
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false); // Hide dialogue UI after dialogue ends
        interactionPrompt.SetActive(true); // Show the prompt again after dialogue ends
        currentLine = 0;
    }
}
