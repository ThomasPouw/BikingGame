using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasMenuAppear : MonoBehaviour
{
    [SerializeField] PopupStorage popupStorage;
    [SerializeField] private GameObject popUpPanel;


    private GameObject MenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        popupStorage = GameObject.Find("LevelEditor").GetComponent<PopupStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //if (!popupManager.GetPopupOpened())
        //{
            //popupManager.ChangeState(true);
            
            MenuPanel = Instantiate(popUpPanel);
            MenuPanel.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            //Testing.GetComponent<PanelControler>().setClickableEvent(updateClickable);

            MenuPanel.transform.position = Camera.main.WorldToScreenPoint(transform.Find("Menu").position);
            SpawnElement[] spawnElements = MenuPanel.GetComponentsInChildren<SpawnElement>();
            AllowedElements allowedElements = GetComponent<AllowedElements>();
            foreach (SpawnElement spawnElement in spawnElements)
            {
                Debug.Log(spawnElement.gameObject.tag);
                spawnElement.Parent = gameObject;
                switch (spawnElement.gameObject.tag)
                {
                    case("Road"):
                        spawnElement.AbleToSpawn = allowedElements.AllowedQuestions;
                    break;
                    case("Waypoint"):
                        spawnElement.AbleToSpawn = allowedElements.AllowedWayPoints;
                    break;
                    case("Question"):
                        spawnElement.AbleToSpawn = allowedElements.AllowedQuestions;
                    break;
                }
            }
            popupStorage.panel = MenuPanel;
            //MenuPanel.GetComponentInChildren<TMP_Text>().text = mainText;
            //MenuPanel.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = leftButton;
            //MenuPanel.transform.GetChild(2).GetComponentInChildren<TMP_Text>().text = rightButton;
        //}
        //gameObject.Destroy();
        
    }
    

}
