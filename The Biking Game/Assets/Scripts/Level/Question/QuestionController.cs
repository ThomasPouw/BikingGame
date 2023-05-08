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
    private Animator _gameScreenAnimator;
    private AudioSource _gameScreenSource;

    // Start is called before the first frame update
    void Start()
    {
        _allowedToVote = true;
    }

    void OnTriggerEnter(Collider other) {
        //_blockQuestion = other.gameObject.GetComponentInChildren<BaseQuestion>();
        if(_blockQuestion != null){
            _navMeshAgent.isStopped = true;
            SetQuestionUI();
        }
    }
    private void OnEnable() {
        Debug.Log(gameObject);
        if(GameObject.Find("BikeOperator") != null){
            _navMeshAgent = GameObject.Find("BikeOperator").GetComponent<NavMeshAgent>();
            _questionScreen = GameObject.Find("QuestionScreen");
            _answerHolderPanel = GameObject.Find("AnswerHolder");
            Transform question = _questionScreen.transform.Find("Question");
            if(question != null){
                _blockQuestion = question.GetComponentInChildren<BaseQuestion>();
                _questionText = question.GetComponent<TMP_Text>();
            }
            _pointComboUI = GameObject.Find("PointsSystem").GetComponent<PointComboUI>();
            _imageStorage = GameObject.Find("Storage").GetComponent<ImageStorage>();
            _navMeshAgent = GameObject.Find("BikeOperator").GetComponent<NavMeshAgent>();
            _gameScreenAnimator = _questionScreen.transform.parent.GetComponent<Animator>();
            _gameScreenSource = _questionScreen.transform.parent.GetComponent<AudioSource>();
        }
    }
    IEnumerator isCorrectAnswer(Entry answer){
        _allowedToVote = false;
        _gameScreenAnimator.SetTrigger("AnswerRise");
        yield return new WaitForSeconds(_gameScreenAnimator.GetCurrentAnimatorStateInfo(0).length);
        int index = -1;
        for (int i = 0; i < _blockQuestion.Answers.Length; i++)
        {
            if(_blockQuestion.Answers[i].IsCorrect){
                index = i;
            }
        }
        
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
            Debug.Log("Here!");
            Debug.Log(answer.OriginalLine);
            _pointComboUI.Combo +=1;
            _pointComboUI.Points +=_blockQuestion.PointsReceived;
        }
        else{
            _pointComboUI.Combo -= _blockQuestion.QuestionWrongPunishment;
            //reduce 1 from combo
        }
        yield return new WaitForSeconds(_gameScreenAnimator.GetCurrentAnimatorStateInfo(0).length);
        _gameScreenAnimator.SetTrigger("Vanish");
        _navMeshAgent.isStopped = false;
        _questionScreen.SetActive(false);
        _blockQuestion.QuestionVehicleMovement(StartEnd.End);
    }
    public void SetQuestionUI(){
        if(_blockQuestion != null){
            _questionScreen.SetActive(true);
            _blockQuestion.Question = new Translation().TranslateSentence(_blockQuestion.Question.OriginalLine, "Question");
            _questionText.text = _blockQuestion.Question.TranslatedLine;
            for (int i = 0; i < _blockQuestion.Answers.Length; i++)
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
        Debug.Log(_blockQuestion.Answers[i].OriginalLine);
        _blockQuestion.Answers[i].SetValue(answer.TranslatedLine, answer.HelperImages);
        Transform P = _answerHolderPanel.transform.GetChild(i);
        P.Find("ButtonToPress").GetChild(0).GetComponent<TMP_Text>().text = (i+1).ToString();
        P.Find("AnswerOption").GetComponent<TMP_Text>().text = _blockQuestion.Answers[i].TranslatedLine;
        Image[] _helperPictures = P.Find("ImageHolder").GetComponentsInChildren<Image>();
        if(_blockQuestion.Answers[i].HelperImages != null){
            
            for (int ii = 0; ii < _helperPictures.Length; ii++)
            {
                Debug.Log(_helperPictures[ii].gameObject);
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
            }
        yield return new WaitForSeconds(2f);
    }
    public void ControleSpeed(float Speed){
        _navMeshAgent.speed = Speed;
    }
}
