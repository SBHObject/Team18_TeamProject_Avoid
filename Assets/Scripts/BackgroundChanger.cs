using UnityEngine;
using System.Collections;

public class BackgroundFader : MonoBehaviour
{
    public SpriteRenderer backgroundRenderer1; // ù ��° ����� SpriteRenderer
    public SpriteRenderer backgroundRenderer2; // �� ��° ����� SpriteRenderer
    public float fadeDuration = 2f; // ���̵� ȿ���� �ɸ��� �ð� (��)

    private bool isFading = false;
    private float fadeTimer = 0f;
    private bool isBackground1Active = true;

    void Start()
    {
        // ù ��° ����� ������ �������ϰ�, �� ��° ����� �����ϰ� ����
        backgroundRenderer1.color = new Color(1, 1, 1, 1); // Alpha 1
        backgroundRenderer2.color = new Color(1, 1, 1, 0); // Alpha 0
    }

    void Update()
    {
        // ���� �ð����� ����� ��ȯ�ϵ��� ���� (5�ʸ��� ��ȯ ����)
        if (!isFading && fadeTimer >= 5f)
        {
            StartCoroutine(FadeBackgrounds());
            fadeTimer = 0f; // Ÿ�̸� ����
        }

        fadeTimer += Time.deltaTime;
    }

    // ���̵� ȿ���� ���� �ڷ�ƾ
    IEnumerator FadeBackgrounds()
    {
        isFading = true;
        float t = 0f;

        // ��� ��ȯ ����
        while (t < fadeDuration)
        {
            t += Time.deltaTime / fadeDuration;

            // Alpha ���� Lerp�� ���������� ��ȭ��Ŵ
            if (isBackground1Active)
            {
                // ù ��° ����� ���̵� �ƿ�, �� ��° ����� ���̵� ��
                backgroundRenderer1.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, t));
                backgroundRenderer2.color = new Color(1, 1, 1, Mathf.Lerp(0f, 1f, t));
            }
            else
            {
                // �� ��° ����� ���̵� �ƿ�, ù ��° ����� ���̵� ��
                backgroundRenderer1.color = new Color(1, 1, 1, Mathf.Lerp(0f, 1f, t));
                backgroundRenderer2.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, t));
            }

            yield return null; // �����Ӹ��� ������Ʈ
        }

        // ��� ��ȯ �Ϸ� �� ��� �÷��� ��ȯ
        isBackground1Active = !isBackground1Active;
        isFading = false;
    }
}
