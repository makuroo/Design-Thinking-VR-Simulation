using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    public float minDuration = 1f;
    public float maxDuration = 20f;


    public GameObject[] NPC;
    public GameObject[] goal;
    GameObject currInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRepeat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Spawn()
    {
        int temp = 0;
        //temp randomized for randomize sex
        temp = Random.Range(0, 2);

        currInstantiated = Instantiate(NPC[temp], this.transform.position, this.transform.rotation);
        NPCScript currNPCScript = currInstantiated.GetComponent<NPCScript>();

        //randomize destionation that players ahead to..
        if(currNPCScript)
        {
            currNPCScript.playerGoal = goal[Random.Range(0, goal.Length)].transform;
        }
        
    }

    IEnumerator SpawnRepeat()
    {
        yield return new WaitForSeconds(Random.Range(minDuration, maxDuration));
        Spawn();
        StartCoroutine(SpawnRepeat());
    }

}
