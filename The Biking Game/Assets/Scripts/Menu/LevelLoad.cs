using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoad : MonoBehaviour
{
    [SerializeField] public int foundLevels = 0;
    [SerializeField] public bool forEditor = false;
    [SerializeField] public GameObject Panel;
    [SerializeField] public GameObject Canvas;
    private void Start() {
        Canvas= GameObject.Find("Canvas");
    }
    private void Update() {
        if(LevelStorage.JSONlevelSizes.Count != foundLevels && LevelStorage.s_isConnected){
            foundLevels = 0;
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
                        menuInfo.transform.Find("Delete").GetComponent<Button>().onClick.AddListener(() => deleteLevel(levelStorage, jSONLevelSize.levelName, menuInfo.gameObject));
                        menuInfo.transform.Find("Edit").GetComponent<Button>().onClick.AddListener(() => Canvas.GetComponent<SceneLoad>().LoadLevelEditor(menuInfo));
                        menuInfo.name = jSONLevelSize.levelName;
                    }
                    else{
                        menuInfo.SetValue(jSONLevelSize.levelName, 0);
                        LevelPanel.GetComponent<Button>().onClick.AddListener(() => Canvas.GetComponent<SceneLoad>().LoadLevel(menuInfo));
                    }
                }
                foundLevels +=1;
            }
            
        }
    }
    public void deleteLevel(LevelStorage levelStorage, string levelName, GameObject menuInfo){
        levelStorage.DeleteLevel(levelName);
        foundLevels -= 1;
        Destroy(menuInfo);
    }
}
