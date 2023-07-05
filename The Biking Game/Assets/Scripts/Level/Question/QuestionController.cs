using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEditor;
using TMPro;


public class QuestionController : MonoBehaviour
{
    [SerializeField] private BaseQuestion _blockQuestion;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    public BaseQuestion BlockQuestion{set {Debug.Log(this+ " "+value);
        _blockQuestion = value;} get{return _blockQuestion;}}
    [SerializeField] private GameObject _questionScreen;
    [SerializeField] private GameObject _answerHolderPanel;
    [SerializeField] private ImageStorage _imageStorage;
    private GameObject _answerPanel;
    [SerializeField]private TMP_Text _questionText;
    private PointComboUI _pointComboUI;
    [SerializeField]private bool _allowedToVote;

    //Animation and Sound
    [SerializeField]private Animator _gameScreenAnimator;
    [SerializeField]private BikeAudio _bikeAudio;
    [SerializeField]private QuestionAudio _questionAudio;

    // Start is called before the first frame update
    void Start()
    {
        _allowedToVote = true;
    }

    void OnTriggerEnter(Collider other) {
        //_blockQuestion = other.gameObject.GetComponentInChildren<BaseQuestion>();
        if(_blockQuestion != null){
            _navMeshAgent.isStopped = true;
            _bikeAudio.changeVolume(0);
            ChangeOutline(true);
            SetQuestionUI();
        }
    }
    private void OnEnable() {
        if(GameObject.Find("BikeOperator") != null){
            _questionScreen = GameObject.Find("QuestionScreen");
            _answerHolderPanel = GameObject.Find("AnswerHolder");
            Transform question = _questionScreen.transform.GetChild(0).Find("Question");
            if(question != null){
                _blockQuestion = question.GetComponentInChildren<BaseQuestion>();
                _questionText = question.GetComponent<TMP_Text>();
            }
            _pointComboUI = GameObject.Find("PointSystem").GetComponent<PointComboUI>();
            _imageStorage = GameObject.Find("Storage").GetComponent<ImageStorage>();
            _navMeshAgent = GameObject.Find("BikeOperator").GetComponent<NavMeshAgent>();
            _gameScreenAnimator = _questionScreen.transform.parent.GetComponent<Animator>();
            _questionAudio = _questionScreen.transform.parent.GetComponent<QuestionAudio>();
            _bikeAudio = GameObject.Find("BikeOperator").transform.GetChild(0).GetComponent<BikeAudio>();
        }
    }
    IEnumerator isCorrectAnswer(Entry answer){
        _allowedToVote = false;
        ChangeOutline(false);
        _gameScreenAnimator.SetTrigger("AnswerRise");
        yield return new WaitForSeconds(_gameScreenAnimator.GetCurrentAnimatorStateInfo(0).length);
        int index = _blockQuestion.Answers.FindIndex(x => x.IsCorrect == true);
        if(index == 0){
            _gameScreenAnimator.SetTrigger("LeftCorrect");
        }
        else if(index == 1){
            _gameScreenAnimator.SetTrigger("CenterCorrect");
        }
        else{
            _gameScreenAnimator.SetTrigger("RightCorrect");
        }
        if(answer.IsCorrect){
            _pointComboUI.Combo +=1;
            _pointComboUI.Points +=_blockQuestion.PointsReceived;
            _questionAudio.QuestionAnswerAudio(true);
        }
        else{
            _pointComboUI.Combo -= _blockQuestion.QuestionWrongPunishment;
            _questionAudio.QuestionAnswerAudio(false);
            //reduce 1 from combo
        }
        yield return new WaitForSeconds(_gameScreenAnimator.GetCurrentAnimatorStateInfo(0).length-0.5f);
        _blockQuestion.QuestionVehicleMovement(StartEnd.End);
        yield return new WaitForSeconds(1.5f);
        _gameScreenAnimator.SetTrigger("Vanish");
        _navMeshAgent.isStopped = false;
        _bikeAudio.changeVolume(1);
        _questionScreen.SetActive(false);
    }
    public void SetQuestionUI(){
        if(_blockQuestion != null){
            _questionScreen.SetActive(true);
            StartCoroutine(LoadQuestion());
            for (int i = 0; i < 3; i++)
            {
                StartCoroutine(LoadAnswer(i));
            }
        }
        else{
            Debug.LogError("_blockQuestion is not filled in. This Block does not have a question related to it.");
        }
        
    }
    private void OnTriggerStay(Collider other) {
        if(_blockQuestion != null && _allowedToVote){
            if(Input.GetKey(KeyCode.Alpha1) && _blockQuestion.Answers[0] != null){
                StartCoroutine(isCorrectAnswer(_blockQuestion.Answers[0]));
            }
            else if(Input.GetKey(KeyCode.Alpha2) && _blockQuestion.Answers[1] != null){
                StartCoroutine(isCorrectAnswer(_blockQuestion.Answers[1]));
            }
            else if(Input.GetKey(KeyCode.Alpha3) && _blockQuestion.Answers[2] != null){

                StartCoroutine(isCorrectAnswer(_blockQuestion.Answers[2]));
            }
        }
    }
    IEnumerator LoadAnswer(int i){
        Entry answer = new Translation().TranslateSentence(_blockQuestion.Answers[i].OriginalLine, "Answer");
        _blockQuestion.Answers[i].SetValue(answer.TranslatedLine, answer.HelperImages);
        Transform P = _answerHolderPanel.transform.GetChild(i);
        P.Find("ButtonToPress").GetChild(0).GetComponent<TMP_Text>().text = (i+1).ToString();
        P.Find("AnswerOption").GetComponent<TMP_Text>().text = _blockQuestion.Answers[i].TranslatedLine;
        Image[] _helperPictures = P.Find("ImageHolder").GetComponentsInChildren<Image>(true);
        for (int ii = 0; ii < 4; ii++)
            {
                if(_blockQuestion.Answers[i].HelperImages.Length > ii){
                    _helperPictures[ii].gameObject.SetActive(true);
                    _imageStorage.DownloadPicture(_blockQuestion.Answers[i].HelperImages[ii], _helperPictures[ii]);
                }
                else{
                    _helperPictures[ii].gameObject.SetActive(false);
                }
                
                //texture.LoadRawTextureData(ImageStorage.Images[_blockQuestion.Answers[i].HelperImages[ii]]);
                //_helperImages[ii].defaultMaterial.mainTexture = texture;
            //Need way to load images
        }
        yield return new WaitForSeconds(2f);
    }
    IEnumerator LoadQuestion(){
            
            _blockQuestion.Question = new Translation().TranslateSentence(_blockQuestion.Question.OriginalLine, "Question");
            _questionText.text = _blockQuestion.Question.TranslatedLine;
            Transform _imageHolder = _questionScreen.transform.Find("QuestionHolder").Find("ImageHolder");
            for (int i = 0; i < _imageHolder.childCount; i++)
            {
                _imageHolder.GetChild(i).gameObject.SetActive(true);
                Image _helperPicture = _imageHolder.GetChild(i).GetComponent<Image>();
                if(_blockQuestion.Question.HelperImages.Length > i && _blockQuestion.Question.HelperImages.Length != 0){
                    _helperPicture.gameObject.SetActive(true);
                    _imageStorage.DownloadPicture(_blockQuestion.Question.HelperImages[i], _helperPicture);
                }
                else{
                    _imageHolder.GetChild(i).gameObject.SetActive(false);
                }
            }
            yield return null;
            
    }
    public void ControleSpeed(float Speed){
        _navMeshAgent.speed = Speed;
    }
    public void ChangeOutline(bool Active)
    {
        Outline[] outlines = _blockQuestion.gameObject.GetComponentsInChildren<Outline>();
        foreach (Outline outline in outlines)
        {
            outline.enabled = Active;
        }
    }
}
