using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;

public class TranslationStorage : MonoBehaviour
{
    DatabaseReference reference;
    [SerializeField] string json;
    [SerializeField] string languageName;
    // Start is called before the first frame update
    void Start()
    {
       reference = FirebaseDatabase.GetInstance("https://bikinggame-3cabe-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
        writeNewTranslation(json, languageName);
        downloadTranslation(languageName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void downloadTranslation(string languageName){
        reference.Child("translation/"+ languageName)
      .GetValueAsync().ContinueWithOnMainThread(task => {
        if (task.IsFaulted) {
          // Handle the error...
        }
        else if (task.IsCompleted) {
          DataSnapshot snapshot = task.Result;
          Debug.Log(snapshot);
          Debug.Log(snapshot.ChildrenCount);
          Debug.Log(snapshot.HasChildren);
          Language t = (Language)snapshot.Value;
          Debug.Log(t.dictionary);
          // Do something with snapshot...
        }
        Debug.Log(task.Status);
      });
    }
    public void writeNewTranslation(string translationFile, string languageName){
        reference.Child("translation/"+ languageName).SetRawJsonValueAsync(translationFile);
    }
}
