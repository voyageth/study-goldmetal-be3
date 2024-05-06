using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    public GameObject EndCursor;

    string targetMessage;
    Text messageText;
    AudioSource audioSource;
    int index;

    private void Awake()
    {
        messageText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMessage(string targetMessage)
    {
        this.targetMessage = targetMessage;
        EffectStart();
    }

    void EffectStart()
    {
        messageText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        Invoke("EffectOnProgress", 1.0f / CharPerSeconds);
    }

    void EffectOnProgress()
    {
        if (messageText.text == targetMessage)
        {
            EffectEnd();
            return;
        }

        messageText.text += targetMessage[index];

        // sound
        if (targetMessage[index] != ' ' || targetMessage[index] != '.')
            audioSource.Play();

        index++;

        Invoke("EffectOnProgress", 1.0f / CharPerSeconds);
    }

    void EffectEnd()
    {
        CancelInvoke();
        EndCursor.SetActive(true);
    }
}
