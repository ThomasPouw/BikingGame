using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using Firebase.Extensions;
using UnityEngine.UI;
using System;
using Firebase.Database;
using System.Threading.Tasks;

public class ImageStorage : MonoBehaviour
{
    public static Dictionary<string, Sprite> Images;
    FirebaseStorage storage;
    StorageReference storageRef;
    DatabaseReference dataBaseRef;
    bool isConnected = false;

    // Start is called before the first frame update
    private void OnEnable() {
        if(Images == null){
            Images = new Dictionary<string, Sprite>();
        }
        storage = FirebaseStorage.DefaultInstance;
        storageRef = storage.GetReferenceFromUrl("gs://bikinggame-3cabe.appspot.com");
        dataBaseRef = FirebaseDatabase.GetInstance("https://bikinggame-3cabe-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
        //dataBaseRef.Database.GoOnline();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void UploadPicture(byte[] imageBytes, string imageName, int AmountofPictures)
    {
        string ImageType = imageName.Split('.')[1];
        switch (ImageType)
        {
            case("jpg"):
                ImageType = $"image/jpeg";
                break;
            case("jpeg"):
                ImageType = $"image/jpeg";
                break;
            case("png"):
                ImageType = $"image/png";
                break;
            default:
                Debug.LogError($"Selected type \"{ImageType}\" is not supported!");
                return;
        }
        StorageReference pathReference = storage.GetReference("images/"+ imageName);
        MetadataChange newMetadata = new MetadataChange()
        {
            ContentType = ImageType
        };
        await pathReference.PutBytesAsync(imageBytes, newMetadata);
        await dataBaseRef.Child("ImageNames/"+AmountofPictures).SetValueAsync(imageName);
    }
    public async void DeletePicture(string imageName, List<string> imageNames)
    {
        StorageReference pathReference = storage.GetReference("images/"+ imageName);
        await dataBaseRef.Child($"ImageNames/").SetValueAsync(imageNames).ContinueWithOnMainThread(task => {
            if (task.IsCompleted) {
            }
            else {
                Debug.LogException(task.Exception);
                return;
            }
        });
        await pathReference.DeleteAsync().ContinueWithOnMainThread(task => {
            if (task.IsCompleted) {
            }
            else {
                Debug.LogException(task.Exception);
                return;
            }
        });
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
        
    }


    //This is needed as there is no way to collect the image folder in firebase Storage (Unity). 
    //There is for Andriod, but not yet for Unity.
    public async Task<List<string>> ListAll(){
        List<string> AllImageNames = new List<string>();
        try{
            new WaitForSeconds(0.5f);
            await dataBaseRef.Child("ImageNames/").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted || task.IsCanceled) {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted) {
                Debug.Log("Mission complete!");
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.GetRawJsonValue());
                foreach(DataSnapshot child in snapshot.Children){
                    AllImageNames.Add(child.Value.ToString());
                }
        }
      });
      return AllImageNames;
        }
        catch(Exception E){
          Debug.LogError(E);
          return null;
        }

    }
}
