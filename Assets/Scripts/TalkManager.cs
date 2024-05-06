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
                    new TalkData(TalkEmotion.IDLE, "평범한 나무 상자다.")
                
                }
            },
            {
                200,
                new TalkData[]{
                    new TalkData(TalkEmotion.IDLE, "누군가 사용했던 흔적이 있는 책상이다.")
                }
            },
            {
                1000,
                new TalkData[]{
                    new TalkData(TalkEmotion.IDLE, "안녕?"),
                    new TalkData(TalkEmotion.TALK, "이 곳에 처음 왔구나?"),
                    new TalkData(TalkEmotion.SMILE, "만나서 반가워"),
                    new TalkData(TalkEmotion.ANGRY, "너무 더워!"),
                }
            },
            {
                2000,
                new TalkData[]{
                    new TalkData(TalkEmotion.IDLE, "여어"),
                    new TalkData(TalkEmotion.ANGRY, "짜증나!"),
                    new TalkData(TalkEmotion.SMILE, "이 호수는 정말 아름답지?"),
                    new TalkData(TalkEmotion.TALK, "사실 이 호수에는 무언가의 비밀이 숨겨져 있다고 해."),
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
