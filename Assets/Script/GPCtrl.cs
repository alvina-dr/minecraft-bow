using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GPCtrl : MonoBehaviour
{
    public static GPCtrl Instance;
    [Header("REFERENCES")]
    public UICtrl UICtrl;
    public PlayerBehavior player;
    [Header("PREFABS")]
    public Drop dropPrefab;
    [HideInInspector] public bool pause = false;

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

    public void GameOver()
    {
        pause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UICtrl.gameOverMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
