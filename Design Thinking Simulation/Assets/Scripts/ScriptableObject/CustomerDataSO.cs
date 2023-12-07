using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "New CustomerData/Create CustomerData")]

public class CustomerDataSO : ScriptableObject
{
    public string peopleName;
    [field: SerializeField]
    public CakePreferencesSO cakePreferences { get;  set; }
    public CakeSO kueFavorit;
    public List<string> goals;
    public List<string> frustration;
    public bool met { get; set; } = false;
    public bool hasShowUserPersona;

    public int CalculateLikeness(int index)
    {
        int likeCake = 0;
        int dislikeCake = 0;
        for (int i = 0; i < cakePreferences.LikeCake.Count; i++)
        {

            likeCake += cakePreferences.LikeCake[i].taste[index];
        }

        for (int i = 0; i < cakePreferences.DislikeCake.Count; i++)
        {

            dislikeCake += cakePreferences.DislikeCake[i].taste[index];
        }

        return likeCake - dislikeCake;
    }

    public int CalculateLikeness(CakeSO givenCake)
    {
        int tempTaste = 0;
        for (int i=0; i < givenCake.taste.Count; i++)
        {
            if (givenCake.taste[i] == cakePreferences.LikeCake[0].taste[i])
            {
                tempTaste += 1;
            }
        }
        return tempTaste;
    }
}
