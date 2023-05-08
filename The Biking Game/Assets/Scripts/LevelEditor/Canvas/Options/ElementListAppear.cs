using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementListAppear : MonoBehaviour
{
    [SerializeField] public List<GameObject> SpawnList;
    [SerializeField] public GameObject ElementList;
    [SerializeField] public GameObject Panel;
    [SerializeField] public GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ViewList(){
        ElementList.SetActive(true);
        Transform[] ElementListChildren = ElementList.GetComponentsInChildren<Transform>();
        foreach (Transform child in ElementListChildren)
        {
            if(child != null && child.gameObject.name != "OptionList")
            Destroy(child.gameObject);
            //Destroy(child);
        }
        foreach (var item in SpawnList)
        {
            GameObject _spawn = Instantiate(Panel);
            //_baseQuestion.name = jsonBlockInfo.baseQuestionName;
            _spawn.transform.parent = ElementList.transform;
            _spawn.GetComponentInChildren<TMP_Text>().text = item.name.Replace("_", " ");
            SpawnElement spawnElement = _spawn.GetComponent<SpawnElement>();
            spawnElement.SpawnPart = item;
            spawnElement.GameObjectlocation = gameObject.name;
            spawnElement.Parent = Parent;
        }
    }
}
