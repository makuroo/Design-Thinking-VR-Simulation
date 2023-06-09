using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmpathyMap", menuName = "New EmpathyMap/Create EmpathyMap")]

public class EmpathyMapSO : ScriptableObject
{
    public List<string> Says = new List<string>();
    public List<string> Thinks = new List<string>();
    public List<string> Does = new List<string>();
    public List<string> Feels = new List<string>();
}
