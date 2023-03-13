using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    //Ʃ�丮�󿡼� ������ �� : �����̴¹�, �Ѿ˹߻�, ������, ��ų ����, �� ��

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
        StartCoroutine(Explain("2048��"));
        yield return new WaitForSeconds(2f);
        StartCoroutine(Explain("�η��� �ڿ����ط� ����� ������ ���� �ΰ��� ��ư� �� �ִ� ���ο� �༺�� ã���� ���ַ� ������ �ȴ�."));
        yield return new WaitForSeconds(7f);
        StartCoroutine(Explain("�ȳ��ϼ���. �÷��̾��, ���� �� ���ֺ��༱�� ����̽ý����Դϴ�."));
        yield return new WaitForSeconds(6f);
        StartCoroutine(Explain("���ݺ��� ������ ���ֿ����� �� �� �ֵ��� ������ Ʃ�丮���� �����ϰڽ��ϴ�."));
        yield return new WaitForSeconds(6f);

        StartCoroutine(MoveAndFireTutorial());

        yield break;
    }

    IEnumerator MoveAndFireTutorial()
    {
        StartCoroutine(Explain("���ֺ��༱�� \"WASD\" �Ǵ� ����Ű�� ������ �� �ֽ��ϴ�."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("�⺻�Ѿ˹߻�� \"J\"Ű �Դϴ�."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("���� �Ѿ˿� �ǰݴ��ϰų� ��� ������ ü���� �����մϴ�."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("�Ϲ� ���͵��� ��ȯ�ɶ��� ���ᰡ �����մϴ�."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("ü���̳� ���ᰡ 0�� �Ǹ� ���ӿ����� �˴ϴ�."));
        yield return new WaitForSeconds(5f);

        StartCoroutine(ItemTutorial());

        yield break;
    }

    IEnumerator ItemTutorial()
    {
        StartCoroutine(Explain("�� �Ǵ� ��� �μ��� ����Ȯ���� �������� ����˴ϴ�."));
        yield return new WaitForSeconds(5f);
        ItemUI.SetActive(true);
        StartCoroutine(Explain("H:ü��ȸ��, F:����ȸ��, B:�⺻�Ѿ� ���׷��̵�, I:5�ʵ��� ���� �Դϴ�."));
        yield return new WaitForSeconds(9f);
        StartCoroutine(Explain("����ȸ���� �Ϲ� �����ۿ� ���� ������� �����ϴ�."));
        yield return new WaitForSeconds(5f);
        ItemUI.SetActive(false);

        StartCoroutine(SkillTutorial());

        yield break;
    }

    IEnumerator SkillTutorial()
    {
        StartCoroutine(Explain("������ �÷����ϴ� ���� ��ų�� ����� �� �ֽ��ϴ�."));
        yield return new WaitForSeconds(5f);
        SkillUI.SetActive(true);
        StartCoroutine(Explain("\"K\"Ű�� ����ϸ� ��ź�� ����Ͽ� ���� ���� �Ѿ�, ��� ���� �� �ֽ��ϴ�."));
        yield return new WaitForSeconds(8f);
        StartCoroutine(Explain("�� �������Դ� �������� ������� �� �� �����ϴ�."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("\"L\"Ű�� ����ϸ� ü���� 20 ȸ�� �� �� �ֽ��ϴ�."));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Explain("�� ��ų�� 10���� ��Ÿ���� ������, �� �������� �� 3���� ��밡���մϴ�."));
        yield return new WaitForSeconds(8f);
        SkillUI.SetActive(false);

        StartCoroutine(LastTutorial());

        yield break;
    }

    IEnumerator LastTutorial()
    {
        StartCoroutine(Explain("�̻� Ʃ�丮���� ��ġ�ڽ��ϴ�."));
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
