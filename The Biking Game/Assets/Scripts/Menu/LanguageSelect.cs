using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageSelect : MonoBehaviour
{
    [SerializeField] TMP_Dropdown languageDropdown;
    [SerializeField] TranslationStorage translationStorage;
    [SerializeField] bool LoadedLanguage = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(TranslationStorage.LanguageOptions.Count != 0 && LoadedLanguage == false){
            foreach(string language in TranslationStorage.LanguageOptions)
            {
                languageDropdown.options.Add(new TMP_Dropdown.OptionData(language));
            } 
            LoadedLanguage = true;
            //languageDropdown.value = TranslationStorage.LanguageOptions.FindIndex(x => x == TranslationStorage.CurrentLanguageName);
            languageDropdown.RefreshShownValue();
        }
    }
    public void setLanguage(){
        string languageName = TranslationStorage.LanguageOptions[languageDropdown.value];
        translationStorage.selectCurrentLanguage(languageName);
    }
}