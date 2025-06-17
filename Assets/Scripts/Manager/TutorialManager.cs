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
}
