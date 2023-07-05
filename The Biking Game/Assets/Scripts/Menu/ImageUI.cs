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
    public void SelectedImage(GameObject selected){
        selected.GetComponent<Animator>().SetTrigger("Clicked");
        if(_selectedImage != null){
            _selectedImage.GetComponent<Animator>().SetTrigger("Clicked");
            if(_selectedImage.name == selected.name){
                GameObject.Find("Delete").GetComponent<Button>().interactable = false;
                _selectedImage = null;
                return;
            }
        }
        GameObject.Find("Delete").GetComponent<Button>().interactable = true;
        _selectedImage = selected;
    }
    [SerializeField]private GameObject _selectedImage;
    // Start is called before the first frame update
    void Start()
    {
        _imageStorage = GameObject.Find("Storage").GetComponent<ImageStorage>();
        _imageNames = new List<string>();
        _images = new List<GameObject>();
        LoadImages();
    }

    public async void LoadImages()
    {
        await _imageStorage.ListAll().ContinueWith(result => {
            _imageNames = result.Result;
            
        });
        foreach(string ImageName in _imageNames){
            Debug.Log(ImageName);
            GameObject image = Instantiate(_imageHolder);
            _imageStorage.DownloadPicture(ImageName, image.transform.GetChild(0).GetComponent<Image>());
            image.name = ImageName;
            image.transform.parent = _imagePlacement.transform;
            image.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => SelectedImage(image));
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
        _imageStorage.UploadPicture(imageBytes, imageName, _imageNames.Count);
        GameObject image = Instantiate(_imageHolder);

        Texture2D texture = new Texture2D(200, 200);
        StartCoroutine(_imageStorage.SetPicture(texture,imageName, image.transform.GetChild(0).GetComponent<Image>(), imageBytes));
        image.name = imageName;
        image.transform.parent = _imagePlacement.transform;
        ImageUI imageUI = this.GetComponent<ImageUI>();
        image.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => imageUI.SelectedImage(image));
        _imageNames.Add(imageName);
        _images.Add(image);

    }
    public void SaveImagesFiles(){
        
        filePicker.ImageFile(this, true);
    }
    public void DeleteImagesFiles(){
        if(_selectedImage != null){ 
            int index = _imageNames.FindIndex(x => x == _selectedImage.name);
            Debug.Log(index);
            Debug.Log(_selectedImage.name);
            _imageNames.Remove(_selectedImage.name);
            _imageStorage.DeletePicture(_selectedImage.name, _imageNames);
            Destroy(_selectedImage);
            _selectedImage = null;
        }
    }
}
