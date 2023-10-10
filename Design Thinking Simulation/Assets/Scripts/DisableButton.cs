using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    public void UninteractButton()
    {
        GetComponent<Button>().interactable = false;
    }
}
