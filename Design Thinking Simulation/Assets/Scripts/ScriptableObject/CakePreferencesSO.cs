using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CakePreference", menuName = "Preference/Create Preference")]
public class CakePreferencesSO : ScriptableObject
{
    public List<CakeSO> LikeCake;
    public List<CakeSO> DislikeCake;
}
