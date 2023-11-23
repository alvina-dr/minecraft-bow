using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPCtrl : MonoBehaviour
{
    public static GPCtrl Instance;
    public UICtrl UICtrl;
    public PlayerBehavior player;
    public bool pause = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UICtrl.deathCounter.Increment(0);
    }

    public void Pause()
    {
        if (pause) //UNPAUSE
        {
            pause = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            UICtrl.pauseMenu.gameObject.SetActive(false);
        }
        else //PAUSE
        {
            pause = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            UICtrl.pauseMenu.gameObject.SetActive(true);
        }

    }
}
