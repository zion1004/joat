using UnityEngine;
using UnityEngine.UI;

public class InvemtoryDisplay : MonoBehaviour
{

    [Header("Inventory")]
    public GameObject inventoryUI;

    [Header("Inventory Blueprint")]
    public GameObject katanaUI;
    public GameObject demonicSwordUI;
    public GameObject slayerUI;
    public GameObject fangUI;
    public GameObject crescentBladeUI;


    [Header("Inventory Ore")]
    public GameObject waterOreUI;
    public GameObject fireOreUI;
    public GameObject poisonOreUI;
    public Text waterOreNumberUI;
    public Text fireOreNumberUI;
    public Text poisonOreNumberUI;

    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        inventoryUI.SetActive(false);
        UpdateInventory();
    }

    // Update is called once per frame
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            UpdateInventory();
            inventoryUI.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.Tab)) {
            inventoryUI.SetActive(false);
        }
    }

    private void UpdateInventory() {
        katanaUI.SetActive(true);
        demonicSwordUI.SetActive(gameManager.blueprintinventory[0] == 1);
        slayerUI.SetActive(gameManager.blueprintinventory[1] == 1);
        fangUI.SetActive(gameManager.blueprintinventory[2] == 1);
        crescentBladeUI.SetActive(gameManager.blueprintinventory[3] == 1);
        bool wo = gameManager.oreinventory[0] > 0, fo = gameManager.oreinventory[1] > 0, po = gameManager.oreinventory[2] > 0;
        waterOreUI.SetActive(wo);
        fireOreUI.SetActive(fo);
        poisonOreUI.SetActive(po);
        if(wo) {
            waterOreNumberUI.text = gameManager.oreinventory[0].ToString();
        }
        if(fo) {
            fireOreNumberUI.text =  gameManager.oreinventory[1].ToString();
        }
        if(po) {
            poisonOreNumberUI.text = gameManager.oreinventory[2].ToString();
        }
    }
}
