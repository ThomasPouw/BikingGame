using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuInfo: MonoBehaviour
{
    public string levelName;
    public float score;
    public void SetValue(MenuInfo menuInfo){
        levelName = menuInfo.levelName;
        score = menuInfo.score;
    }
    public void SetValue(string levelName, float score){
        transform.Find("LevelName").GetComponent<TMP_Text>().text = levelName;
        this.levelName = levelName;
        transform.Find("Score").GetComponent<TMP_Text>().text = score.ToString();
        this.score = score;
    }
}

