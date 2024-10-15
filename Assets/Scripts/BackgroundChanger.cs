using UnityEngine;
using System.Collections;

public class BackgroundFader : MonoBehaviour
{
    public SpriteRenderer backgroundRenderer1; // 첫 번째 배경의 SpriteRenderer
    public SpriteRenderer backgroundRenderer2; // 두 번째 배경의 SpriteRenderer
    public float fadeDuration = 2f; // 페이드 효과가 걸리는 시간 (초)

    private bool isFading = false;
    private float fadeTimer = 0f;
    private bool isBackground1Active = true;

    void Start()
    {
        // 첫 번째 배경을 완전히 불투명하게, 두 번째 배경을 투명하게 시작
        backgroundRenderer1.color = new Color(1, 1, 1, 1); // Alpha 1
        backgroundRenderer2.color = new Color(1, 1, 1, 0); // Alpha 0
    }

    void Update()
    {
        // 일정 시간마다 배경을 전환하도록 설정 (5초마다 전환 예시)
        if (!isFading && fadeTimer >= 5f)
        {
            StartCoroutine(FadeBackgrounds());
            fadeTimer = 0f; // 타이머 리셋
        }

        fadeTimer += Time.deltaTime;
    }

    // 페이드 효과를 위한 코루틴
    IEnumerator FadeBackgrounds()
    {
        isFading = true;
        float t = 0f;

        // 배경 전환 시작
        while (t < fadeDuration)
        {
            t += Time.deltaTime / fadeDuration;

            // Alpha 값을 Lerp로 점진적으로 변화시킴
            if (isBackground1Active)
            {
                // 첫 번째 배경은 페이드 아웃, 두 번째 배경은 페이드 인
                backgroundRenderer1.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, t));
                backgroundRenderer2.color = new Color(1, 1, 1, Mathf.Lerp(0f, 1f, t));
            }
            else
            {
                // 두 번째 배경은 페이드 아웃, 첫 번째 배경은 페이드 인
                backgroundRenderer1.color = new Color(1, 1, 1, Mathf.Lerp(0f, 1f, t));
                backgroundRenderer2.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, t));
            }

            yield return null; // 프레임마다 업데이트
        }

        // 배경 전환 완료 후 배경 플래그 전환
        isBackground1Active = !isBackground1Active;
        isFading = false;
    }
}
