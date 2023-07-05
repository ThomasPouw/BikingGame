using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;

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
        if(JSONlevelSizes == null){
            JSONlevelSizes = new List<JSONLevelSize>();
        }
        isDatabaseOnline((online) => {
            ReadLevels();
        });
            
        
    }
    public async void isDatabaseOnline(System.Action<bool> callback){
      DatabaseReference DataRef = FirebaseDatabase.GetInstance("https://bikinggame-3cabe-default-rtdb.europe-west1.firebasedatabase.app/").GetReference(".info/connected");
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
                if(s_loopCount == 50){}
                else if(s_isConnected == false){
                    await isDatabaseOnlineLoop(database);
                }
            }
        });
        return s_isConnected;
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
        JSONLevelSize JLS = JSONlevelSizes.Find(x => x.levelName == LevelName);
        JSONlevelSizes.Remove(JLS);
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
