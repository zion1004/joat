using TMPro;
using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    GameManager gm;

    public GameObject endUI;
    public TextMeshProUGUI totalCoins;
    public TextMeshProUGUI totalOres;
    public TextMeshProUGUI totalBlueprints;
    public TextMeshProUGUI totalRepairs;
    public TextMeshProUGUI totalDestroyedObjects;

    private bool endSceneDone = false;
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.gameFinished)
        {
            return;
        }
        if (endSceneDone)
        {
            return;
        }
        endUI.SetActive(true);
        totalCoins.text = gm.totalcoins.ToString();
        totalOres.text = (gm.totaloreinventory[0] + gm.totaloreinventory[1] + gm.totaloreinventory[2]).ToString();
        totalBlueprints.text = (gm.blueprintinventory[0] + gm.blueprintinventory[1] + gm.blueprintinventory[2] + gm.blueprintinventory[3]).ToString();
        totalRepairs.text = gm.totalsuri.ToString();
        totalDestroyedObjects.text = gm.destroyedObjects.Count.ToString();
 

        endSceneDone = true;
    }
}
