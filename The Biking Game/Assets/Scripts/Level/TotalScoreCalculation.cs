using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TotalScoreCalculation : MonoBehaviour
{
    [SerializeField] [Range(1.00f, 1.20f)] private float _comboModifier;
    
    private List<BaseQuestion> _baseQuestions;
    [SerializeField] private float totalPossiblePoints = 0f;
    [SerializeField] private float[] rank;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetTotalScore(List<BlockInfo> blockInfos){
        foreach(BlockInfo blockInfo in blockInfos){
            BaseQuestion baseQuestion = blockInfo.tile.GetComponentInChildren<BaseQuestion>();
            if(baseQuestion != null){
                _baseQuestions.Add(baseQuestion);
            }
            
        }
        _baseQuestions.OrderByDescending(question => question.PointsReceived);
        int combo = 0;
        foreach(BaseQuestion question in _baseQuestions){
            totalPossiblePoints += question.PointsReceived * (Mathf.Pow(_comboModifier, combo));
            combo++;
        }
        return totalPossiblePoints;
    }
    public void GetFinalRank(float score, float totalPossiblePoints){
        float percentage = score/totalPossiblePoints*100;
        for(int i = 0; i < rank.Length; i++){
            if(i < rank.Length){
                if(percentage > 0 && percentage <= rank[i+1]){
                return;
            }
            if(i < rank.Length){
                if(percentage > rank[i] && percentage <= 100){
                    return;
                }
            }
                if(percentage > rank[i] && percentage < rank[i+1]){
                    return;
                }
            }
        }
    }
}
