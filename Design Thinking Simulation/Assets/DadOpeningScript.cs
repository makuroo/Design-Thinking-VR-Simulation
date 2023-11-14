using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DadOpeningScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform playerGoal;
    Animator anim;
    NavMeshAgent agent;
    float distanceToGoal;

    float toleranceGoalAchieve = 1f;


    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ChangeWeightAnimationHand(1f);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void ChangeWeightAnimationHand(float value)
    {
        anim.SetLayerWeight(1, value);
    }

    public void ChangeWeightAnimationDisappointed(float value)
    {
        anim.SetLayerWeight(2, value);
    }

    public void MoveTo(Vector3 position)
    {
        distanceToGoal = Vector3.Distance(this.gameObject.transform.position, playerGoal.position);
        if (distanceToGoal > toleranceGoalAchieve)
        {
            if (playerGoal)
            {
                agent.SetDestination(position);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
