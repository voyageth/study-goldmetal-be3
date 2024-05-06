using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TalkManager;

public class GameManager : MonoBehaviour
{
    private const string H_AXIS = "Horizontal";
    private const string V_AXIS = "Vertical";
    
    public GameObject talkPanel;
    public Image portraitImage;
    public Text talkText;
    public TalkManager talkManager;
    
    bool isTalkDialogOpen;

    public void DialogAction(GameObject scanObject)
    {
        ObjectData objectData = scanObject.GetComponent<ObjectData>();
        Talk(objectData);
        talkPanel.SetActive(isTalkDialogOpen);
    }

    private void Talk(ObjectData objectData)
    {
        TalkData? nullableTalk = talkManager.GetTalk(objectData);
        if (nullableTalk == null)
        {
            isTalkDialogOpen = false;
            return;
        }

        TalkData talk = nullableTalk.Value;

        if (objectData.isNpc)
        {
            Sprite portraitSprite = objectData.GetPortraitSprite(talk.talkEmotion);
            portraitImage.sprite = portraitSprite;
            portraitImage.color = new Color(1, 1, 1, 1);
        } 
        else
        {
            portraitImage.sprite = null;
            portraitImage.color = new Color(1, 1, 1, 0);
        }

        isTalkDialogOpen = true;
        talkText.text = talk.talkString;
    }

    public void GetPlayerInput(out float h, out float v, out bool hDown, out bool vDown, out bool hUp, out bool vUp)
    {
        if (isTalkDialogOpen)
        {
            // move value
            h = 0f;
            v = 0f;

            // check button up/down
            hDown = false;
            vDown = false;
            hUp = false;
            vUp = false;
        }
        else
        {
            // move value
            h = Input.GetAxisRaw(H_AXIS);
            v = Input.GetAxisRaw(V_AXIS);

            // check button up/down
            hDown = Input.GetButtonDown(H_AXIS);
            vDown = Input.GetButtonDown(V_AXIS);
            hUp = Input.GetButtonUp(H_AXIS);
            vUp = Input.GetButtonUp(V_AXIS);

        }
    }
}
