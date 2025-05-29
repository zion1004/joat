using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public HashSet<string> destroyedObjects = new HashSet<string>();

    public HashSet<string> collectedItems = new HashSet<string>();

    private string saveKey = "swordrunsavefileasdfsdfaf";

    [SerializeField] public Player player;
    [SerializeField] public Transform playerTransform;
    [SerializeField] public Rigidbody playerRigidbody;

    [SerializeField] public int durability = 100;
    [SerializeField] public int maxDurability = 100;
    [SerializeField] public int coins = 0;
    [SerializeField] public int[] oreinventory = new int[3];
    [SerializeField] public int[] blueprintinventory = new int[4];
    [SerializeField] public int attack = 10;


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
    public bool playerChanged = false;
    public Player.Weapon weapon;
    public Player.Type type;

    public Vector3 playerPos;
    public Quaternion playerRot;
    public Vector3 playerLinearVeloc;
    public Vector3 playerAngularVeloc;

    public bool isLoadingGame = false;

    public bool stage2reentry = false;
    public bool stage3reentry = false;
    public bool stage4reentry = false;
    public bool stage5reentry = false;


    private void Start()
    {

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
        data.posx = playerTransform.position.x;
        data.posy = playerTransform.position.y;
        data.posz = playerTransform.position.z;

        data.rotx = playerTransform.eulerAngles.x;
        data.roty = playerTransform.eulerAngles.y;
        data.rotz = playerTransform.eulerAngles.z;


        data.weapon = weapon;
        data.type = type;
        data.coins = coins;
        data.blueprintList = new List<int>(blueprintinventory);
        data.oreList = new List<int>(oreinventory);
        data.destroyedObjects = new List<string>(destroyedObjects);
        data.collectedItems = new List<string>(collectedItems);
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(saveKey, json);
        PlayerPrefs.Save();
    }

    public bool HasSave() {
        return PlayerPrefs.HasKey(saveKey);
    }

    public void LoadGame() {
        if(PlayerPrefs.HasKey(saveKey)) {
            isLoadingGame = true;
            string json = PlayerPrefs.GetString(saveKey);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerPos = new Vector3(data.posx, data.posy, data.posz);
            playerRot = Quaternion.Euler(data.rotx, data.roty, data.rotz);



            weapon = data.weapon;
            type = data.type;
            coins = data.coins;
            blueprintinventory = data.blueprintList.ToArray();
            oreinventory = data.oreList.ToArray();
            destroyedObjects = new HashSet<string>(data.destroyedObjects);
            collectedItems = new HashSet<string>(data.collectedItems);
        }
    }

    public void ResetSave() {
        PlayerPrefs.DeleteKey(saveKey);
        playerTransform = null;
        destroyedObjects.Clear();
        collectedItems.Clear();
        durability = 100;
        maxDurability = 100;
        coins = 0;
        oreinventory = new int[3];
        blueprintinventory = new int[4];
    }

    void OnApplicationQuit() {
        //Debug.Log("Quitting");
        //SaveGame();
    }
}
