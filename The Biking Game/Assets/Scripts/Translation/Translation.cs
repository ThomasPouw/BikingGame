using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation
{
    public TextAsset jsonTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ImportLanguage(TextAsset JSONLanguage)
    {
        new TranslationStorage().writeNewTranslation(JSONLanguage.text, JSONLanguage.name);
    }
    /*public void DeleteLanguage(string languageName)
    {
        if(currentLanguage.LanguageName == languageName){
            //Give error to prevent no language selected.
        }
        //Language ImportLanguage = JsonUtility.FromJson<Language>(JSONLanguage);
        //Database stuff!
    }*/
    public Entry TranslateSentence(string sentence, string dictionaryType){
        Debug.Log(TranslationStorage.CurrentLanguage[dictionaryType][sentence].OriginalLine);
        Debug.Log(TranslationStorage.CurrentLanguage[dictionaryType][sentence].TranslatedLine);
        return TranslationStorage.CurrentLanguage[dictionaryType][sentence];
    }
}
[System.Serializable]
public class Language
{
    
    public string LanguageName;
    public LanguageDictionary[] dictionary;
}
[System.Serializable]
public class LanguageDictionary{
    public string DictionaryType;
    public Entry[] TranslationDictionary;
}
