using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using Firebase.Extensions;
using UnityEngine.UI;
using System;

public class ImageStorage : MonoBehaviour
{
    public static Dictionary<string, byte[]> Images;
    FirebaseStorage storage;
    StorageReference storageRef;
    // Start is called before the first frame update
    void Start()
    {
        if(Images == null){
            Images = new Dictionary<string, byte[]>();
        }
        storage = FirebaseStorage.DefaultInstance;
        storageRef = storage.GetReferenceFromUrl("gs://bikinggame-3cabe.appspot.com");
        //DownloadPicture("CV_Foto.jpg");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UploadPicture(string file){
        //Maybe?
    }
    public void DownloadPicture(string imageName, Image image){
        try{
            Texture2D texture = new Texture2D(50, 50);
        if(Images.ContainsKey(imageName)){
            texture.LoadImage(Images[imageName]);
            image.sprite= Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width/2, texture.height/2));
        }
        else{
            StorageReference pathReference = storage.GetReference("images/"+ imageName);
            const long maxAllowedSize = 1 * 1024 * 1024;
            pathReference.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task => {
                if (task.IsFaulted || task.IsCanceled) 
                {
                    Debug.LogException(task.Exception);
                    return;
                    // Uh-oh, an error occurred!
                }
                else {
                    Debug.Log(task.Result);
                    Debug.Log("DownloadPicture: "+imageName);
                    Images.Add(imageName, task.Result);
                    Debug.Log("Finished downloading!");
                    texture.LoadImage(Images[imageName]);
                    
                    image.sprite= Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width/2, texture.height/2));
                    //image.sprite= Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width/2, texture.height/2));
                    //texture.Apply();
                }
            });
        }
        }
        catch(Exception E){
            Debug.LogError(E);
        }
    }
}
