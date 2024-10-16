using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneBGMcontroller : MonoBehaviour
{
    public AudioClip mainBGM; // 메인 씬에서 재생할 BGM
    public AudioClip failBGM; //실패 BGM
    private float bgmSpeed = 1f; // BGM 초기 속도
    private float elapsedTime = 0f;
    private bool isGameRunning = true;

    void Start()
    {
        // 메인 씬에 들어오면 메인 BGM을 재생
        if (BGMManager.Instance != null && mainBGM != null)
        {
            BGMManager.Instance.PlayBGM(mainBGM);
            // 60초 동안 BGM 속도를 1배속에서 2배속으로 증가시키는 코루틴 시작
            StartCoroutine(IncreaseBGMSpeedOverTime());
        }
    }
    IEnumerator IncreaseBGMSpeedOverTime()
    {
        // 60초 동안 BGM 속도를 1배속에서 2배속으로 점진적으로 증가
        while (elapsedTime < 60f && isGameRunning)
        {
            elapsedTime += Time.deltaTime;
            float bgmSpeed = Mathf.Lerp(1f, 2f, elapsedTime / 60f);
            BGMManager.Instance.SetBGMSpeed(bgmSpeed);
            yield return null; // 매 프레임마다 갱신
        }
    }
    public void End()
    {   // 게임 종료 시 BGM을 멈추고, 게임 실패 BGM을 한 번 재생 후 실패 처리
        // BGM 속도를 1배속으로 다시 설정 후 실패 BGM 재생
        isGameRunning = false; // 게임이 종료되면 더 이상 BGM 속도가 증가하지 않도록 설정

        // BGM 속도를 1배속으로 다시 설정 후 실패 BGM 재생
        BGMManager.Instance.StopBGM();
        BGMManager.Instance.PlayBGM(failBGM, false); // 실패 BGM 재생 (한 번만)
    }

}