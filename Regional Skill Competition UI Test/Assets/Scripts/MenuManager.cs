using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public Button[] difBtns;

    public void DifBtnClick(int index)
    {
        for (int i = 0; i < difBtns.Length; i++)
        {
            if (i == index)
            {
                difBtns[i].interactable = false;
            }
            else
            {
                difBtns[i].interactable = true;
            }
        }
    }
}
