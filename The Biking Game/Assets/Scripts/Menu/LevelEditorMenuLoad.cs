using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorMenuLoad : MonoBehaviour
{
    
    [SerializeField] public bool foundLevels = false;
    [SerializeField] public GameObject Panel;
    [SerializeField] public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelStorage.JSONlevelSizes.Count != 0 && !foundLevels){
            foreach (JSONLevelSize jSONLevelSize in LevelStorage.JSONlevelSizes)
            {
                GameObject LevelPanel = Instantiate(Panel, transform.position, transform.rotation, transform);
                if(LevelPanel != null){
                    LevelPanel.SetActive(true);
                    //LevelPanel.transform.Find("LevelName").GetComponent<TMP_Text>().text = jSONLevelSize.levelName;
                    MenuInfo menuInfo = LevelPanel.GetComponent<MenuInfo>();
                    menuInfo.SetValue(jSONLevelSize.levelName, 0);
                    LevelPanel.GetComponent<Button>().onClick.AddListener(() => Canvas.GetComponent<SceneLoad>().LoadLevel(menuInfo));
                }
            }
            foundLevels = true;
        }
    }
}
