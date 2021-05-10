using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour
{
    public bool canPause = true;

    private void OnMouseDown()
    {
        if (canPause && !MathHelper.IsPointerOverUIObject())
        {
            if (!GameController.Instance.IsPaused())
            {
                GameController.Instance.Pause();
                PauseMenu.Instance.Open();
            }
            else
            {
                if (!PauseMenu.Instance.gameObject.activeSelf)
                {
                    ChallengeMenu.Instance.TransOut();
                }
                PauseMenu.Instance.Unpause();
                Invoke("EnablePause", 0.5f);
                canPause = false;
            }
        }
    }

    

    private void EnablePause()
    {
        canPause = true;
    }
}
