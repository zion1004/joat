using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject menu;

    [Header("Main Menu")]
    public GameObject pauseMenu;


    [Header("Settings")]
    public GameObject settingsMenu;
    public TextMeshProUGUI headerText;
    public GameObject settingsSubMenu;
    public GameObject settingsSubBackButton;

    [Header("Graphics")]
    public GameObject graphicsSettingMenu;

    [Header("Audio")]
    public GameObject audioSettingsMenu;

    [Header("Reset")]
    public GameObject resetMenu;

    [Header("Exit")]
    public GameObject exitMenu;

    private GameManager gameManager;

    private bool pauseMenuActive = false;

    void Start()
    {
        gameManager = GameManager.Instance;
        menu.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        graphicsSettingMenu.SetActive(false);
        audioSettingsMenu.SetActive(false);
    }

    public void PauseMenu()
    {
        if (!pauseMenuActive)
        {
            pauseMenuActive = true;
            menu.SetActive(true);
            pauseMenu.SetActive(true);
            settingsSubBackButton.SetActive(false);
            settingsMenu.SetActive(false);
            graphicsSettingMenu.SetActive(false);
            audioSettingsMenu.SetActive(false);
            resetMenu.SetActive(false);
            exitMenu.SetActive(false);
        }
        else
        {
            pauseMenuActive = false;
            Resume();
        }
    }

    public void Resume() {
        menu.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        resetMenu.SetActive(false);
        exitMenu.SetActive(false);
    }

    public void Settings() {
        pauseMenu.SetActive(false);
        resetMenu.SetActive(false);
        exitMenu.SetActive(false);

        settingsMenu.SetActive(true);
        headerText.text = "Settings";
        settingsSubMenu.SetActive(true);
        settingsSubBackButton.SetActive(false);
    }

    public void BackFromSettings() {
        pauseMenu.SetActive(true);
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

    public void ResetMenu() {
        pauseMenu.SetActive(false);
        resetMenu.SetActive(true);
        exitMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void ResetGame() {
        gameManager.ResetSave();
        gameManager.returnPosition = new Vector3(0f, 0f, 0f);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void ExitMenu() {
        pauseMenu.SetActive(false);
        resetMenu.SetActive(false);
        exitMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void ExitToMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        gameManager.SaveGame();
        SceneManager.LoadScene("Main");
    }

}
