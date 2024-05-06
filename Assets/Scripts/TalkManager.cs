using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public enum TalkEmotion
    {
        IDLE,
        TALK,
        SMILE,
        ANGRY,
    }

    public struct TalkData
    {
        public TalkEmotion talkEmotion;
        public string talkString;

        public TalkData(TalkEmotion talkEmotion, string talkString)
        {
            this.talkEmotion = talkEmotion;
            this.talkString = talkString;
        }
    }

    Dictionary<int, TalkData[]> objectTalkDictionary;

    void Awake()
    {
        GenerateData();
    }

    private void GenerateData()
    {
        this.objectTalkDictionary = new Dictionary<int, TalkData[]>
        {
            {
                100,
                new TalkData[]{
                    new TalkData(TalkEmotion.IDLE, "����� ���� ���ڴ�.")
                
                }
            },
            {
                200,
                new TalkData[]{
                    new TalkData(TalkEmotion.IDLE, "������ ����ߴ� ������ �ִ� å���̴�.")
                }
            },
            {
                1000,
                new TalkData[]{
                    new TalkData(TalkEmotion.IDLE, "�ȳ�?"),
                    new TalkData(TalkEmotion.TALK, "�� ���� ó�� �Ա���?"),
                    new TalkData(TalkEmotion.SMILE, "������ �ݰ���"),
                    new TalkData(TalkEmotion.ANGRY, "�ʹ� ����!"),
                }
            },
            {
                2000,
                new TalkData[]{
                    new TalkData(TalkEmotion.IDLE, "����"),
                    new TalkData(TalkEmotion.ANGRY, "¥����!"),
                    new TalkData(TalkEmotion.SMILE, "�� ȣ���� ���� �Ƹ�����?"),
                    new TalkData(TalkEmotion.TALK, "��� �� ȣ������ ������ ����� ������ �ִٰ� ��."),
                }
            },
        };
    }

    public TalkData? GetTalk(ObjectData objectData)
    {
        if (this.objectTalkDictionary == null)
        {
            GenerateData();
        }
        
        if (objectData == null)
            return null;

        if (!(objectTalkDictionary.ContainsKey(objectData.id)))
            return null;
        
        TalkData[] talkList = objectTalkDictionary[objectData.id];

        int currentTalkIndex = objectData.talkIndex;
        if (currentTalkIndex > (talkList.Length - 1))
        {
            objectData.talkIndex = 0;
            return null;
        }
        else
        {
            objectData.talkIndex = currentTalkIndex + 1;
            return talkList[currentTalkIndex];
        }
    }
}
