using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    [SerializeField] JsonBlockInfo jsonBlockInfo;
    [SerializeField] LevelSize levelSize;
    [SerializeField] bool Activate = false;
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
            levelSize.MakeBlock(jsonBlockInfo, 1, 1);
            Activate = false;
        }
    }
}
