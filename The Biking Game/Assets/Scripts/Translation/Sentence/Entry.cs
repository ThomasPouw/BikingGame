using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entry
{
    [SerializeField] public string OriginalLine;
    [SerializeField] public string TranslatedLine;
    [SerializeField] public string[] HelperImages;
    [SerializeField] public bool IsCorrect;
}
[System.Serializable]
public class Question
{
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

