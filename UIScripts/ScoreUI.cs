using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Instance;

    public int currentLevel { get; private set; }
    Text Level;
    Text Score;

    private void Awake()
    {
        Instance = this;
        Level = transform.Find("Level").GetComponent<Text>();
        Score = transform.Find("Score").GetComponent<Text>();
        gameObject.SetActive(false);
    }

    public void SetScore(int score)
    {
        Score.text = score.ToString();
    }

    public void IncrementLevel()
    {
        currentLevel++;
        Level.text = "Lvl  " + currentLevel.ToString();

        if (currentLevel > PlayerStats.Instance.Data.BestLevel)
        {
            PlayerStats.Instance.Data.BestLevel = currentLevel;
        }
    }

    public void Reset()
    {
        currentLevel = 1;
        Level.text = "Lvl  " + 1.ToString();
        Score.text = 0.ToString();
    }
}
