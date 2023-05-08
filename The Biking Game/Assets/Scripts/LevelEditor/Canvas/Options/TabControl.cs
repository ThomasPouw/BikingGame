using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabControl : MonoBehaviour
{
    [SerializeField] GameObject ObjectPanel;
    [SerializeField] GameObject ObjectMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetActiveTab(string TabName){
        GameObject[] baseBlocks = Resources.LoadAll<GameObject>("Prefab/"+TabName);
        foreach (GameObject baseBlock in baseBlocks)
        {
            GameObject panel = Instantiate(ObjectPanel);
            SpawnElement SE = panel.GetComponent<SpawnElement>();
            if(SE != null){
                SE.SpawnPart = baseBlock;
                GameObject G = Instantiate(baseBlock);
                G.transform.SetParent(panel.transform, false);
                G.layer = 5;
            }
            panel.transform.SetParent(ObjectMenu.transform, false);

        }
    }
    
}
