using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveLevel : MonoBehaviour
{

    [SerializeField] LevelStorage _LevelStorage;
    [SerializeField] LevelSize levelSize;
    [SerializeField] TMP_InputField levelName;
    public void saveLevel(){
        levelSize.levelName = levelName.text;
        _LevelStorage.SaveLevel(new JSONLevelSize(levelSize));
    }
    private void OnEnable() {
        _LevelStorage = GameObject.Find("Storage").GetComponent<LevelStorage>();
    }

}
