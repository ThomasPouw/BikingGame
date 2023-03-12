using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuestion : MonoBehaviour
{
    [SerializeField] public Sentence Question;
    [SerializeField] public Answer[] Answers;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1) && Answers[0] != null){
            isCorrectAnswer(Answers[0]);
        }
        else if(Input.GetKey(KeyCode.Alpha2) && Answers[1] != null){
            isCorrectAnswer(Answers[1]);
        }
        else if(Input.GetKey(KeyCode.Alpha3) && Answers[2] != null){
            isCorrectAnswer(Answers[2]);
        }
    }
    private void isCorrectAnswer(Answer answer){
        if(answer.IsCorrect){
            //Get Points
            //Get Combo
        }
        else{
            //reduce 1 from combo
        }
    }
}
