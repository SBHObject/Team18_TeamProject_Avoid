using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneBGMcontroller : MonoBehaviour
{
    public AudioClip mainBGM; // ���� ������ ����� BGM
    public AudioClip failBGM; //���� BGM
    private float bgmSpeed = 1f; // BGM �ʱ� �ӵ�
    private float elapsedTime = 0f;
    private bool isGameRunning = true;

    void Start()
    {
        // ���� ���� ������ ���� BGM�� ���
        if (BGMManager.Instance != null && mainBGM != null)
        {
            BGMManager.Instance.PlayBGM(mainBGM);
            // 60�� ���� BGM �ӵ��� 1��ӿ��� 2������� ������Ű�� �ڷ�ƾ ����
            StartCoroutine(IncreaseBGMSpeedOverTime());
        }
    }
    IEnumerator IncreaseBGMSpeedOverTime()
    {
        // 60�� ���� BGM �ӵ��� 1��ӿ��� 2������� ���������� ����
        while (elapsedTime < 60f && isGameRunning)
        {
            elapsedTime += Time.deltaTime;
            float bgmSpeed = Mathf.Lerp(1f, 2f, elapsedTime / 60f);
            BGMManager.Instance.SetBGMSpeed(bgmSpeed);
            yield return null; // �� �����Ӹ��� ����
        }
    }
    public void End()
    {   // ���� ���� �� BGM�� ���߰�, ���� ���� BGM�� �� �� ��� �� ���� ó��
        // BGM �ӵ��� 1������� �ٽ� ���� �� ���� BGM ���
        isGameRunning = false; // ������ ����Ǹ� �� �̻� BGM �ӵ��� �������� �ʵ��� ����

        // BGM �ӵ��� 1������� �ٽ� ���� �� ���� BGM ���
        BGMManager.Instance.StopBGM();
        BGMManager.Instance.PlayBGM(failBGM, false); // ���� BGM ��� (�� ����)
    }

}