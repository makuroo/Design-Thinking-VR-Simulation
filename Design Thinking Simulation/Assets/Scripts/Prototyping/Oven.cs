using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public GameObject ovenDoor;
    public GameObject bowl;
    public GameObject recipePanel;
    public GameObject doneBakingUI;
    private Baking bowlScript;

    // Start is called before the first frame update
    void Start()
    {
        bowlScript = bowl.GetComponent<Baking>();
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bowl"))
        {
            Debug.Log("Bowl Detected");
            if (bowlScript.isBakeReady)
            {
                Debug.Log("Ready to bake");
                ovenDoor.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                bowlScript.InstantiateCake(bowlScript.selectedCake);
                recipePanel.SetActive(false);
                doneBakingUI.SetActive(true);
                //Add cake here
                GameManager.Instance.hasDonePrototyping = true;
                GameManager.Instance.isTesting = true;
                GameObject cake = Instantiate(GameManager.Instance.bakedCake.cakeAsset, bowlScript.initialBowlPosition, Quaternion.identity);
                cake.transform.localScale = new Vector3(0.085f, 0.085f, 0.085f);
                Destroy(bowl);
            }
        }
    }
}
