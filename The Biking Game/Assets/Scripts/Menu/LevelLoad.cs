using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoad : MonoBehaviour
{
    [SerializeField] public bool foundLevels = false;
    [SerializeField] public bool forEditor = false;
    [SerializeField] public GameObject Panel;
    [SerializeField] public GameObject Canvas;
    private void Start() {
        Canvas= GameObject.Find("Canvas");
    }
    private void Update() {
        if(LevelStorage.JSONlevelSizes.Count != 0 && !foundLevels && LevelStorage.s_isConnected){
            foreach (JSONLevelSize jSONLevelSize in LevelStorage.JSONlevelSizes)
            {
                GameObject LevelPanel = Instantiate(Panel, transform.position, transform.rotation, transform);
                if(LevelPanel != null){
                    LevelPanel.SetActive(true);
                    //LevelPanel.transform.Find("LevelName").GetComponent<TMP_Text>().text = jSONLevelSize.levelName;
                    MenuInfo menuInfo = LevelPanel.GetComponent<MenuInfo>();
                    if(forEditor){
                        LevelPanel.transform.parent = GameObject.Find("Content").transform;
                        menuInfo.SetValue(jSONLevelSize.levelName);
                        LevelStorage levelStorage = GameObject.Find("Storage").GetComponent<LevelStorage>();
                        menuInfo.transform.Find("Delete").GetComponent<Button>().onClick.AddListener(() => levelStorage.DeleteLevel(jSONLevelSize.levelName));
                        menuInfo.transform.Find("Edit").GetComponent<Button>().onClick.AddListener(() => Canvas.GetComponent<SceneLoad>().LoadLevelEditor(menuInfo));
                        
                    }
                    else{
                        menuInfo.SetValue(jSONLevelSize.levelName, 0);
                        LevelPanel.GetComponent<Button>().onClick.AddListener(() => Canvas.GetComponent<SceneLoad>().LoadLevel(menuInfo));
                    }
                }
            }
            foundLevels = true;
        }
    }
    private void levelLoad(){

    }
}
