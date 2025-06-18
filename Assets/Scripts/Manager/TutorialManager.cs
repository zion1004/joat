using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject endTutorialDialogue;
    public GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameManager.Instance;
    }
    public void TutorialFinished()
    {
        gm.hasCompletedTutorial = true;
        gm.player.StopMove();
        endTutorialDialogue.SetActive(true);
    }

    public void MoveToGame()
    {
        gm.returnPosition = new Vector3(-5f, 3f, 0f);
        gm.MoveToMainGame();
    }
}
