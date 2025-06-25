using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blacksmith : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu;

    public GameObject repairMenu;
    public GameObject repairSubmenu;

    public GameObject changeWeaponMenu;
    public GameObject changeWeaponSubmenu;


    [Header("Repair")]
    public TextMeshProUGUI maxDurability;
    public TextMeshProUGUI coinNeeded;
    public ActualTooltip insufficientFundTooltip;
    public Button repairButton;
    public int repairCost = 10;

    [Header("Change Weapon")]
    public GameObject oreSection;
    public TextMeshProUGUI oreNeeded;
    public ActualTooltip insufficientOreTooltip;
    public TextMeshProUGUI oreErrorTooltipText;
    public Button forgeButton;
    public Color toggleOffColor;
    public Color toggleOnColor;
    public int katanaOreCost = 1;
    public int demonicSwordOreCost = 3;
    public int slayerOreCost = 7;
    public int fangOreCost = 5;
    public int crescentBladeOreCost = 7;

    [Header("Change Weapon Weapon Game Objects")]
    public GameObject baseDemonicSword;
    public GameObject baseFang;
    public GameObject baseSlayer;
    public GameObject waterDemonicSword;
    public GameObject waterFang;
    public GameObject waterSlayer;
    public GameObject fireDemonicSword;
    public GameObject fireFang;
    public GameObject fireSlayer;
    public GameObject poisonDemonicSword;
    public GameObject poisonFang;
    public GameObject poisonSlayer;
    public GameObject baseKatana;
    public GameObject baseCrescentBlade;

    [Header("Change Weapon Blueprint")]
    public GameObject blueprintView;
    public GameObject blueprintList;
    public GameObject noBlueprintMessage;
    public ToggleGroup blueprintToggleGroup;
    public GameObject katana;
    public GameObject demonicSword;
    public GameObject slayer;
    public GameObject fang;
    public GameObject crescentBlade;
    public Text katanaText;
    public Text demonicSwordText;
    public Text slayerText;
    public Text fangText;
    public Text crescentBladeText;
    public Toggle katanaToggle;
    public Toggle demonicSwordToggle;
    public Toggle slayerToggle;
    public Toggle fangToggle;
    public Toggle crescentBladeToggle;

    [Header("Change Weapon Ore")]
    public GameObject oreView;
    public GameObject oreList;
    public GameObject noOreMessage;
    public GameObject insufficientOreMessage;

    public ToggleGroup oreToggleGroup;
    public GameObject waterOre;
    public GameObject fireOre;
    public GameObject poisonOre;
    public Text waterOreLabel;
    public Text fireOreLabel;
    public Text poisonOreLabel;
    public Text waterOreNumber;
    public Text fireOreNumber;
    public Text poisonOreNumber;
    public Toggle waterOreToggle;
    public Toggle fireOreToggle;
    public Toggle poisonOreToggle;

    public string insufficientOreText = "Insufficient ore for forging";
    public string sameWeaponText = "Cannot forge into same weapon";

    private int oreAmountCost;
    private Player.Weapon selectedWeapon;
    private Player.Type selectedType;


    public void Start()
    {
        GameManager gm = GameManager.Instance;
        repairCost = gm.repairPrice[(int)gm.currentBlacksmith];
        mainMenu.SetActive(true);
        repairMenu.SetActive(false);
        repairSubmenu.SetActive(false);
        changeWeaponMenu.SetActive(false);
        changeWeaponSubmenu.SetActive(false);
        insufficientOreMessage.SetActive(false);
    }


    public void OpenRepairMenu()
    {
        mainMenu.SetActive(false);
        repairMenu.SetActive(true);
        repairSubmenu.SetActive(true);
        UpdateRepairMenu();
    }

    public void Repair()
    {
        if (GameManager.Instance.coins < repairCost)
        {
            return;
        }
        AudioManager.Instance.PlaySound(AudioManager.Instance.anvil);
        GameManager.Instance.coins -= repairCost;
        GameManager.Instance.durability = GameManager.Instance.maxDurability;
        GameManager.Instance.totalsuri += 1;
        UpdateRepairMenu();
    }

    public void UpdateRepairMenu()
    {
        maxDurability.text = GameManager.Instance.maxDurability.ToString();
        coinNeeded.text = repairCost.ToString();

        if (GameManager.Instance.coins < repairCost)
        {
            insufficientFundTooltip.isActive = true;
            repairButton.interactable = false;
        }
        else
        {
            insufficientFundTooltip.isActive = false;
            repairButton.interactable = true;
        }
    }


    public void OpenChangeWeaponMenu()
    {
        mainMenu.SetActive(false);
        changeWeaponMenu.SetActive(true);
        changeWeaponSubmenu.SetActive(true);
        oreView.SetActive(false);
        forgeButton.gameObject.SetActive(false);

        noBlueprintMessage.SetActive(false);
        int[] blueprints = GameManager.Instance.blueprintinventory;

        katana.SetActive(true);
        demonicSword.SetActive(blueprints[0] == 1);
        slayer.SetActive(blueprints[1] == 1);
        fang.SetActive(blueprints[2] == 1);
        crescentBlade.SetActive(blueprints[3] == 1);
    }

    public void ShowWeaponModel(int i)
    {
        baseDemonicSword.SetActive(i == 1);
        baseFang.SetActive(i == 2);
        baseSlayer.SetActive(i == 3);
        waterDemonicSword.SetActive(i == 4);
        waterFang.SetActive(i == 5);
        waterSlayer.SetActive(i == 6);
        fireDemonicSword.SetActive(i == 7);
        fireFang.SetActive(i == 8);
        fireSlayer.SetActive(i == 9);
        poisonDemonicSword.SetActive(i == 10);
        poisonFang.SetActive(i == 11);
        poisonSlayer.SetActive(i == 12);
        baseKatana.SetActive(i == 13);
        baseCrescentBlade.SetActive(i == 14);
    }

    public void SelectBlueprint()
    {
        oreToggleGroup.SetAllTogglesOff(true);

        katanaText.color = toggleOffColor;
        demonicSwordText.color = toggleOffColor;
        slayerText.color = toggleOffColor;
        fangText.color = toggleOffColor;
        crescentBladeText.color = toggleOffColor;


        if (katanaToggle.isOn)
        {
            katanaText.color = toggleOnColor;
            oreAmountCost = katanaOreCost;
            selectedWeapon = Player.Weapon.Katana;
            ShowOreList();
            ShowWeaponModel(13);
        }
        else if (demonicSwordToggle.isOn)
        {
            demonicSwordText.color = toggleOnColor;
            oreAmountCost = demonicSwordOreCost;
            selectedWeapon = Player.Weapon.DemonicSword;
            ShowOreList();
            ShowWeaponModel(1);
        }
        else if (slayerToggle.isOn)
        {
            slayerText.color = toggleOnColor;
            oreAmountCost = slayerOreCost;
            selectedWeapon = Player.Weapon.Slayer;
            ShowOreList();
            ShowWeaponModel(3);
        }
        else if (fangToggle.isOn)
        {
            fangText.color = toggleOnColor;
            oreAmountCost = fangOreCost;
            selectedWeapon = Player.Weapon.Fang;
            ShowOreList();
            ShowWeaponModel(2);
        }
        else if (crescentBladeToggle.isOn)
        {
            crescentBladeText.color = toggleOnColor;
            oreAmountCost = crescentBladeOreCost;
            selectedWeapon = Player.Weapon.CrescentBlade;
            ShowOreList();
            ShowWeaponModel(14);
        }
        else
        {
            oreView.SetActive(false);
            ShowWeaponModel(0);
        }
    }

    public void ShowOreList()
    {
        oreNeeded.text = oreAmountCost.ToString();

        int[] ores = GameManager.Instance.oreinventory;
        int waterOreAmount = ores[0];
        int fireOreAmount = ores[1];
        int poisonOreAmount = ores[2];
        if (waterOreAmount > 0)
        {
            waterOre.SetActive(true);
            waterOreNumber.text = waterOreAmount.ToString();
        }
        else
        {
            waterOre.SetActive(false);
        }
        if (fireOreAmount > 0)
        {
            fireOre.SetActive(true);
            fireOreNumber.text = fireOreAmount.ToString();
        }
        else
        {
            fireOre.SetActive(false);
        }
        if (poisonOreAmount > 0)
        {
            poisonOre.SetActive(true);
            poisonOreNumber.text = poisonOreAmount.ToString();
        }
        else
        {
            poisonOre.SetActive(false);
        }

        noOreMessage.SetActive(waterOreAmount <= 0 && fireOreAmount <= 0 && poisonOreAmount <= 0);
        oreView.SetActive(true);
    }

    public void SelectOre()
    {
        waterOreLabel.color = toggleOffColor;
        waterOreNumber.color = toggleOffColor;
        fireOreLabel.color = toggleOffColor;
        fireOreNumber.color = toggleOffColor;
        poisonOreLabel.color = toggleOffColor;
        poisonOreNumber.color = toggleOffColor;

        if (waterOreToggle.isOn)
        {
            waterOreLabel.color = toggleOnColor;
            waterOreNumber.color = toggleOnColor;
            selectedType = (selectedWeapon != Player.Weapon.Katana && selectedWeapon != Player.Weapon.CrescentBlade) ? Player.Type.Water : Player.Type.Normal;
            forgeButton.gameObject.SetActive(true);
            if (GameManager.Instance.oreinventory[0] < oreAmountCost)
            {
                insufficientOreTooltip.isActive = true;
                forgeButton.interactable = false;
                oreErrorTooltipText.text = insufficientOreText;
            }
            else if ((selectedWeapon == Player.Weapon.Katana || selectedWeapon == Player.Weapon.CrescentBlade) && GameManager.Instance.weapon == selectedWeapon)
            {
                insufficientOreTooltip.isActive = true;
                forgeButton.interactable = false;
                oreErrorTooltipText.text = sameWeaponText;
            }
            else if (GameManager.Instance.weapon == selectedWeapon && GameManager.Instance.type == Player.Type.Water)
            {
                insufficientOreTooltip.isActive = true;
                forgeButton.interactable = false;
                oreErrorTooltipText.text = sameWeaponText;
            }
            else
            {
                insufficientOreTooltip.isActive = false;
                forgeButton.interactable = true;
                int i = selectedWeapon == Player.Weapon.DemonicSword ? 4 :
                        selectedWeapon == Player.Weapon.Fang ? 5 :
                        selectedWeapon == Player.Weapon.Slayer ? 6 : 0;
                if (i != 0)
                {
                    ShowWeaponModel(i);
                }
            }
        }
        else if (fireOreToggle.isOn)
        {
            fireOreLabel.color = toggleOnColor;
            fireOreNumber.color = toggleOnColor;
            selectedType = (selectedWeapon != Player.Weapon.Katana && selectedWeapon != Player.Weapon.CrescentBlade) ? Player.Type.Fire : Player.Type.Normal;
            forgeButton.gameObject.SetActive(true);
            if (GameManager.Instance.oreinventory[1] < oreAmountCost)
            {
                insufficientOreTooltip.isActive = true;
                forgeButton.interactable = false;
                oreErrorTooltipText.text = insufficientOreText;
            }
            else if ((selectedWeapon == Player.Weapon.Katana || selectedWeapon == Player.Weapon.CrescentBlade) && GameManager.Instance.weapon == selectedWeapon)
            {
                insufficientOreTooltip.isActive = true;
                forgeButton.interactable = false;
                oreErrorTooltipText.text = sameWeaponText;
            }
            else if (GameManager.Instance.weapon == selectedWeapon && GameManager.Instance.type == Player.Type.Fire)
            {
                insufficientOreTooltip.isActive = true;
                forgeButton.interactable = false;
                oreErrorTooltipText.text = sameWeaponText;
            }
            else
            {
                insufficientOreTooltip.isActive = false;
                forgeButton.interactable = true;
                int i = selectedWeapon == Player.Weapon.DemonicSword ? 7 :
                        selectedWeapon == Player.Weapon.Fang ? 8 :
                        selectedWeapon == Player.Weapon.Slayer ? 9 : 0;
                if (i != 0)
                {
                    ShowWeaponModel(i);
                }
            }
        }
        else if (poisonOreToggle.isOn)
        {
            poisonOreLabel.color = toggleOnColor;
            poisonOreNumber.color = toggleOnColor;
            selectedType = (selectedWeapon != Player.Weapon.Katana && selectedWeapon != Player.Weapon.CrescentBlade) ? Player.Type.Poison : Player.Type.Normal;
            forgeButton.gameObject.SetActive(true);
            if (GameManager.Instance.oreinventory[2] < oreAmountCost)
            {
                insufficientOreTooltip.isActive = true;
                forgeButton.interactable = false;
                oreErrorTooltipText.text = insufficientOreText;
            }
            else if ((selectedWeapon == Player.Weapon.Katana || selectedWeapon == Player.Weapon.CrescentBlade) && GameManager.Instance.weapon == selectedWeapon)
            {
                insufficientOreTooltip.isActive = true;
                forgeButton.interactable = false;
                oreErrorTooltipText.text = sameWeaponText;
            }
            else if (GameManager.Instance.weapon == selectedWeapon && GameManager.Instance.player.type == Player.Type.Poison)
            {
                insufficientOreTooltip.isActive = true;
                forgeButton.interactable = false;
                oreErrorTooltipText.text = sameWeaponText;
            }
            else
            {
                insufficientOreTooltip.isActive = false;
                forgeButton.interactable = true;
                int i = selectedWeapon == Player.Weapon.DemonicSword ? 10 :
                        selectedWeapon == Player.Weapon.Fang ? 11 :
                        selectedWeapon == Player.Weapon.Slayer ? 12 : 0;
                if (i != 0)
                {
                    ShowWeaponModel(i);
                }
            }
        }
        else
        {
            forgeButton.gameObject.SetActive(false);
            ShowWeaponModel(0);
        }
    }

    public void Forge()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.forge);
        GameManager.Instance.playerChanged = true;
        GameManager.Instance.weapon = selectedWeapon;
        int orecost = selectedWeapon == Player.Weapon.Katana ? 1 :
                      selectedWeapon == Player.Weapon.DemonicSword ? 3 :
                      selectedWeapon == Player.Weapon.Fang ? 5 :
                      selectedWeapon == Player.Weapon.Slayer ? 7 :
                      selectedWeapon == Player.Weapon.CrescentBlade ? 7 : 0;

        GameManager.Instance.type = selectedType;
        if (selectedType == Player.Type.Water)
        {
            GameManager.Instance.oreinventory[0] -= orecost;
        }
        else if (selectedType == Player.Type.Water)
        {
            GameManager.Instance.oreinventory[1] -= orecost;
        }
        else if (selectedType == Player.Type.Poison)
        {
            GameManager.Instance.oreinventory[2] -= orecost;
        }
    }

    public void BackToMainMenu()
    {
        repairMenu.SetActive(false);
        repairSubmenu.SetActive(false);
        changeWeaponMenu.SetActive(false);
        changeWeaponSubmenu.SetActive(false);
        mainMenu.SetActive(true);
        ShowWeaponModel(0);
    }

    public void ExitRoom()
    {
        AudioManager.Instance.MusicSource.Stop();
        AudioManager.Instance.MusicSource.volume = 1f;
        SceneManager.LoadScene("mapload");
    }
}
