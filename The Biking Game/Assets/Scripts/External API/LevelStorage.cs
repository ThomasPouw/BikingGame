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

    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.GetInstance("https://bikinggame-3cabe-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
        if(JSONlevelSizes == null){
            JSONlevelSizes = new List<JSONLevelSize>();
            ReadLevels();
        }
    }
    public void SaveLevel(JSONLevelSize JSONlevelSize){
        reference.Child("level/"+ JSONlevelSize.levelName).SetRawJsonValueAsync(JsonUtility.ToJson(JSONlevelSize));
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
                Debug.Log("Level Mission complete!");
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.GetRawJsonValue());
                foreach (DataSnapshot child in snapshot.Children)
                {
                    JSONLevelSize loadLevel = JsonUtility.FromJson<JSONLevelSize>(child.GetRawJsonValue());
                    JSONlevelSizes.Add(loadLevel);
                }
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
    

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable() {
        reference = FirebaseDatabase.GetInstance("https://bikinggame-3cabe-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
        if(JSONlevelSizes == null){
            ReadLevels();
        }
    }
}
