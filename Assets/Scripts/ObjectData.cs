using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static TalkManager;

public class ObjectData : MonoBehaviour
{   
    public int id;
    public bool isNpc;
    public int talkIndex = 0;
    public TalkTypeSpritePair[] portraitSprites;

    Dictionary<TalkEmotion, Sprite> portraitDictionary;

    internal Sprite GetPortraitSprite(TalkEmotion talkType)
    {
        LoadData();
        
        if (!(portraitDictionary.ContainsKey(talkType)))
        {
            return null;
        }
        else
        {
            return portraitDictionary.GetValueOrDefault(talkType);
        }
    }

    private void Awake()
    {
        LoadData();
    }

    void LoadData()
    {
        if (portraitDictionary != null)
        {
            return;
        }

        portraitDictionary = new Dictionary<TalkEmotion, Sprite>();
        if (portraitSprites != null)
            foreach (TalkTypeSpritePair pair in portraitSprites)
                portraitDictionary[pair.talkType] = pair.sprite;
    }
}

[System.Serializable]
public class TalkTypeSpritePair
{
    public TalkEmotion talkType;
    public Sprite sprite;
}


[CustomEditor(typeof(ObjectData))]
public class YourComponentEditor : Editor
{
    private SerializedProperty portraitSpritesProperty;

    private void OnEnable()
    {
        portraitSpritesProperty = serializedObject.FindProperty("portraitSprites");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(portraitSpritesProperty, true);

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
