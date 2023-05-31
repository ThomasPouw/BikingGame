using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using Firebase.Extensions;
using UnityEngine.UI;
using System;

public class ImageStorage : MonoBehaviour
{
    public static Dictionary<string, Sprite> Images;
    FirebaseStorage storage;
    StorageReference storageRef;
    // Start is called before the first frame update
    void Start()
    {
        if(Images == null){
            Images = new Dictionary<string, Sprite>();
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
    public async void DownloadPicture(string imageName, Image image){
        try{
            Texture2D texture = new Texture2D(200, 200);
        if(Images.ContainsKey(imageName)){
            image.sprite = Images[imageName];
        }
        else{
            StorageReference pathReference = storage.GetReference("images/"+ imageName);
            const long maxAllowedSize = 1 * 1024 * 1024;
            DateTime time = DateTime.Now;
            await pathReference.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task => {
                if (task.IsFaulted || task.IsCanceled) 
                {
                    Debug.LogException(task.Exception);
                    return;
                    // Uh-oh, an error occurred!
                }
                else {
                    Debug.Log(imageName+ ": "+ (DateTime.Now - time).ToString());
                    if(!Images.ContainsKey(imageName)){
                        StartCoroutine(SetPicture(texture, imageName, image, task.Result));
                    }
                    else{
                        image.sprite = Images[imageName];
                    }
                }
            });
        }
        }
        catch(Exception E){
            Debug.LogError(E);
        }
    }
    public IEnumerator SetPicture(Texture2D texture, string imageName, Image image, byte[] result){
        texture.LoadImage(result);      
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width/2, texture.height/2));
        Images.Add(imageName, sprite);
        Debug.Log(imageName+": " +result.Length);
        image.sprite = sprite;
        yield return null;
        //yield return new WaitForSeconds(0.5f);
    }
}
