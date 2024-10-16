using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioSource bgmSource; // BGM을 재생할 AudioSource
    public AudioClip defaultBGM;  // 기본 BGM (시작씬)

    private void Awake()
    {
        // 싱글턴 패턴: 이미 BGMManager가 존재하면 새로 생성된 것은 삭제
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 오브젝트가 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 존재하는 BGMManager가 있으면 새로 생성된 오브젝트를 삭제
        }
    }

    private void Start()
    {
        // 시작 씬일 때 기본 BGM 재생
        if (defaultBGM != null)
        {
            PlayBGM(defaultBGM);
        }
    }

    // BGM 재생 함수
    public void PlayBGM(AudioClip bgm, bool loop = true)
    {
        if (bgmSource != null && bgm != null)
        {
            bgmSource.clip = bgm;
            bgmSource.loop = loop;
            bgmSource.pitch = 1f;  // 항상 1배속으로 재생되도록 초기화
            bgmSource.Play();
        }
    }

    // BGM 멈추기
    public void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    // BGM 속도 설정
    public void SetBGMSpeed(float speed)
    {
        if (bgmSource != null)
        {
            bgmSource.pitch = speed;
        }
    }
}
