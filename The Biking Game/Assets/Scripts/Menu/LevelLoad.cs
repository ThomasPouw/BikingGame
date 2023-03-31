using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoad : MonoBehaviour
{
    [SerializeField] public bool foundLevels = false;
    public void LoadLevel(MenuInfo menuInfo){
        Debug.Log(menuInfo);
        StaticMachine.menuInfo.SetValue(menuInfo);
        SceneManager.LoadScene("Level");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(LevelStorage.JSONlevelSizes.Count != 0 && !foundLevels){
            foundLevels = true;
            GameObject LevelHolder = GameObject.Find("LevelHolder");
            int levelPanelCounter = 0;
            foreach (JSONLevelSize jSONLevelSize in LevelStorage.JSONlevelSizes)
            {
                Debug.Log(jSONLevelSize);
                GameObject LevelPanel = LevelHolder.transform.GetChild(0).gameObject;
                levelPanelCounter++;
                if(LevelPanel != null){
                    Debug.Log(LevelPanel);
                    LevelPanel.SetActive(true);
                    //LevelPanel.transform.Find("LevelName").GetComponent<TMP_Text>().text = jSONLevelSize.levelName;
                    LevelPanel.GetComponent<MenuInfo>().SetValue(jSONLevelSize.levelName, 0);
                }
            }
            
        }
    }
}
