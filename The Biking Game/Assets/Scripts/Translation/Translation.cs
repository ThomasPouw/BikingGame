using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation
{
    public TextAsset jsonTest;
    // Start is called before the first frame update
    public void ImportLanguage(Language JSONLanguage)
    {
        new TranslationStorage().writeNewTranslation(JsonUtility.ToJson(JSONLanguage), JSONLanguage.LanguageName);
    }
    public void EditLanguage(Language JSONLanguage, string EditLanguageName)
    {
        if(JSONLanguage.LanguageName == EditLanguageName){
            new TranslationStorage().writeNewTranslation(JsonUtility.ToJson(JSONLanguage), JSONLanguage.LanguageName);
        }
        else
        {
            new TranslationStorage().deleteTranslation(EditLanguageName);
            new TranslationStorage().writeNewTranslation(JsonUtility.ToJson(JSONLanguage), JSONLanguage.LanguageName);
        }
    }
    public void DeleteLanguage(string DeleteLanguageName)
    {
        new TranslationStorage().deleteTranslation(DeleteLanguageName);
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
