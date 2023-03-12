using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public class QuestionController : MonoBehaviour
{
    [SerializeField] private BaseQuestion _blockQuestion;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    public BaseQuestion BlockQuestion{set {_blockQuestion = value;}}
    [SerializeField] private GameObject _questionPanel;
    private GameObject _answerHolderPanel;
    private GameObject _answerPanel;
    private TMP_Text _questionText;


    // Start is called before the first frame update
    void Start()
    {
        _questionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetQuestionUI(){
        _questionPanel.SetActive(true);
        if(_blockQuestion != null){
            _questionText.text = _blockQuestion.Question.TranslatedLine;
            for (int i = 0; i < _blockQuestion.Answers.Length; i++)
            {
                Transform P = _answerHolderPanel.transform.GetChild(i);
                P.Find("ButtonToPress").GetChild(0).GetComponent<TMP_Text>().text = i.ToString();
                P.Find("AnswerOption").GetComponent<TMP_Text>().text = _blockQuestion.Answers[i].TranslatedLine;
                Image[] _helperImages = P.Find("ImageHolder").GetComponents<Image>();
                if(_blockQuestion.Answers[i].HelperImages != null){
                    for (int ii = 0; ii < _blockQuestion.Answers[i].HelperImages.Length; ii++)
                    {
                        //_helperImages[ii].defaultMaterial.mainTexture = _blockQuestion.Answers[i].HelperImages[ii]
                        //Need way to load images
                    }
                }
            }
        }
        else{
            Debug.LogError("_blockQuestion is not filled in. This Block does not have a question related to it.");
        }
        
    }
    public void ControleSpeed(float Speed){
        _navMeshAgent.speed = Speed;
    }
    void OnTriggerEnter(Collider other) {
        ControleSpeed(0f);
        SetQuestionUI();
    }
    private void OnEnable() {
        _navMeshAgent = GameObject.Find("BikeOperator").GetComponent<NavMeshAgent>();
        _questionPanel = GameObject.Find("QuestionScreen");
        _questionText = GameObject.Find("Question").GetComponent<TMP_Text>();
        _answerPanel = GameObject.Find("AnswerHolder");
    
    }
}
