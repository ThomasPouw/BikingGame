using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSwitch : MonoBehaviour
{
    [SerializeField] public GameObject FuturePanel;
    [SerializeField] public GameObject CurrentPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchPanel(){
        FuturePanel.SetActive(true);
        CurrentPanel.SetActive(false);
    }
    [System.Serializable]
    public class MenuEntry{
        public TMP_Text TextLocation;
        public Entry entry;
    }
}
