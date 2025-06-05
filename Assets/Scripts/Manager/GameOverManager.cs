using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    GameManager gm;

    public GameObject gameoverUI;

    private bool gameoverSceneEnd = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameoverSceneEnd)
        {
            return;
        }
        if (!gm.gameOver)
        {
            return;
        }
        gameoverUI.SetActive(true);
        gameoverSceneEnd = true;
    }
}
