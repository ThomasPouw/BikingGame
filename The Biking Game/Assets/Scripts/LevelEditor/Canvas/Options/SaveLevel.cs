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
        if(levelName.text != ""){
            levelSize.levelName = levelName.text;
            gameObject.GetComponentInChildren<TMP_Text>().text = new Translation().TranslateSentence("Saved", "Menu").TranslatedLine;
            _LevelStorage.SaveLevel(new JSONLevelSize(levelSize));
            _LevelStorage.ReadLevels();
        }
    }
    private void OnEnable() {
        _LevelStorage = GameObject.Find("Storage").GetComponent<LevelStorage>();
    }

}
