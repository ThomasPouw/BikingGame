using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] public TMP_Text ReturnButton;
    [SerializeField] public TMP_Text PointsText;
    [SerializeField] public GameObject GameScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LoadLevelSelect(){
        //StaticMachine.menuInfo.SetValue(menuInfo);
        SceneManager.LoadScene("Menu");
    }
    public void ShowEndScreen(){
        GameScreen = GameObject.Find("PointsSystem");
        float points = GameScreen.GetComponent<PointComboUI>().Points;
        PointsText.text = new Translation().TranslateSentence("PointText", "LevelUI").TranslatedLine + points;
        GameScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
