using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Michael;

    [Header("Main Menu")]
    public GameObject mainMenuBar;
    public Button startGameButton;
    public Button loadGameButton;
    public Button settingsButton;
    public Button exitGameButton;

    [Header("Settings")]
    public GameObject settingsMenu;
    public TextMeshProUGUI headerText;
    public GameObject settingsSubMenu;
    public GameObject settingsSubBackButton;

    [Header("Graphics")]
    public GameObject graphicsSettingMenu;

    [Header("Audio")]
    public GameObject audioSettingsMenu;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        if(gameManager.HasSave()) {
            loadGameButton.interactable = true;
        }
        else
        {
            loadGameButton.interactable = false;
        }
        settingsMenu.SetActive(false);
        graphicsSettingMenu.SetActive(false);
        audioSettingsMenu.SetActive(false);
    }

    public void StartGame() {
        SceneManager.LoadScene("mapload");
    }

    public void LoadGame() {
        gameManager.LoadGame();
        SceneManager.LoadScene("mapload");
    }

    public void Settings() {
        mainMenuBar.SetActive(false);
        settingsMenu.SetActive(true);
        headerText.text = "Settings";
        settingsSubMenu.SetActive(true);
        settingsSubBackButton.SetActive(false);
    }

    public void BackFromSettings() {
        mainMenuBar.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void BackFromSettingSubMenu() {
        headerText.text = "Settings";
        settingsSubMenu.SetActive(true);
        settingsSubBackButton.SetActive(false);
        graphicsSettingMenu.SetActive(false);
        audioSettingsMenu.SetActive(false);
    }

    public void GraphicMenu() {
        headerText.text = "Graphics";
        settingsSubMenu.SetActive(false);
        settingsSubBackButton.SetActive(true);
        graphicsSettingMenu.SetActive(true);
    }

    public void AudioMenu() {
        headerText.text = "Audio";
        settingsSubMenu.SetActive(false);
        settingsSubBackButton.SetActive(true);
        audioSettingsMenu.SetActive(true);
    }

    public void ExitGame(){
    
    }
}
