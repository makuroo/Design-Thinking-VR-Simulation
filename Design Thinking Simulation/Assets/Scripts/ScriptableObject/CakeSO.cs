using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ukuran
{
    SangatKecil,
    Kecil,
    Sedang,
    Besar,
    SangatBesar
}

[CreateAssetMenu(fileName ="Cake", menuName = "New Cake/Create Cake")]
public class CakeSO : ScriptableObject
{
    public string cakeName;
    // 0. manis 1. asin 2. asem 3. pahit 4. susu 5. coklat 6. vanila
    public List<int> taste = new List<int>();
    public Ukuran ukuranKue;
    public Dictionary<int, string> cakeComponent = new Dictionary<int, string>();
}