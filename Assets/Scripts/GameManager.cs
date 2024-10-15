using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject rain;
    public GameObject endPanel;

    public Text totalScoreTxt;

    int totalScore;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeRain", 0f , 1f);

    }

    void MakeRain()
    {
        Instantiate(rain);
    }  

    public void AddScore(int score)
    {
        totalScore += score;
        totalScoreTxt.text = totalScore.ToString();
     
    }

    public void EndGame()
    {
        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }
} 
