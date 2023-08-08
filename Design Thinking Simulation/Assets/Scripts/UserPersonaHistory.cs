using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserPersonaHistory : MonoBehaviour
{

    [SerializeField] List<CustomerDataSO> userPersonaList;
    public TMP_Text nameText;
    public TMP_Text kueFavoritText;
    public TMP_Text goalsText;
    public TMP_Text frustrationText;
    public List<Toggle> tasteToggleList;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = userPersonaList[index].peopleName;
        kueFavoritText.text = userPersonaList[index].kueFavorit;
        goalsText.text = userPersonaList[index].goals[0];
        frustrationText.text = userPersonaList[index].frustration[0];
    }

    public void  FindCustomer(CustomerDataSO customer)
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

    public void Prev()
    {
        if (index != 0)
        {
            index--;
        }
    }

    public void Next()
    {
        if (index != userPersonaList.Count - 1 && userPersonaList.Count!=0)
        {
            index++;
        }
    }

    public void FavoritCakeAnswer(TMP_Text answer)
    {
        userPersonaList[index].kueFavorit = answer.text;
    }

    public void GoalsAnswer(TMP_Text answer)
    {
        userPersonaList[index].goals[0] = answer.text;
    }

    public void FrustrationAnswer(TMP_Text answer)
    {
        userPersonaList[index].frustration[0] = answer.text;
    }
}
