using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entry : MonoBehaviour
{
    [SerializeField] public string OriginalLine;
    [SerializeField] public string TranslatedLine;
    [SerializeField] public string[] HelperImages;
}
