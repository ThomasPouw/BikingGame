using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticMachine : MonoBehaviour
{
    [SerializeField]public static MenuInfo menuInfo;
    //[SerializeField] public MenuInfo m;
    private void Update() {
        //m = menuInfo;
        //Debug.Log(SceneManager.GetActiveScene().name+ " "+ menuInfo);
    }
    private void Awake() {
        DontDestroyOnLoad(gameObject);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Start()
    {
    menuInfo = gameObject.AddComponent<MenuInfo>();
    } 
}
