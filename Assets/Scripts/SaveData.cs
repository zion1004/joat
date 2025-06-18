using System.Collections.Generic;

[System.Serializable]
public class SaveData {
    public bool hasCompletedTutorial;
    public int currentStage;
    public bool stage1entry;
    public bool stage2entry;
    public bool stage3entry;
    public bool stage4entry;
    public bool stage5entry;
    public int sanityCheck;
    public float posx;
    public float posy;
    public float posz;

    public float rotx;
    public float roty;
    public float rotz;

    public Player.Weapon weapon;
    public Player.Type type;
    public int durability;
    public int totalRepair;
    public int coins;
    public int totalcoins;
    public List<int> blueprintList = new List<int>();
    public List<int> oreList = new List<int>();
    public List<int> totalOreList = new List<int>(); 
    public List<string> collectedItems = new List<string>();
    public List<string> destroyedObjects = new List<string>();
}