using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using Firebase.Extensions;
using UnityEngine.UI;

public class ImageStorage : MonoBehaviour
{
    FirebaseStorage storage;
    StorageReference storageRef;
    // Start is called before the first frame update
    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        storageRef = storage.GetReferenceFromUrl("gs://bikinggame-3cabe.appspot.com");
        DownloadPictures(new string[1]{"CV_Foto.jpg"});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UploadPicture(string file){
        //Maybe?
    }
    public void DownloadPictures(string[] imageNames){
        foreach (string imageName in imageNames)
        {
            StorageReference pathReference = storage.GetReference("images/"+ imageName);
            //StorageReference httpsReference = storage.GetReferenceFromUrl("https://firebasestorage.googleapis.com/b/bucket/o/images%20"+ imageName);
            string localUrl = "file:///Resources/images/"+ imageName;
            pathReference.GetFileAsync(localUrl).ContinueWithOnMainThread(task => {
                if(!task.IsFaulted && !task.IsCanceled){
                    Debug.Log("File DownLoaded");
                }
                else{
                    Debug.LogError(task.Status);
                }
            });
        }
    }
    public Image LoadPictureFromLocal(string ImageName){
       return Resources.Load<Image>("images/"+ ImageName);
    }
}
