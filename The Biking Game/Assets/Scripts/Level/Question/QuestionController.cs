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
    public BaseQuestion BlockQuestion{set {_blockQuestion = value;} get{return _blockQuestion;}}
    [SerializeField] private GameObject _questionPanel;
    [SerializeField] private GameObject _answerHolderPanel;
    [SerializeField] private ImageStorage _imageStorage;
    private GameObject _answerPanel;
    [SerializeField]private TMP_Text _questionText;
    private PointComboUI _pointComboUI;
    [SerializeField]private bool _allowedToVote;

    // Start is called before the first frame update
    void Start()
    {
        
        _questionPanel.SetActive(false);
        _navMeshAgent.isStopped = false;
        _allowedToVote = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void isCorrectAnswer(Entry answer){
        _allowedToVote = false;
        if(answer.IsCorrect){
            _pointComboUI.Combo +=1;
            _pointComboUI.Points +=_blockQuestion.PointsReceived;
        }
        else{
            _pointComboUI.Combo -= _blockQuestion.QuestionWrongPunishment;
            //reduce 1 from combo
        }
        _navMeshAgent.isStopped = false;
        _questionPanel.SetActive(false);
        _blockQuestion.QuestionVehicleMovement(StartEnd.End);
    }
    public void SetQuestionUI(){
        if(_blockQuestion != null){
            _questionPanel.SetActive(true);
            _blockQuestion.Question = new Translation().TranslateSentence(_blockQuestion.Question.OriginalLine, "Question");
            Debug.Log(_questionText.text);
            _questionText.text = _blockQuestion.Question.TranslatedLine;
            for (int i = 0; i < _blockQuestion.Answers.Length; i++)
            {
                Entry answer = new Translation().TranslateSentence(_blockQuestion.Answers[i].OriginalLine, "Answer");
                _blockQuestion.Answers[i].SetValue(answer.TranslatedLine, answer.HelperImages);
                Transform P = _answerHolderPanel.transform.GetChild(i);
                P.Find("ButtonToPress").GetChild(0).GetComponent<TMP_Text>().text = (i+1).ToString();
                P.Find("AnswerOption").GetComponent<TMP_Text>().text = _blockQuestion.Answers[i].TranslatedLine;
                Image[] _helperImages = P.Find("ImageHolder").GetComponentsInChildren<Image>();
                if(_blockQuestion.Answers[i].HelperImages != null){
                    for (int ii = 0; ii < _blockQuestion.Answers[i].HelperImages.Length; ii++)
                    {
                        Debug.Log(_helperImages.Length);
                        _imageStorage.DownloadPicture(_blockQuestion.Answers[i].HelperImages[ii], _helperImages[ii]);
                        //texture.LoadRawTextureData(ImageStorage.Images[_blockQuestion.Answers[i].HelperImages[ii]]);
                        //_helperImages[ii].defaultMaterial.mainTexture = texture;
                        //Need way to load images
                    }
                }
            }
        }
        else{
            Debug.LogError("_blockQuestion is not filled in. This Block does not have a question related to it.");
        }
        
    }
    private void OnTriggerStay(Collider other) {
        if(_blockQuestion != null && _allowedToVote){
            if(Input.GetKey(KeyCode.Alpha1) && _blockQuestion.Answers[0] != null){
                Debug.Log("1");
            isCorrectAnswer(_blockQuestion.Answers[0]);
            }
            else if(Input.GetKey(KeyCode.Alpha2) && _blockQuestion.Answers[1] != null){
                Debug.Log("2");
            isCorrectAnswer(_blockQuestion.Answers[1]);
            }
            else if(Input.GetKey(KeyCode.Alpha3) && _blockQuestion.Answers[2] != null){
                Debug.Log("3");
            isCorrectAnswer(_blockQuestion.Answers[2]);
            }
        }
    }
    public void ControleSpeed(float Speed){
        _navMeshAgent.speed = Speed;
    }
    void OnTriggerEnter(Collider other) {
        if(_blockQuestion != null){
            _navMeshAgent.isStopped = true;
            SetQuestionUI();
        }
    }
    private void OnEnable() {
        _navMeshAgent = GameObject.Find("BikeOperator").GetComponent<NavMeshAgent>();
        _questionPanel = GameObject.Find("QuestionScreen");
        _answerHolderPanel = GameObject.Find("AnswerHolder");
        Transform question = transform.parent.parent.parent.Find("Question");
        if(question != null)
        _blockQuestion = question.GetComponentInChildren<BaseQuestion>();
        _questionText = _questionPanel.transform.Find("Question").GetComponent<TMP_Text>();
        _pointComboUI = GameObject.Find("PointsSystem").GetComponent<PointComboUI>();
        _imageStorage = GameObject.Find("Storage").GetComponent<ImageStorage>();
        
    }
}
