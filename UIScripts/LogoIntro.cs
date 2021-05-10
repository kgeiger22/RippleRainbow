using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoIntro : MonoBehaviour
{
    public static LogoIntro Instance;

    private Animator animator;
    
    void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void PlayIntro()
    {
        animator.Play("LogoIntro");
    }

    public void PlayOutro()
    {
        animator.Play("LogoOutro");
    }
}
