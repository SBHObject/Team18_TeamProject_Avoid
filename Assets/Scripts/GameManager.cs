using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject spear;
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
        InvokeRepeating("MakeSpear", 0f , 0.2f);

    }

    void MakeSpear()
    {
        Instantiate(spear);
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
