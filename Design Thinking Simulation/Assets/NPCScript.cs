using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AdvancedPeopleSystem;

public class NPCScript : MonoBehaviour
{
    public Transform playerGoal;

    NavMeshAgent agent;
    CharacterCustomization charCust;
    float distanceToGoal;

    float toleranceGoalAchieve = 1f;
    // Start is called before the first frame update

    private void Awake()
    {
        charCust = GetComponent<CharacterCustomization>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        charCust.Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToGoal = Vector3.Distance(this.gameObject.transform.position, playerGoal.position);
        if(distanceToGoal > toleranceGoalAchieve)
        {
            if(playerGoal)
            {
                agent.SetDestination(playerGoal.position);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
