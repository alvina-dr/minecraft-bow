using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{
    public UI_Inventory inventoryBar;
    public UI_HealthBar healthBar;
    public UI_Counter deathCounter;
    public UI_Counter chronoWavePause;
    public UI_PauseMenu pauseMenu;
    public GameObject gameOverMenu;
    public Slider sensitivitySlider;

    private void Start()
    {
        sensitivitySlider.value = GPCtrl.Instance.player.cameraSpeed;
    }

    public void SetSensitivity(float _value)
    {
        GPCtrl.Instance.player.cameraSpeed = _value;
    }
}
