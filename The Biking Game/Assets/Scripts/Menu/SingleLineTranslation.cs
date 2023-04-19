using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleLineTranslation : MonoBehaviour
{
    public Entry entry;
    public string DictionaryType;

    private TMP_Text _displayText;
    private void OnEnable() {
        _displayText = GetComponent<TMP_Text>();
    }
    private void Start() {
        if(entry != null){
           entry = new Translation().TranslateSentence(entry.OriginalLine, DictionaryType);
           _displayText.text = entry.TranslatedLine;
        }
        else
        {
            Debug.LogError("Forgot the Translation! At "+ gameObject.name);
        }
    }
}
