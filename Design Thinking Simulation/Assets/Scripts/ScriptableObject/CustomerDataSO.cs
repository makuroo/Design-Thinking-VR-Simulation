using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "New CustomerData/Create CustomerData")]

public class CustomerDataSO : ScriptableObject
{
    public string peopleName;
    public string kueFavorit;
    public List<string> goals;
    public List<string> frustration;
    public bool met = false;
}
