using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System.IO;

public class TranslationUI : MonoBehaviour
{
    [SerializeField] TMP_Dropdown languageDropdown;
    [SerializeField] Language ImportedLanguage;
    //[SerializeField] TranslationStorage _translationStorage;
    // Start is called before the first frame update
    //[SerializeField] TMP_Text ExistingText;
    [SerializeField] TMP_Text ScrollText;
    void Start()
    {
        foreach(string language in TranslationStorage.LanguageOptions){
            languageDropdown.options.Add(new TMP_Dropdown.OptionData(language));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ImportLanguageFiles(){
        string path = EditorUtility.OpenFilePanel("Import Language file", "", "json");
        if(path != null){
            var fileContent = File.ReadAllBytes(path);
            ImportedLanguage = JsonUtility.FromJson<Language>(System.Text.Encoding.UTF8.GetString(fileContent));
            DisplayTranslation(false);
        }
    }
    public void ExportLanguageFiles(){
        string path = EditorUtility.SaveFilePanel("Export Language file", "",TranslationStorage.CurrentLanguageName + ".json", "json");
        if (path.Length != 0)
        {
            var languageData = JsonUtility.ToJson(TranslationStorage.CurrentLanguage, true);
            if (languageData != null)
                File.WriteAllBytes(path, System.Text.Encoding.UTF8.GetBytes(languageData));
        }
    }
    public void SaveTranslation(){
        new Translation().ImportLanguage(ImportedLanguage);
    }
    public void EditTranslation(){
        new Translation().EditLanguage(ImportedLanguage, languageDropdown.options[languageDropdown.value].text);
    }
    public void DeleteTranslation(){
        new Translation().DeleteLanguage(languageDropdown.options[languageDropdown.value].text);
    }
    public void DisplayTranslation(bool Lefttext)
    {
        ScrollText.text = JsonUtility.ToJson(ImportedLanguage, true);
    }
}
