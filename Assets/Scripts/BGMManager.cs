using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioSource bgmSource; // BGM�� ����� AudioSource
    public AudioClip defaultBGM;  // �⺻ BGM (���۾�)

    private void Awake()
    {
        // �̱��� ����: �̹� BGMManager�� �����ϸ� ���� ������ ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� ������Ʈ�� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �����ϴ� BGMManager�� ������ ���� ������ ������Ʈ�� ����
        }
    }

    private void Start()
    {
        // ���� ���� �� �⺻ BGM ���
        if (defaultBGM != null)
        {
            PlayBGM(defaultBGM);
        }
    }

    // BGM ��� �Լ�
    public void PlayBGM(AudioClip bgm, bool loop = true)
    {
        if (bgmSource != null && bgm != null)
        {
            bgmSource.clip = bgm;
            bgmSource.loop = loop;
            bgmSource.pitch = 1f;  // �׻� 1������� ����ǵ��� �ʱ�ȭ
            bgmSource.Play();
        }
    }

    // BGM ���߱�
    public void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    // BGM �ӵ� ����
    public void SetBGMSpeed(float speed)
    {
        if (bgmSource != null)
        {
            bgmSource.pitch = speed;
        }
    }
}
