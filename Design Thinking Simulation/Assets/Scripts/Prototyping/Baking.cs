using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class Baking : MonoBehaviour
{
    public List<int> bowlContent = new();
    public List<int> bakeRecipe = new();

    private PourDetector graterScript;
    public bool isBakeReady = false;

    [Header("Oven")]
    public GameObject ovenDoor;

    [Header("UI Related")]
    public GameObject selectCake;
    public GameObject recipePanel;
    public GameObject ingredientsItem;
    public GameObject saltedCaramelBrowniesRecipe;
    public GameObject strawberryShortcakeRecipe;
    public GameObject bittersweetChocolateTartRecipe;
    public GameObject sourCherryAlmondCakeRecipe;
    public GameObject tamarindDarkChocolateBrowniesRecipe;
    public GameObject bitterAlmondTartRecipe;

    [Header("Cake Scriptable Object")]
    public CakeSO creamPastrySO;

    [Header("OnRelease Related")]
    public GameObject flour;
    public GameObject bakingPowder;
    public GameObject sugar;
    public GameObject seaSalt;
    public GameObject butter;
    public GameObject milk;
    public GameObject vanillaExtract;
    public GameObject lemonSqueezer;
    public GameObject darkChocolate;
    public GameObject grater;
    public GameObject lemon;
    private Vector3 initialFlourTransform;
    private Vector3 initialFlourRotation;
    private Vector3 initialBakingPowderTransform;
    private Vector3 initialBakingPowderRotation;
    private Vector3 initialSugarTransform;
    private Vector3 initialSugarRotation;
    private Vector3 initialSeaSaltTransform;
    private Vector3 initialSeaSaltRotation;
    private Vector3 initialButterTransform;
    private Vector3 initialButterRotation;
    private Vector3 initialMilkTransform;
    private Vector3 initialMilkRotation;
    private Vector3 initialVanillaExtractTransform;
    private Vector3 initialVanillaExtractRotation;
    private Vector3 initialLemonSqueezerTransform;
    private Vector3 initialLemonSqueezerRotation;
    private Vector3 initialDarkChocolateTransform;
    private Vector3 initialDarkChocolateRotation;
    private Vector3 initialGraterTransform;
    private Vector3 initialGraterRotation;
    private Vector3 initialLemonTransform;
    private Vector3 initialLemonRotation;

    private int sameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        bowlContent = new() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        graterScript = grater.GetComponent<PourDetector>();

        // Set initial position and rotation of ingredients object to reset on release
        initialFlourTransform = flour.gameObject.transform.localPosition;
        initialFlourRotation = flour.gameObject.transform.localRotation.eulerAngles;

        initialBakingPowderTransform = bakingPowder.gameObject.transform.localPosition;
        initialBakingPowderRotation = bakingPowder.gameObject.transform.localRotation.eulerAngles;

        initialSugarTransform = sugar.gameObject.transform.localPosition;
        initialSugarRotation = sugar.gameObject.transform.localRotation.eulerAngles;

        initialSeaSaltTransform = seaSalt.gameObject.transform.localPosition;
        initialSeaSaltRotation = seaSalt.gameObject.transform.localRotation.eulerAngles;

        initialDarkChocolateTransform = darkChocolate.gameObject.transform.localPosition;
        initialDarkChocolateRotation = darkChocolate.gameObject.transform.localRotation.eulerAngles;

        initialButterTransform = butter.gameObject.transform.localPosition;
        initialButterRotation = butter.gameObject.transform.localRotation.eulerAngles;

        initialMilkTransform = milk.gameObject.transform.localPosition;
        initialMilkRotation = milk.gameObject.transform.localRotation.eulerAngles;

        initialVanillaExtractTransform = vanillaExtract.gameObject.transform.localPosition;
        initialVanillaExtractRotation = vanillaExtract.gameObject.transform.localRotation.eulerAngles;

        initialLemonTransform = lemon.gameObject.transform.localPosition;
        initialLemonRotation = lemon.gameObject.transform.localRotation.eulerAngles;

        initialLemonSqueezerTransform = lemonSqueezer.gameObject.transform.localPosition;
        initialLemonSqueezerRotation = lemonSqueezer.gameObject.transform.localRotation.eulerAngles;

        initialGraterTransform = grater.gameObject.transform.localPosition;
        initialGraterRotation = grater.gameObject.transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (bakeRecipe != null)
        {
            if (!isBakeReady)
            {
                sameCount = 0;
                for (int i = 0; i < bakeRecipe.Count; i++)
                {
                    if (bowlContent[i] == bakeRecipe[i]) {
                        sameCount++;
                        if (sameCount == bakeRecipe.Count)
                        {
                            isBakeReady = true;
                            break;
                        }
                    }
                }
            }
        }
        if (isBakeReady)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<MeshCollider>().convex = true;
            gameObject.GetComponent<Grabbable>().enabled = true;
            ingredientsItem.SetActive(false);
            OpenOvenDoor();
        }
    }

    // Adding ingredients into the bowl
    public void TepungTeriguAdded()
    {
        bowlContent.RemoveAt(0);
        bowlContent.Insert(0, 1);
    }

    public void BakingPowderAdded()
    {
        bowlContent.RemoveAt(1);
        bowlContent.Insert(1, 1);
    }

    public void GulaPasirAdded()
    {
        bowlContent.RemoveAt(2);
        bowlContent.Insert(2, 1);
    }

    public void SeaSaltAdded()
    {
        bowlContent.RemoveAt(3);
        bowlContent.Insert(3, 1);
    }

    public void MentegaAdded()
    {
        bowlContent.RemoveAt(4);
        bowlContent.Insert(4, 1);
    }

    public void SusuAdded()
    {
        bowlContent.RemoveAt(5);
        bowlContent.Insert(5, 1);
    }

    public void EkstrakVanillaAdded()
    {
        bowlContent.RemoveAt(6);
        bowlContent.Insert(6, 1);
    }

    public void AirPerasanLemonAdded()
    {
        bowlContent.RemoveAt(7);
        bowlContent.Insert(7, 1);
    }

    public void DarkChocolateAdded()
    {
        bowlContent.RemoveAt(8);
        bowlContent.Insert(8, 1);
    }

    public void LemonZestAdded()
    {
        bowlContent.RemoveAt(9);
        bowlContent.Insert(9, 1);
    }

    public void CakeButtonClicked()
    {
        selectCake.SetActive(false);
        recipePanel.SetActive(true);
        ingredientsItem.SetActive(true);
    }

    public void SaltedCaramelBrowniesSelect()
    {
        bakeRecipe = creamPastrySO.recipe;
        saltedCaramelBrowniesRecipe.SetActive(true);
    }

    public void StrawberryShortcakeSelect()
    {
        strawberryShortcakeRecipe.SetActive(true);
    }

    public void BittersweetChocolateTartSelect()
    {
        bittersweetChocolateTartRecipe.SetActive(true);
    }

    public void SourCherryAlmondCakeSelect()
    {
        sourCherryAlmondCakeRecipe.SetActive(true);
    }

    public void TamarindDarkChocolateBrowniesSelect()
    {
        tamarindDarkChocolateBrowniesRecipe.SetActive(true);
    }

    public void BitterAlmondTartSelect()
    {
        bitterAlmondTartRecipe.SetActive(true);
    }

    public void OpenOvenDoor()
    {
        ovenDoor.transform.rotation = Quaternion.Euler(-140f, 0f, 0f);
    }

    // On Releasing Grab on Ingredients to reset position
    public void OnReleaseFlour()
    {
        flour.transform.localPosition = initialFlourTransform;
        flour.transform.localRotation = Quaternion.Euler(initialFlourRotation);
    }

    public void OnReleaseBakingPowder()
    {
        bakingPowder.transform.localPosition = initialBakingPowderTransform;
        bakingPowder.transform.localRotation = Quaternion.Euler(initialBakingPowderRotation);
    }

    public void OnReleaseSugar()
    {
        sugar.transform.localPosition = initialSugarTransform;
        sugar.transform.localRotation = Quaternion.Euler(initialSugarRotation);
    }

    public void OnReleaseSeaSalt()
    {
        seaSalt.transform.localPosition = initialSeaSaltTransform;
        seaSalt.transform.localRotation = Quaternion.Euler(initialSeaSaltRotation);
    }

    public void OnReleaseButter()
    {
        butter.transform.localPosition = initialButterTransform;
        butter.transform.localRotation = Quaternion.Euler(initialButterRotation);
    }

    public void OnReleaseMilk()
    {
        milk.transform.localPosition = initialMilkTransform;
        milk.transform.localRotation = Quaternion.Euler(initialMilkRotation);
    }

    public void OnReleaseDarkChocolate()
    {
        //Resets dark chocolate position and rotation to initial position and rotation
        darkChocolate.transform.localPosition = initialDarkChocolateTransform;
        darkChocolate.transform.localRotation = Quaternion.Euler(initialDarkChocolateRotation);
    }

    public void OnReleaseGrater() 
    {
        grater.transform.localPosition = initialGraterTransform;
        grater.transform.localRotation = Quaternion.Euler(initialGraterRotation);
    }

    public void OnReleaseLemon()
    {
        lemon.transform.localPosition = initialLemonTransform;
        lemon.transform.localRotation = Quaternion.Euler(initialLemonRotation);
    }

    public void OnReleaseLemonSqueezer()
    {
        lemonSqueezer.transform.localPosition = initialLemonSqueezerTransform;
        lemonSqueezer.transform.localRotation = Quaternion.Euler(initialLemonSqueezerRotation);
    }

    public void OnReleaseVanillaExtract()
    {
        vanillaExtract.transform.localPosition = initialVanillaExtractTransform;
        vanillaExtract.transform.localRotation = Quaternion.Euler(initialVanillaExtractRotation);
    }

    // Checking if ingredients object trigger bowl collider
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Flour"){
            TepungTeriguAdded();
        }
        else if (other.gameObject.name == "Baking Powder"){
            BakingPowderAdded();
        }
        else if (other.gameObject.name == "Sugar"){
            GulaPasirAdded();
        }
        else if (other.gameObject.name == "Salt"){
            SeaSaltAdded();
        }
        else if(other.gameObject.name == "Dark Chocolate"){
            DarkChocolateAdded();
        }
        else if(other.gameObject.name == "Butter"){
            MentegaAdded();
        }
        else if(other.gameObject.name == "Milk"){
            SusuAdded();
        }
        else if(other.gameObject.name == "Vanilla Extract")
        {
            EkstrakVanillaAdded();
        }
        else if(other.gameObject.name == "Lemon Squeezer")
        {
            AirPerasanLemonAdded();
        }
        else if(other.gameObject.name == "Grater")
        {
            if (graterScript.isAddLemonZest)
            {
                LemonZestAdded();
            }
        }
        else if(other.gameObject.name == "Lemon Half")
        {
            if (graterScript.isAddLemonZest)
            {
                LemonZestAdded();
            }
        }
    }
}