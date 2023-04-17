using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] public LevelSize LevelSize;
    [SerializeField] public bool Activate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos() {
        if(Activate){
            JSONLevelSize jSONLevelSize = new JSONLevelSize(LevelSize);
            Debug.Log(JsonUtility.ToJson(jSONLevelSize));
            Activate = false;
        }
    }
}
