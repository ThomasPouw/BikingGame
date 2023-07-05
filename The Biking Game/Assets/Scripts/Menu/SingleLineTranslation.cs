using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleLineTranslation : MonoBehaviour
{
    public Entry entry;
    public string DictionaryType;
    public bool onVisibleTranslate;

    private TMP_Text _displayText;
    private void OnEnable() {
        _displayText = GetComponent<TMP_Text>();
    }
    private void Start() {
        if(entry != null){
           translate();
        }
        else
        {
            Debug.LogError("Forgot the Translation! At "+ gameObject.name);
        }
    }
    private void Update() {
        if(onVisibleTranslate){
            translate();
            onVisibleTranslate = false;
        }
    }
    public void translate(){
        StartCoroutine(new Translation().TranslateSentence(entry.OriginalLine, DictionaryType, (entry)=> {
            _displayText.text = entry.TranslatedLine;
           }));
    }
    public void switchLanguagetranslate(){
        if(isActiveAndEnabled){
            translate();
        }
        else{
            onVisibleTranslate = true;
        }
    }
}
