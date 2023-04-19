using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    
    public void LoadLevel(MenuInfo menuInfo){
        Debug.Log(menuInfo);
        StaticMachine.menuInfo.SetValue(menuInfo);
        SceneManager.LoadScene("Level");
    }
    public void LoadLevelEditor(){
        SceneManager.LoadScene("LevelEditor");
    }
    
}
