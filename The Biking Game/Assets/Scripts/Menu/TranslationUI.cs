using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TranslationUI : MonoBehaviour
{
    [SerializeField] TMP_Dropdown languageDropdown;
    [SerializeField] public Language SelectedLanguage;
    [SerializeField] public Language ImportedLanguage;
    [SerializeField] public FilePicker filePicker;
    [SerializeField] GameObject ParentOfScrollviews;

    void Start()
    {
        foreach(string language in TranslationStorage.LanguageOptions){
            languageDropdown.options.Add(new TMP_Dropdown.OptionData(language));
        }
        SelectedLanguage = TranslationStorage.AllLanguages.Find(x => x.LanguageName == languageDropdown.options[0].text);
        languageDropdown.RefreshShownValue();
        DisplayTranslation(true);
    }
    public void ImportLanguageFiles(){
        
        filePicker.TranslationFile(this, true);
    }
    public void ExportLanguageFiles(){
        filePicker.TranslationFile(this,false);
        /*string path = EditorUtility.SaveFilePanel("Export Language file", "",TranslationStorage.CurrentLanguageName + ".json", "json");
        if (path.Length != 0)
        {
            var languageData = JsonUtility.ToJson(TranslationStorage.CurrentLanguage, true);
            if (languageData != null)
                File.WriteAllBytes(path, System.Text.Encoding.UTF8.GetBytes(languageData));
        }*/
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
    public void DisplayTranslation(bool Left)
    {
        if(Left){
            ParentOfScrollviews.transform.GetChild(0).Find("Scroll View").GetComponentInChildren<TMP_Text>().text = JsonUtility.ToJson(SelectedLanguage, true);
        }
        else{
            ParentOfScrollviews.transform.GetChild(1).gameObject.SetActive(true);
            ParentOfScrollviews.transform.GetChild(1).Find("Scroll View").GetComponentInChildren<TMP_Text>().text = JsonUtility.ToJson(ImportedLanguage, true);
        }
    }
}
