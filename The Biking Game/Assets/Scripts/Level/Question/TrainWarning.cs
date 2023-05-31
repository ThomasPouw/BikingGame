using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainWarning : BaseQuestion
{
    [SerializeField] private Animator TrainBarriers;
    public override void QuestionVehicleMovement(StartEnd startEnd)
    {
        Debug.Log("Override!");
        if(startEnd == StartEnd.Start){
            TrainBarriers.SetTrigger("Barriers");
            TrainBarriers.SetBool("Lights", true);
        }
        else{
            TrainBarriers.SetBool("Barriers", false);
            TrainBarriers.SetBool("Lights", false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
