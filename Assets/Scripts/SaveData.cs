using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {
    public float posx;
    public float posy;
    public float posz;

    public float rotx;
    public float roty;
    public float rotz;

    public Player.Weapon weapon;
    public Player.Type type;
    public int coins;
    public List<int> blueprintList = new List<int>();
    public List<int> oreList = new List<int>();
    public List<string> collectedItems = new List<string>();
    public List<string> destroyedObjects = new List<string>();
}