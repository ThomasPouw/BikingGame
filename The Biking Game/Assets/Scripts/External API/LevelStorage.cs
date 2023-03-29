using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class LevelStorage : MonoBehaviour
{
    DatabaseReference reference;
    public static List<JSONLevelSize> JSONlevelSizes;
    public static JSONLevelSize JSONlevelSize;
    [SerializeField] LevelSize levelSize;

    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.GetInstance("https://bikinggame-3cabe-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
        SaveLevel(new JSONLevelSize(levelSize));
    }
    public void SaveLevel(JSONLevelSize JSONlevelSize){
        reference.Child("level/"+ JSONlevelSize.levelName).SetRawJsonValueAsync(JsonUtility.ToJson(JSONlevelSize));
    }
    public void DeleteLevel(string LevelName){
        reference.Child("level/"+LevelName).RemoveValueAsync();
    }
    public void ReadLevels(JSONLevelSize JSONlevelSize){
        reference.Child("level/").GetValueAsync().ContinueWithOnMainThread(task => 
        {
            if (task.IsFaulted || task.IsCanceled) {
                Debug.LogError(task.Exception);
                return;
            }
            else if (task.IsCompleted) 
            {
                Debug.Log("Mission complete!");
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.GetRawJsonValue());
                JSONlevelSizes = JsonUtility.FromJson<List<JSONLevelSize>>(snapshot.GetRawJsonValue());
                //foreach (DataSnapshot child in snapshot.Children)
                //{
                    // Language loadLanguage = JsonUtility.FromJson<Language>(child.GetRawJsonValue());
                    // Debug.Log(loadLanguage.LanguageName);
                    // JSONlevelSizes.Add(loadLanguage);
                    //LanguageOptions.Add(loadLanguage.LanguageName);
                //}
                //selectCurrentLanguage(AllLanguages[0].LanguageName);
                }
            }
        );
    }
    public void ReadLevel(string LevelName){
        JSONlevelSize = JSONlevelSizes.Find(x => x.levelName == LevelName);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
