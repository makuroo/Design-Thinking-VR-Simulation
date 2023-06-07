using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public bool isWorldSpawner;
    public bool isCafeSpawner;
    public bool isRestaurantSpawner;
    public bool isCanteenSpawner;


    [SerializeField] int WorldCustomerCount;
    [SerializeField] int CafeCustomerCount;
    [SerializeField] int RestaurantCustomerCount;
    [SerializeField] int CanteenCustomerCount;

    public GameObject prefabCustomer;
    List<GameObject> instantiatedCustomer = new List<GameObject>();

    private void Awake()
    {
        WorldCustomerCount = Random.Range(1, 4);
        CafeCustomerCount = Random.Range(1, 4);
        RestaurantCustomerCount = Random.Range(1, 4);
        CanteenCustomerCount = Random.Range(1, 4);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isWorldSpawner)
        {
            for (int i = 0; i < WorldCustomerCount; i++)
            {
                GameObject instantiatedPrefab = Instantiate(prefabCustomer, transform.position, transform.rotation);
                instantiatedPrefab.transform.position = new Vector3(Random.Range(-7.56f, -0.647f), -8.752809f, Random.Range(14.563f, 11.2f));
                instantiatedPrefab.transform.rotation = new Quaternion(0, Random.Range(0, 360), 0, 0);
                instantiatedCustomer.Add(instantiatedPrefab);
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
            }
        }
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
