using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] public LevelSize LevelSize;
    // Start is called before the first frame update
    void Start()
    {
        JSONLevelSize jSONLevelSize = new JSONLevelSize(LevelSize);
        Debug.Log(JsonUtility.ToJson(jSONLevelSize));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
