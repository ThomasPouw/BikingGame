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
           StartCoroutine(new Translation().TranslateSentence(entry.OriginalLine, DictionaryType, (entry)=> {
            _displayText.text = entry.TranslatedLine;
           }));
           //CALLBACK!
           //_displayText.text = entry.TranslatedLine;
        }
        else
        {
            Debug.LogError("Forgot the Translation! At "+ gameObject.name);
        }
    }
}
