using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image dialogueImage;
    [SerializeField] private Sprite dialogueSprite;
    public static event Action<bool> PausePlayer = delegate { };

    private float readDelay = 1.2f;
    private float typingTime = 0.05f;
    private bool isPlayerInRange = false;
    private bool didDialogueStart = false;
    private int lineIndex;
    private bool hasDialogueFinished = false;
    private bool isLineFullyDisplayed = false;



    private void Update()
    {
        if (isPlayerInRange && !hasDialogueFinished)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (isLineFullyDisplayed)
            {
                NextDialogueLine();
            }
        }
    }

    private IEnumerator ShowLine()
    {
        isLineFullyDisplayed = false;
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        yield return new WaitForSecondsRealtime(readDelay); 
        isLineFullyDisplayed = true;
    }

    private void StartDialogue()
    {
        PausePlayer.Invoke(true);
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        if (dialogueImage != null && dialogueSprite != null)
        {
            dialogueImage.sprite = dialogueSprite;
            dialogueImage.enabled = true;
        } 
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            hasDialogueFinished = true; 
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
            PausePlayer.Invoke(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
}
