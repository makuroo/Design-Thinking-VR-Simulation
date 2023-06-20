using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public bool isWorldSpawner;
    public bool isCafeSpawner;
    public bool isRestaurantSpawner;
    public bool isCanteenSpawner;

    int WorldCustomerCount;
    int CafeCustomerCount;
    int RestaurantCustomerCount;
    int CanteenCustomerCount;

   [SerializeField] int MaxWorldCustomerCount;
    [SerializeField] int MaxCafeCustomerCount;
    [SerializeField] int MaxRestaurantCustomerCount;
    [SerializeField] int MaxCanteenCustomerCount;

    public GameObject prefabCustomer;
    public List<GameObject> instantiatedCustomer = new List<GameObject>();
    List<People> peopleScript = new List<People>();
    public People Temp;

    private void Awake()
    {
        WorldCustomerCount = Random.Range(1, MaxWorldCustomerCount);
        CafeCustomerCount = Random.Range(1, MaxCafeCustomerCount);
        RestaurantCustomerCount = Random.Range(1, MaxRestaurantCustomerCount);
        CanteenCustomerCount = Random.Range(1, MaxCanteenCustomerCount);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isWorldSpawner)
        {
            Debug.Log(WorldCustomerCount);
            for (int i = 0; i < WorldCustomerCount; i++)
            {
                GameObject instantiatedPrefab = Instantiate(prefabCustomer, transform.position, transform.rotation);
                instantiatedPrefab.transform.position = new Vector3(Random.Range(-7.56f, -0.647f), -8.752809f, Random.Range(14.563f, 11.2f));
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
        else if(isCafeSpawner)
        {

            for (int i = 0; i < CafeCustomerCount; i++)
            {
                GameObject instantiatedPrefab = Instantiate(prefabCustomer, transform.position, transform.rotation);
                instantiatedPrefab.transform.position = new Vector3(Random.Range(-7.705f, 0.91f), -8.732f, Random.Range(9.591f, 15.884f));
                instantiatedPrefab.transform.rotation = new Quaternion(0,Random.Range(0,360), 0, 0);
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

            for (int i = 0; i < RestaurantCustomerCount; i++)
            {
                GameObject instantiatedPrefab = Instantiate(prefabCustomer, transform.position, transform.rotation);
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

            for (int i = 0; i < CanteenCustomerCount; i++)
            {
                GameObject instantiatedPrefab = Instantiate(prefabCustomer, transform.position, transform.rotation);
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
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
