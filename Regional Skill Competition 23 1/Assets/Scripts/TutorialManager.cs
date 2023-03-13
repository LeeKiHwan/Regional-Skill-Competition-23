using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    //튜토리얼에서 설명할 것 : 움직이는법, 총알발사, 아이템, 스킬 사용법, 그 외

    public TextMeshProUGUI ExplainText;
    public GameObject ItemUI;
    public GameObject SkillUI;

    private void Start()
    {
        StartCoroutine(FirstExplain());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    IEnumerator FirstExplain()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(Explain("2048년"));
        yield return new WaitForSeconds(2f);
        StartCoroutine(Explain("인류는 자연재해로 멸망한 지구를 떠나 인간이 살아갈 수 있는 새로운 행성을 찾으러 우주로 나가게 된다."));
        yield return new WaitForSeconds(7f);
        StartCoroutine(Explain("안녕하세요. 플레이어님, 저는 이 우주비행선의 도우미시스템입니다."));
        yield return new WaitForSeconds(6f);
        StartCoroutine(Explain("지금부터 안전한 우주여행을 할 수 있도록 간단한 튜토리얼을 시작하겠습니다."));
        yield return new WaitForSeconds(6f);

        StartCoroutine(MoveAndFireTutorial());

        yield break;
    }

    IEnumerator MoveAndFireTutorial()
    {
        StartCoroutine(Explain("우주비행선은 \"WASD\" 또는 방향키로 조종할 수 있습니다."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("기본총알발사는 \"J\"키 입니다."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("적의 총알에 피격당하거나 운석에 맞으면 체력이 감소합니다."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("일반 몬스터들이 소환될때는 연료가 감소합니다."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("체력이나 연료가 0이 되면 게임오버가 됩니다."));
        yield return new WaitForSeconds(5f);

        StartCoroutine(ItemTutorial());

        yield break;
    }

    IEnumerator ItemTutorial()
    {
        StartCoroutine(Explain("적 또는 운석을 부수면 일정확률로 아이템이 드랍됩니다."));
        yield return new WaitForSeconds(5f);
        ItemUI.SetActive(true);
        StartCoroutine(Explain("H:체력회복, F:연료회복, B:기본총알 업그레이드, I:5초동안 무적 입니다."));
        yield return new WaitForSeconds(9f);
        StartCoroutine(Explain("연료회복은 일반 아이템에 비해 드랍률이 높습니다."));
        yield return new WaitForSeconds(5f);
        ItemUI.SetActive(false);

        StartCoroutine(SkillTutorial());

        yield break;
    }

    IEnumerator SkillTutorial()
    {
        StartCoroutine(Explain("게임을 플레이하는 동안 스킬을 사용할 수 있습니다."));
        yield return new WaitForSeconds(5f);
        SkillUI.SetActive(true);
        StartCoroutine(Explain("\"K\"키를 사용하면 폭탄을 사용하여 적과 적의 총알, 운석을 없앨 수 있습니다."));
        yield return new WaitForSeconds(8f);
        StartCoroutine(Explain("단 보스에게는 직접적인 대미지를 줄 수 없습니다."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("\"L\"키를 사용하면 체력을 20 회복 할 수 있습니다."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("각 스킬은 10초의 쿨타임을 가지고, 각 스테이지 당 3번씩 사용가능합니다."));
        yield return new WaitForSeconds(8f);
        SkillUI.SetActive(false);

        StartCoroutine(LastTutorial());

        yield break;
    }

    IEnumerator LastTutorial()
    {
        StartCoroutine(Explain("이상 튜토리얼을 마치겠습니다."));
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("SampleScene");

        yield break;
    }

    IEnumerator Explain(string text)
    {
        ExplainText.text = "";
        for (int i=0;i<text.Length;i++)
        {
            ExplainText.text += text[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }
}
