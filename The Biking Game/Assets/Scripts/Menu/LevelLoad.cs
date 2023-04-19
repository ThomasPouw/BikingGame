using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoad : MonoBehaviour
{
    [SerializeField] public bool foundLevels = false;
    private void Update() {
        if(LevelStorage.JSONlevelSizes.Count != 0 && !foundLevels && gameObject.activeInHierarchy){
            int levelPanelCounter = 0;
            foreach (JSONLevelSize jSONLevelSize in LevelStorage.JSONlevelSizes)
            {
                Debug.Log(jSONLevelSize);
                GameObject LevelPanel = transform.GetChild(levelPanelCounter).gameObject;
                levelPanelCounter++;
                if(LevelPanel != null){
                    Debug.Log(LevelPanel);
                    LevelPanel.SetActive(true);
                    //LevelPanel.transform.Find("LevelName").GetComponent<TMP_Text>().text = jSONLevelSize.levelName;
                    LevelPanel.GetComponent<MenuInfo>().SetValue(jSONLevelSize.levelName, 0);
                }
            }
            foundLevels = true;
        }
    }
    
        
}
