using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuInfo: MonoBehaviour
{
    public string levelName;
    public float score;
    public void SetValue(MenuInfo menuInfo){
        Debug.Log("HERE!");
        levelName = menuInfo.levelName;
        score = menuInfo.score;
        StartCoroutine(LoadLevel());
    }
    public void SetValue(string levelName, float score){
        transform.Find("LevelName").GetComponent<TMP_Text>().text = levelName;
        this.levelName = levelName;
        //transform.Find("Score").GetComponent<TMP_Text>().text = score.ToString();
        this.score = score;
    }
    public void SetValue(string levelName){
        transform.Find("LevelName").GetComponent<TMP_Text>().text = levelName;
        this.levelName = levelName;
        
        //transform.Find("Score").GetComponent<TMP_Text>().text = score.ToString();
    }
    IEnumerator LoadLevel(){
        yield return new WaitForSeconds(0.5f);
        if(SceneManager.GetActiveScene().name == "Level"){
            JSONLevelSize jSONLevelSize = GetComponent<LevelStorage>().ReadLevel(levelName);
            GameObject.Find("LevelMaker").GetComponent<LevelSize>().SetValue(jSONLevelSize);
        }
        if(SceneManager.GetActiveScene().name == "LevelEditor"){
            JSONLevelSize jSONLevelSize = GetComponent<LevelStorage>().ReadLevel(levelName);
            GameObject.Find("LevelEditor").GetComponent<LevelSize>().SetValue(jSONLevelSize);
            GameObject.Find("LevelEditor").GetComponent<LevelSize>().skipMakeLevel = true;
            GameObject.Find("XField").GetComponent<TMP_InputField>().text = jSONLevelSize.xMax.ToString();
            GameObject.Find("ZField").GetComponent<TMP_InputField>().text = jSONLevelSize.zMax.ToString();
            GameObject.Find("NameInput").GetComponent<TMP_InputField>().text = jSONLevelSize.levelName;
            GameObject.Find("LevelEditor").GetComponent<LevelSize>().skipMakeLevel = false;
        }
    }
   
}

