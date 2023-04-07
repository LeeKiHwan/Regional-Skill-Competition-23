using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("MenuUI")]
    public RectTransform menuUI;
    public TextMeshProUGUI gameNameText;

    [Header("Dif UI")]
    public Button[] difBtns;
    public int dif;

    [Header("Help UI")]
    public GameObject helpUI;
    public GameObject[] helpUIs;
    public int helpUIIndex;
    public GameObject nextBtn;
    public GameObject prevBtn;

    private void Awake()
    {
        //StartCoroutine(MoveMenuUI());
        StartCoroutine(GameNameEffect());
    }

    //public IEnumerator MoveMenuUI()
    //{
    //    while (menuUI.anchoredPosition.y < 0)
    //    {
    //        menuUI.transform.Translate(0,1000 * Time.deltaTime, 0);
    //        yield return null;
    //    }

    //    yield break;
    //}

    public IEnumerator GameNameEffect()
    {


        yield break;
    }

    public void DifBtnClick(int index)
    {
        dif = index;
        InGameManager.dif = dif;

        for (int i = 0; i < difBtns.Length; i++)
        {
            if (i == index) difBtns[i].interactable = false;
            else difBtns[i].interactable = true;
        }
    }

    public void GameStart()
    {
        if (dif == 0)
        {
            Debug.Log("난이도를 선택하시오.");
        }
        else
        {
            SceneManager.LoadScene("InGameScene");
        }
    }

    public void OnHelpUI()
    {
        helpUIIndex = 0;
        helpUI.SetActive(true);
        helpUIs[0].SetActive(true);
        nextBtn.SetActive(true);
        prevBtn.SetActive(false);
    }

    public void HelpUIChange(int page)
    {
        helpUIIndex += page;

        for (int i = 0; i<helpUIs.Length; i++)
        {
            if (helpUIIndex == i) helpUIs[i].SetActive(true);
            else helpUIs[i].SetActive(false);
        }

        if (helpUIIndex == 0) prevBtn.SetActive(false);
        else if (helpUIIndex == helpUIs.Length - 1) nextBtn.SetActive(false);
        else
        {
            nextBtn.SetActive(true);
            prevBtn.SetActive(true);
        }
    }
}
