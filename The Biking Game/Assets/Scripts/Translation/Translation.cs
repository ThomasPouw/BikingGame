using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour
{
    public static Language currentLanguage;
    public string jsonTest;
    // Start is called before the first frame update
    void Start()
    {
        ImportLanguage(jsonTest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ImportLanguage(string JSONLanguage)
    {
        Language ImportLanguage = JsonUtility.FromJson<Language>(JSONLanguage);
        Debug.Log(JsonUtility.ToJson(ImportLanguage));
        Debug.Log(ImportLanguage);
        Debug.Log(ImportLanguage.LanguageName);
        Debug.Log(ImportLanguage.LanguageName);
        //Database stuff!
    }
    public void EditLanguage(string JSONLanguage, string LanguageName)
    {
        Language ImportLanguage = JsonUtility.FromJson<Language>(JSONLanguage);
        //Database stuff!
    }
    public void DeleteLanguage(string languageName)
    {
        if(currentLanguage.LanguageName == languageName){
            //Give error to prevent no language selected.
        }
        //Language ImportLanguage = JsonUtility.FromJson<Language>(JSONLanguage);
        //Database stuff!
    }
    public Language ReadLanguage(string languageName){
        if(currentLanguage.LanguageName == languageName){
            return currentLanguage;
        }
        return null;
    }
    public string[] ReadLanguageOptions(){
        return null;
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
