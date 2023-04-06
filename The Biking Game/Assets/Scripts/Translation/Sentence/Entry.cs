using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Entry
{
    [SerializeField] public string OriginalLine;
    [SerializeField] public string TranslatedLine;
    [SerializeField] public string[] HelperImages;
    [SerializeField] public bool IsCorrect;
    public void SetValue(string translationLine, string[] helperImages){
        TranslatedLine = translationLine;
        HelperImages =helperImages;
    }
}
[System.Serializable]
public class Question
{
    [SerializeField] public Guid EntryID;
    [SerializeField] public string OriginalLine;
    [SerializeField] public Entry Entry;
}
[System.Serializable]
public class Answer
{
    [SerializeField] public string OriginalLine;
    [SerializeField] public Entry Entry;
    [SerializeField] public bool IsCorrect;
}

