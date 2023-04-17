using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElement : MonoBehaviour
{
    [SerializeField] public List<GameObject> AbleToSpawn;
    [SerializeField] public GameObject Parent;
    [SerializeField] GameObject _spawn;
    [SerializeField] private string _gameObjectlocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn(){
        _spawn = Instantiate(AbleToSpawn[0], Parent.transform.Find(_gameObjectlocation));
        //_baseQuestion.name = jsonBlockInfo.baseQuestionName;
        _spawn.transform.parent = Parent.transform.Find(_gameObjectlocation).transform;
    }
    
}
