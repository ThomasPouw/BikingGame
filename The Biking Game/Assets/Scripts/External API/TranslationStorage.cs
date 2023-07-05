using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;
using System;

public class TranslationStorage : MonoBehaviour
{
    DatabaseReference reference;
    [SerializeField] public Language LanguageCheatSheet;
    [SerializeField] public TextAsset json;
    [SerializeField] public static List<string> LanguageOptions;
    [SerializeField] public static List<Language> AllLanguages;
    [SerializeField] public static Dictionary<string, Dictionary<string, Entry>> CurrentLanguage;
    [SerializeField] public static string CurrentLanguageName;

    [SerializeField] private ImageStorage _imageStorage;

    [SerializeField] public static bool s_isConnected;
    [SerializeField] private static int s_loopCount = 0;
    
    private void OnEnable() {
      
      s_isConnected = false;
      reference = FirebaseDatabase.GetInstance("https://bikinggame-3cabe-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
      Debug.Log("TranslationSystem started!");
      if(AllLanguages == null){
          AllLanguages = new List<Language>();
          LanguageOptions = new List<string>();
          CurrentLanguage = new Dictionary<string, Dictionary<string, Entry>>();
      }
      isDatabaseOnline((online) => {
        if(!online){
          Debug.LogError("DataBase never went online!");
          return;
        }
          ReadAllLanguages();
      });
      
    }
    public async void isDatabaseOnline(System.Action<bool> callback){
      DatabaseReference DataRef = FirebaseDatabase.GetInstance("https://bikinggame-3cabe-default-rtdb.europe-west1.firebasedatabase.app/").GetReference(".info/connected");
      callback(await isDatabaseOnlineLoop(DataRef));
      await isDatabaseOnlineLoop(DataRef).ContinueWith((online) => {
        callback(online.Result);
      });
    }
    public async Task<bool> isDatabaseOnlineLoop(DatabaseReference database){
      await database.GetValueAsync().ContinueWith(async task => {
        s_loopCount +=1;
        if (task.IsFaulted || task.IsCanceled) {
          await isDatabaseOnlineLoop(database);
        }
        else if (task.IsCompleted){
          s_isConnected = (bool)task.Result.Value;
          Debug.Log(s_loopCount + " "+ s_isConnected);
          if(s_loopCount == 10){
          }
          else if(s_isConnected == false){
            await isDatabaseOnlineLoop(database);
          }
        }
        });
        return s_isConnected;
    }
    public void selectCurrentLanguage(string languageName)
    {
      if(!CurrentLanguage.ContainsKey(languageName))
      {
        foreach(Language language in AllLanguages){
          if(language.LanguageName == languageName){
            MakeDictionaryForCurrent(language);
            return;
          }
        }
      }
    }
    public void ReadAllLanguages(){
        try{
          Debug.Log("Reading files...");
        reference.Child("translation/").GetValueAsync().ContinueWith(task => {
        if (task.IsFaulted || task.IsCanceled) {
          Debug.LogError(task.Exception);
          return;
        }
        else if (task.IsCompleted) {
          Debug.Log("Mission complete!");
          DataSnapshot snapshot = task.Result;
          Debug.Log(snapshot.GetRawJsonValue());
          //AllLanguages = JsonUtility.FromJson<List<Language>>(snapshot.GetRawJsonValue());
          if(snapshot.ChildrenCount != AllLanguages.Count)
          foreach (DataSnapshot child in snapshot.Children)
          {
              Language loadLanguage = JsonUtility.FromJson<Language>(child.GetRawJsonValue());
              AllLanguages.Add(loadLanguage);
              LanguageOptions.Add(loadLanguage.LanguageName);
          }
          //selectCurrentLanguage(AllLanguages[0].LanguageName);
        }
      }).ContinueWith(task => {selectCurrentLanguage(AllLanguages[0].LanguageName);});
        }
        catch(Exception E){
          Debug.LogError(E);
        }
    }
    public void writeNewTranslation(string translationFile, string languageName){
        reference.Child("translation/"+languageName).SetRawJsonValueAsync(translationFile);
    }
    public void deleteTranslation(string languageName){
        reference.Child("translation/"+languageName).RemoveValueAsync();
    }
    public void MakeDictionaryForCurrent(Language currentListLanguage){
      Dictionary<string, Entry> _translationDictionary = new Dictionary<string, Entry>();
       Debug.Log(JsonUtility.ToJson(currentListLanguage));
      CurrentLanguage = new Dictionary<string, Dictionary<string, Entry>>();
      CurrentLanguageName = currentListLanguage.LanguageName;
      try{
        foreach(LanguageDictionary LD in currentListLanguage.dictionary){
          //Debug.Log(LD);
          //CurrentLanguage.Add(LD.DictionaryType, new Dictionary<string, Entry>());
          foreach(Entry E in LD.TranslationDictionary){
              //Debug.Log(E.OriginalLine);
              _translationDictionary.Add(E.OriginalLine, E);
              //if(E.HelperImages.Length != 0)
              //_imageStorage.DownloadPicture(E.HelperImages);
          }
          //Debug.Log(LD.DictionaryType);
          CurrentLanguage.Add(LD.DictionaryType, _translationDictionary);
          _translationDictionary = new Dictionary<string, Entry>();

        }
      }
      catch(Exception E){
        Debug.LogError(E.Data);
        Debug.LogError(E);
      }
    }
}
