using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public bool isWorldSpawner;
    public bool isCafeSpawner;
    public bool isRestaurantSpawner;
    public bool isCanteenSpawner;
    public GameObject prefabCustomer;
    public List<GameObject> instantiatedCustomer = new List<GameObject>();
    List<People> peopleScript = new List<People>();
    People Temp;
    SpawnerRef spawnerRefScript;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        spawnerRefScript = GameObject.Find("SpawnRef").GetComponent<SpawnerRef>();
        //if (isWorldSpawner)
        //{
        //    for (int i = 0; i < GameManager.Instance.WorldCustomerCount; i++)
        //    {
        //        GameObject instantiatedPrefab = Instantiate(GameManager.Instance.worldCustomerList[i], transform.position, transform.rotation);
        //        instantiatedPrefab.transform.position = new Vector3(Random.Range(-7.56f, -0.647f), -8.752809f, Random.Range(14.563f, 11.2f));
        //        instantiatedPrefab.transform.rotation = new Quaternion(0, Random.Range(0, 360), 0, 0);
        //        instantiatedCustomer.Add(instantiatedPrefab);

        //        //dapetin canvasUIPertanyaan yang mau di assign
        //        Temp = instantiatedPrefab.transform.Find("Customer").GetComponent<People>();
        //        peopleScript.Add(Temp);

        //        Canvas canvasUIPertanyaan = peopleScript[i].UIPertanyaan.GetComponent<Canvas>();


        //        /*//reference si cameracaster
        //        Camera cameraCaster = GameObject.Find("CameraCaster").GetComponent<Camera>();

        //        canvasUIPertanyaan.renderMode = RenderMode.WorldSpace;
        //        canvasUIPertanyaan.worldCamera = cameraCaster;//
        //        //masih ngebug di worldcamera harusnya itu eventCamera
        //        canvasUIPertanyaan.worldCamera = cameraCaster;*/
        //    }
        //}
        if (isCafeSpawner)
        {

            for (int i = 0; i < GameManager.Instance.CafeCustomerCount; i++)
            {
                Debug.Log(GameManager.Instance.cafeCustomerList.Count);
                GameObject instantiatedPrefab = Instantiate(GameManager.Instance.cafeCustomerList[i], transform.position, transform.rotation);
                if(spawnerRefScript != null && instantiatedPrefab != null)
                {
                    spawnerRefScript.SetPosition(instantiatedPrefab);
                    Debug.Log(instantiatedPrefab.transform.position);
                }
                else if(spawnerRefScript == null && instantiatedPrefab != null)
                {
                    instantiatedPrefab.transform.position = new Vector3(Random.Range(-7.705f, 0.91f), -8.732f, Random.Range(9.591f, 15.884f));
                    instantiatedPrefab.transform.rotation = new Quaternion(0,Random.Range(0,360), 0, 0);
                    Debug.Log("spawned NOT on ref");
                }

                instantiatedCustomer.Add(instantiatedPrefab);


                //dapetin canvasUIPertanyaan yang mau di assign
                Temp = instantiatedPrefab.transform.Find("Customer").GetComponent<People>();
                peopleScript.Add(Temp);

                Canvas canvasUIPertanyaan = peopleScript[i].UIPertanyaan.GetComponent<Canvas>();


                //reference si cameracaster
                Camera cameraCaster = GameObject.Find("CameraCaster").GetComponent<Camera>();

                canvasUIPertanyaan.renderMode = RenderMode.WorldSpace;
                canvasUIPertanyaan.worldCamera = cameraCaster;//
                //masih ngebug di worldcamera harusnya itu eventCamera
                canvasUIPertanyaan.worldCamera = cameraCaster;
            }
        }
        else if(isRestaurantSpawner)
        {

            for (int i = 0; i < GameManager.Instance.RestaurantCustomerCount; i++)
            {
                GameObject instantiatedPrefab = Instantiate(GameManager.Instance.restaurantCustomerList[i], transform.position, transform.rotation);
                instantiatedPrefab.transform.position = new Vector3(Random.Range(-7.705f, 0.91f), -8.732f, Random.Range(9.591f, 15.884f));
                instantiatedPrefab.transform.rotation = new Quaternion(0, Random.Range(0, 360), 0, 0);
                instantiatedCustomer.Add(instantiatedPrefab);


                //dapetin canvasUIPertanyaan yang mau di assign
                Temp = instantiatedPrefab.transform.Find("Customer").GetComponent<People>();
                peopleScript.Add(Temp);

                Canvas canvasUIPertanyaan = peopleScript[i].UIPertanyaan.GetComponent<Canvas>();


                //reference si cameracaster
                Camera cameraCaster = GameObject.Find("CameraCaster").GetComponent<Camera>();
                canvasUIPertanyaan.renderMode = RenderMode.WorldSpace;
                canvasUIPertanyaan.worldCamera = cameraCaster;//
                //masih ngebug di worldcamera harusnya itu eventCamera
                canvasUIPertanyaan.worldCamera = cameraCaster;
            }
        }
        else if(isCanteenSpawner)
        {

            for (int i = 0; i < GameManager.Instance.CanteenCustomerCount; i++)
            {
                GameObject instantiatedPrefab = Instantiate(GameManager.Instance.canteenCustomerList[i], transform.position, transform.rotation);
                instantiatedPrefab.transform.position = new Vector3(Random.Range(-7.705f, 0.91f), -8.732f, Random.Range(9.591f, 15.884f));
                instantiatedPrefab.transform.rotation = new Quaternion(0, Random.Range(0, 360), 0, 0);
                instantiatedCustomer.Add(instantiatedPrefab);

                //dapetin canvasUIPertanyaan yang mau di assign
                Temp = instantiatedPrefab.transform.Find("Customer").GetComponent<People>();
                peopleScript.Add(Temp);

                Canvas canvasUIPertanyaan = peopleScript[i].UIPertanyaan.GetComponent<Canvas>();
            }
        }
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
