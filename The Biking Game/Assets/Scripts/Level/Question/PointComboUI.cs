using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointComboUI : MonoBehaviour
{
    [SerializeField]public ComboText[] ComboTexts;
    public float Points {
        get{return _points;} 
        set {
            _points += (value * Mathf.Pow(_comboModifier, _combo));
            pointTextWriter();
            }
        }
    public int Combo {
        get{return _combo;} 
        set{ 
            if(value < 0)
                _combo = 0;
            else
            _combo = value;
            _comboText.enabled = true;
            comboTextWriter();
        }
        }
    [SerializeField] [Range(1.00f, 1.20f)] private float _comboModifier;
    [SerializeField] private float _points;
    [SerializeField] private int _combo;
    private TMP_Text _pointText;
    private TMP_Text _comboText;
    private Translation _tranlation;
    // Start is called before the first frame update
    void Start()
    {
        _pointText = transform.GetChild(0).Find("PointCounter").GetComponent<TMP_Text>();
        _comboText = transform.GetChild(0).Find("ComboCounter").GetComponent<TMP_Text>();
        _comboText.enabled = false;
        _tranlation = new Translation();
        pointTextWriter();
        comboTextWriter();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void comboTextWriter(){
        for (int i = ComboTexts.Length - 1; i >= 0 ; i--)
        {
            if(ComboTexts[i].MinimumCombo <= _combo){
                _comboText.text = _tranlation.TranslateSentence(ComboTexts[i].OriginalComboText, "LevelUI").TranslatedLine+"<br>" + _combo.ToString()+ "x " + _tranlation.TranslateSentence("Combo", "LevelUI").TranslatedLine;
                break;
            }
            else{
                
            }
        } 
    }
    private void pointTextWriter(){
        _pointText.text = _points.ToString();
    }
    private void OnEnable() {
    _tranlation = new Translation();
}
}
[System.Serializable]
public class ComboText{
    public string OriginalComboText;
    public int MinimumCombo;
}

