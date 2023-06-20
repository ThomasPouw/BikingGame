using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class LevelStorage : MonoBehaviour
{
    DatabaseReference reference;
    public static List<JSONLevelSize> JSONlevelSizes;
    [SerializeField] public static JSONLevelSize JSONlevelSize;
    [SerializeField] LevelSize levelSize;
    [SerializeField] public static bool s_isConnected;
    [SerializeField] private static int s_loopCount = 0;

    // Start is called before the first frame update
    private void OnEnable() {
        reference = FirebaseDatabase.GetInstance("https://bikinggame-3cabe-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
        isDatabaseOnline();
        if(JSONlevelSizes == null){
            JSONlevelSizes = new List<JSONLevelSize>();
            ReadLevels();
        }
    }
    public void isDatabaseOnline(){
      DatabaseReference DataRef = FirebaseDatabase.GetInstance("https://bikinggame-3cabe-default-rtdb.europe-west1.firebasedatabase.app/").GetReference(".info/connected");
      isDatabaseOnlineLoop(DataRef);
    }
    public async void isDatabaseOnlineLoop(DatabaseReference database){
      await database.GetValueAsync().ContinueWith(task => {
        s_loopCount +=1;
        if (task.IsFaulted || task.IsCanceled) {
          isDatabaseOnlineLoop(database);
        }
        else if (task.IsCompleted){
          s_isConnected = (bool)task.Result.Value;
          Debug.Log(s_loopCount + " "+ s_isConnected);
          if(s_loopCount == 10){
          }
          else if(s_isConnected == false){
            isDatabaseOnlineLoop(database);
          }
        }
        });
    }
    public void SaveLevel(JSONLevelSize JSONlevelSize){
        try{
            reference.Child("level/"+ JSONlevelSize.levelName).SetRawJsonValueAsync(JsonUtility.ToJson(JSONlevelSize));
        }
        catch(Exception E){
            Debug.LogError(E);
        }
    }
    public void DeleteLevel(string LevelName){
        reference.Child("level/"+LevelName).RemoveValueAsync();
    }
    public void ReadLevels(){
        try{
            reference.Child("level/").GetValueAsync().ContinueWithOnMainThread(task => 
            {
                if (task.IsFaulted || task.IsCanceled) {
                    Debug.LogError(task.Exception);
                    return;
                }
                else if (task.IsCompleted) 
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log(snapshot.GetRawJsonValue());
                    List<JSONLevelSize> tempJsonLevelSize = new List<JSONLevelSize>();
                    foreach (DataSnapshot child in snapshot.Children)
                    {
                        JSONLevelSize loadLevel = JsonUtility.FromJson<JSONLevelSize>(child.GetRawJsonValue());
                        tempJsonLevelSize.Add(loadLevel);
                    }
                    JSONlevelSizes = tempJsonLevelSize;
                    s_isConnected = true;
                }
            }
        );
        }
        catch(Exception E){
            Debug.LogError(E);
        }
        
    }
    public JSONLevelSize ReadLevel(string LevelName){
        JSONlevelSize = JSONlevelSizes.Find(x => x.levelName == LevelName);
        return JSONlevelSize;
    }

}
