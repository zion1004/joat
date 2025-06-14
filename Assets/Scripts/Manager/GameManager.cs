using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum BlacksmithPosition
    {
        Stage1, Stage2, Stage3, Stage4, Stage5, Tutorial
    };

    public bool[] tutorialComplete = { false, false, false, false };
    public static GameManager Instance;
    public GameObject mainCamera;
    public BlacksmithPosition currentBlacksmith;
    public int[] repairPrice = { 0, 10, 15, 20, 25, 0 };
    public HashSet<string> destroyedObjects = new HashSet<string>();

    public HashSet<string> collectedItems = new HashSet<string>();

    private string fileName = "D.joat";
    private FileDataHandler fdh;

    [SerializeField] public Player player;
    [SerializeField] public Transform playerTransform;
    [SerializeField] public Rigidbody playerRigidbody;

    [SerializeField] public int durability = 100;
    [SerializeField] public int maxDurability = 100;
    [SerializeField] public int coins = 0;
    [SerializeField] public int totalcoins = 0;
    [SerializeField] public int[] oreinventory = new int[3];
    [SerializeField] public int[] totaloreinventory = new int[3];
    [SerializeField] public int[] blueprintinventory = new int[4];
    [SerializeField] public int attack = 10;
    public int totalsuri = 0;
    public GameObject waterDemonicSword;
    public GameObject waterSlayer;
    public GameObject waterFang;

    public GameObject fireDemonicSword;
    public GameObject fireSlayer;
    public GameObject fireFang;

    public GameObject poisonDemonicSword;
    public GameObject poisonSlayer;
    public GameObject poisonFang;

    public GameObject katana;
    public GameObject crescentBlade;

    public GameObject yeonsikDick;

    public Vector3 returnPosition;
    public Quaternion returnRotation;
    public bool playerChanged = false;
    public Player.Weapon weapon;
    public Player.Type type;

    public Vector3 playerPos;
    public Quaternion playerRot;
    public Vector3 playerLinearVeloc;
    public Vector3 playerAngularVeloc;

    public bool isLoadingGame = false;
    public bool stage2entry = false;
    public bool stage3entry = false;
    public bool stage4entry = false;
    public bool stage5entry = false;


    public bool stage2reentry = false;
    public bool stage3reentry = false;
    public bool stage4reentry = false;
    public bool stage5reentry = false;

    public bool gameFinished = false;
    public bool gameOver = false;

    private void Start()
    {
        fdh = new FileDataHandler(Application.persistentDataPath, fileName);
    }
    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public bool IsCollected(string id) {
        return collectedItems.Contains(id);
    }

    public void MarkCollected(string id) {
        collectedItems.Add(id);
    }

    public bool IsDestroyed(string id) {
        return destroyedObjects.Contains(id);
    }

    public void MarkDestroyed(string id) {
        destroyedObjects.Add(id);
    }

    public void SaveGame() {
        SaveData data = new SaveData();
        data.sanityCheck = 1;
        data.posx = playerTransform.position.x;
        data.posy = playerTransform.position.y;
        data.posz = playerTransform.position.z;

        data.rotx = playerTransform.eulerAngles.x;
        data.roty = playerTransform.eulerAngles.y;
        data.rotz = playerTransform.eulerAngles.z;


        data.weapon = weapon;
        data.type = type;
        data.durability = durability;
        data.coins = coins;
        data.totalcoins = totalcoins;
        data.blueprintList = new List<int>(blueprintinventory);
        data.oreList = new List<int>(oreinventory);
        data.totalOreList = new List<int>(totaloreinventory);
        data.destroyedObjects = new List<string>(destroyedObjects);
        data.collectedItems = new List<string>(collectedItems);
        fdh.Save(data);
    }

    public bool HasSave()
    {
        SaveData data = fdh.Load();
        if (data == null)
        {
            return false;
        }
        if (data.sanityCheck == 0)
        {
            return false;
        }
        if (data.sanityCheck == 1)
        {
            return true;
        }
        return false;
    }

    public void LoadGame() {
        SaveData data = fdh.Load();
        if (data == null)
        {
            return;
        }
        if (data.sanityCheck == 0)
        {
            return;
        }
        data.sanityCheck = 0;
        fdh.Save(data);

        isLoadingGame = true;
        playerPos = new Vector3(data.posx, data.posy, data.posz);
        playerRot = Quaternion.Euler(data.rotx, data.roty, data.rotz);
        weapon = data.weapon;
        type = data.type;
        durability = data.durability;
        coins = data.coins;
        blueprintinventory = data.blueprintList.ToArray();
        oreinventory = data.oreList.ToArray();
        destroyedObjects = new HashSet<string>(data.destroyedObjects);
        collectedItems = new HashSet<string>(data.collectedItems);
    }

    public void ResetSave() {
        playerTransform = null;
        destroyedObjects.Clear();
        collectedItems.Clear();
        durability = maxDurability;
        coins = 0;
        totalcoins = 0;
        oreinventory = new int[3];
        totaloreinventory = new int[3];
        blueprintinventory = new int[4];
    }

    public void GameFinished()
    {
        gameFinished = true;
    }

    void OnApplicationQuit() {
        //Debug.Log("Quitting");
        //SaveGame();
    }
}
