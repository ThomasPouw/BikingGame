using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageUI : MonoBehaviour
{
    [SerializeField] private GameObject _imageHolder;
    [SerializeField] private GameObject _imagePlacement;
    [SerializeField] public FilePicker filePicker;
    private ImageStorage _imageStorage;
    private List<string> _imageNames;
    private List<GameObject> _images;
    // Start is called before the first frame update
    void Start()
    {
        _imageStorage = GameObject.Find("Storage").GetComponent<ImageStorage>();
        _imageNames = new List<string>();
        _images = new List<GameObject>();
        LoadImages();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void LoadImages()
    {
        await _imageStorage.ListAll().ContinueWith(result => {
            _imageNames = result.Result;
            
        });
        foreach(string ImageName in _imageNames){
            Debug.Log(ImageName);
            GameObject image = Instantiate(_imageHolder);
            _imageStorage.DownloadPicture(ImageName, image.GetComponent<Image>());
            image.name = ImageName;
            image.transform.parent = _imagePlacement.transform;
            _images.Add(image);
        }
    }
    public void ImageSearch(TMP_InputField field)
    {
        string searchTerm = field.text;
        foreach (GameObject gameObject in _images)
        {
            if(gameObject.name.Contains(searchTerm)){
                gameObject.SetActive(true);
            }
            else{
                gameObject.SetActive(false);
            }
        }
    }
    public void setImage(byte[] imageBytes, string imageName){
        Debug.Log("Debug.Log(_imageNames.Count)"+ _imageNames.Count);
        Debug.Log("imageBytes"+ imageBytes);
        Debug.Log("imageBytes"+ imageName);
        Debug.Log("_imageStorage"+ _imageStorage);
        _imageStorage.UploadPicture(imageBytes, imageName, _imageNames.Count);
    }
    public void ImportLanguageFiles(){
        
        filePicker.ImageFile(this, true);
    }
    public void ExportLanguageFiles(){
        filePicker.ImageFile(this,false);
    }
}
