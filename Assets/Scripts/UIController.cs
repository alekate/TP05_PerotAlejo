using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PointsSystem pointsSystem;
    [SerializeField] private PlayerHealth playerHealth;

    [Header("GameUI")]
    [SerializeField] private TextMeshProUGUI pointUI;
    [SerializeField] private Image currentHealthBar;

    [Header("AudioSetting")]
    [SerializeField] private AudioMixer myMixer;

    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider MenuSlider;

    [Header("UI")]
    private bool isPaused = false;
    public GameObject pauseUI;
    public GameObject gameUI;

    private void Start()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetMenuVolume();
    }
    public void UpdateHealthBar()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 4;
    }
    public void UpdatePoints()
    {
        pointUI.text = pointsSystem.currentPoints.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                pauseUI.SetActive(true);
                isPaused = true;
                gameUI.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;
                pauseUI.SetActive(false);
                isPaused = false;
                gameUI.SetActive(true);

            }
        }
    }
    public void SetMasterVolume()
    {
        float volume = MasterSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume()
    {
        float volume = MusicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
    public void SetMenuVolume()
    {
        float volume = MenuSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

}
