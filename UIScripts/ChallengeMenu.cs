using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeMenu : MonoBehaviour
{
    public static ChallengeMenu Instance;

    CanvasGroup canvasGroup;
    ChallengeDisplay[] challengeDisplays;
    GameObject caller;
    bool open = false;

    private void Awake()
    {
        Instance = this;

        canvasGroup = GetComponent<CanvasGroup>();
        challengeDisplays = GameObject.FindObjectsOfType<ChallengeDisplay>();
    }

    public void TransIn(GameObject _caller)
    {
        canvasGroup.alpha = 1;
        caller = _caller;
        caller.SetActive(false);
        SetupChallengeDisplays();
        open = true;
    }

    public void TransOut()
    {
        canvasGroup.alpha = 0;
        caller.SetActive(true);
        open = false;
    }

    public void SetupChallengeDisplays()
    {
        for (int i = 0; i < challengeDisplays.Length; ++i)
        {
            challengeDisplays[i].Setup();
        }
    }

    void Update()
    {
        if (open && Input.GetMouseButtonDown(0))
        {
            TransOut();
        }
    }
}
