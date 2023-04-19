using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupStorage : MonoBehaviour
{
    public GameObject panel {set {if(_Panel != null)Destroy(_Panel);_Panel = value;} get{return _Panel;}}
    [SerializeField] private GameObject _Panel;
}
