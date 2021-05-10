using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeDisplay : MonoBehaviour
{
    public enum CHALLENGE
    {
        SCORE,
        COMBO,
        LEVEL,
        BUBBLES,
        FULLCLEAR,
    }
    public CHALLENGE challenge;

    Image image;
    Image fill;
    Text challengeText;
    string challengeTextInitialString;
    Text progressText;

    private void Awake()
    {
        image = GetComponent<Image>();
        fill = transform.Find("Fill").GetComponent<Image>();
        progressText = transform.Find("ChallengeProgress").GetComponent<Text>();
        challengeText = transform.Find("ChallengeName").GetComponent<Text>();
        challengeTextInitialString = challengeText.text;
    }

    public void Setup()
    {
        int playerProgress;
        int[] thresholds;
        int currentThreshold = -1;

        switch (challenge)
        {
            case CHALLENGE.SCORE:
                playerProgress = PlayerStats.Instance.Data.BestScore;
                thresholds = PlayerStats.ScoreThresholds;
                break;
            case CHALLENGE.COMBO:
                playerProgress = PlayerStats.Instance.Data.BestCombo;
                thresholds = PlayerStats.ComboThresholds;
                break;
            case CHALLENGE.LEVEL:
                playerProgress = PlayerStats.Instance.Data.BestLevel;
                thresholds = PlayerStats.LevelThresholds;
                break;
            case CHALLENGE.BUBBLES:
                playerProgress = PlayerStats.Instance.Data.TotalBubblesPopped;
                thresholds = PlayerStats.TotalBubblesPoppedThresholds;
                break;
            case CHALLENGE.FULLCLEAR:
                playerProgress = PlayerStats.Instance.Data.LevelsFullyCleared;
                thresholds = PlayerStats.LevelsFullyClearedThresholds;
                break;
            default:
                playerProgress = 0;
                thresholds = new int[] { 0 };
                break;
        }

        int i;
        for (i = 0; i < thresholds.Length; ++i)
        {
            if (playerProgress < thresholds[i])
            {
                currentThreshold = thresholds[i];
                break;
            }
        }

        if (i == thresholds.Length)
        {
            currentThreshold = thresholds[thresholds.Length - 1];
            i--;
        }

        string progressString = playerProgress.ToString() + '/' + currentThreshold + ' ';
        progressText.text = progressString;
        string challengeString = challengeTextInitialString + ' ';
        for (; i >= 0; i--)
        {
            challengeString += 'I';
        }
        challengeText.text = challengeString;
        fill.fillAmount = Mathf.Min(1, (float)playerProgress / currentThreshold);
    }
}
