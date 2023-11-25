using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserPersonaHistory : MonoBehaviour
{
    [System.Serializable]
    public struct KeyValuePairs
    {
        public string key;
        public CustomerDataSO value;
    }

    [SerializeField] List<CustomerDataSO> userPersonaList;
    public Dictionary<string,CustomerDataSO> userPersonaDict = new();
    public TMP_Text nameText;
    public TMP_Text kueFavoritText;
    public TMP_Text goalsText;
    public TMP_Text frustrationText;
    public List<Toggle> dislikeTasteToggleList;

    int index = 0;

    public List<KeyValuePairs> dictValues = new();

    // Start is called before the first frame update
    void Start()
    {
        kueFavoritText.text = "???";
        for(int i=0; i < userPersonaList.Count; i++)
        {
            userPersonaList[i].peopleName = "???";
            userPersonaList[i].kueFavorit = null;
            userPersonaList[i].goals[0] = "???";
            userPersonaList[i].frustration[0] = "???";
        }

    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = userPersonaList[index].peopleName;
        if(userPersonaList[index].kueFavorit!=null)
            kueFavoritText.text = userPersonaList[index].kueFavorit.cakeName;
        goalsText.text = userPersonaList[index].goals[0];
        frustrationText.text = userPersonaList[index].frustration[0];
    }

    public void  FindCustomer(CustomerDataSO customer)
    {
        if (customer == null)
            Debug.Log("null");
        else
        {
            for (int i = 0; i < userPersonaList.Count; i++)
            {
                if (userPersonaList[i].peopleName == customer.peopleName)
                {
                    index = i;

                    break;
                }
                else if (userPersonaList[i].peopleName == "???")
                {
                    userPersonaList[i].peopleName = customer.peopleName;
                    index = i;
                    break;
                }
                else
                {
                    index++;
                }
            }
        }
        
    }

    public void Prev()
    {
        if (index != 0)
        {
            index--;
        }
    }

    public void Next()
    {
        if (index != userPersonaList.Count - 1 && userPersonaList.Count>0)
        {
            index++;
        }
    }

    public void FavoritCakeAnswer(CustomerDataSO customer)
    {
        userPersonaList[index].kueFavorit =customer.kueFavorit;
    }

    public void GoalsAnswer(TMP_Text answer)
    {
        userPersonaList[index].goals[0] = answer.text;
    }

    public void FrustrationAnswer(TMP_Text answer)
    {
        userPersonaList[index].frustration[0] = answer.text;
    }    
    public void CakePreferenceAnswer(CakePreferencesSO preference)
    {
       Debug.Log(userPersonaList[index]);
       userPersonaList[index].cakePreferences = preference;
    }

    public void AddToDict(string key, CustomerDataSO value)
    {
        if (userPersonaDict.TryAdd(key, value))
        {
            KeyValuePairs datas = new();
            datas.key = key;
            datas.value = value;
            dictValues.Add(datas);
            Debug.Log("here");
        }
    }
}
