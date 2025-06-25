using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    GameManager gm;
    AudioManager am;

    public GameObject hitbox;
    public GameObject cutsenes;
    public GameObject scene1;


    public GameObject scene2;

    public GameObject scene3;

    private int sceneint = 0;

    void Start()
    {
        gm = GameManager.Instance;
        am = AudioManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            if (!gm.hasCompletedCutscene)
            { 
                gm.player.StopMove();
                cutsenes.SetActive(true);                
            }
        }
    }

    public void NextScene()
    {
        if (sceneint == 0)
        {
            ShowScene1();
        }
        else if (sceneint == 1)
        {
            ShowScene2();
        }
        else if (sceneint == 2)
        {
            ShowScene3();
        }
        else
        {
            EndScene();
        }
        sceneint += 1;
    }

    public void ShowScene1()
    {
        scene1.SetActive(true);
        scene2.SetActive(false);
        scene3.SetActive(false);
    }

    public void ShowScene2()
    {
        scene1.SetActive(false);
        scene2.SetActive(true);
        scene3.SetActive(false);
    }

    public void ShowScene3()
    {
        scene1.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(true);
    }

    public void EndScene()
    {
        gm.hasCompletedCutscene = true;
        hitbox.SetActive(false);
        scene1.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(false);
        cutsenes.SetActive(false);
        gm.player.StartMove();
        am.PlayVoice(am.stage1);
    }
    
}
