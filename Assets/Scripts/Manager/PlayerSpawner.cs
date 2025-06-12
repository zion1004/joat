using HeneGames.DialogueSystem;
using UnityEngine;


public class PlayerSpawner : MonoBehaviour
{
    public Player player;
    public GameObject playercam;
    public Vector3 offset = new Vector3(0, 1.5f, 0);

    public bool isTutorial = false;

    void Start() {
        GameManager gm = GameManager.Instance;
        Vector3 spawnPoint = gm.returnPosition + offset;
        Quaternion spawnRot = gm.returnRotation;
        Player.Weapon selectedWeapon = gm.weapon;
        Player.Type selectedType = gm.type;
        if (gm.isLoadingGame)
        {
            spawnPoint = gm.playerPos;
            spawnRot = gm.playerRot;
            gm.isLoadingGame = false;
        }

        GameObject newPlayer = selectedWeapon == Player.Weapon.Katana ? gm.katana :
           selectedWeapon == Player.Weapon.DemonicSword && selectedType == Player.Type.Water ? gm.waterDemonicSword :
           selectedWeapon == Player.Weapon.DemonicSword && selectedType == Player.Type.Fire ? gm.waterDemonicSword :
           selectedWeapon == Player.Weapon.DemonicSword && selectedType == Player.Type.Poison ? gm.waterDemonicSword :
           selectedWeapon == Player.Weapon.Slayer && selectedType == Player.Type.Water ? gm.waterSlayer :
           selectedWeapon == Player.Weapon.Slayer && selectedType == Player.Type.Fire ? gm.fireSlayer :
           selectedWeapon == Player.Weapon.Slayer && selectedType == Player.Type.Poison ? gm.poisonSlayer :
           selectedWeapon == Player.Weapon.Fang && selectedType == Player.Type.Water ? gm.waterFang :
           selectedWeapon == Player.Weapon.Fang && selectedType == Player.Type.Fire ? gm.fireFang :
           selectedWeapon == Player.Weapon.Fang && selectedType == Player.Type.Poison ? gm.poisonFang :
           gm.crescentBlade;
        if (isTutorial)
        {
            newPlayer = gm.yeonsikDick;
        }

        GameObject instanciatedPlayer = Instantiate(newPlayer, spawnPoint, spawnRot);
        gm.player = instanciatedPlayer.GetComponent<Player>();
        gm.attack = gm.player.attack;
        gm.weapon = gm.player.weapon;
        gm.type = gm.player.type;
        gm.playerTransform = instanciatedPlayer.transform;
        playercam.transform.position = spawnPoint;
        
        MagmaFountain[] siba = (MagmaFountain[]) GameObject.FindObjectsByType(typeof(MagmaFountain), FindObjectsSortMode.None);

        foreach (var jott in siba)
        {
            foreach (var bozzi in instanciatedPlayer.GetComponent<Player>().blade)
                jott.magmaParticle.trigger.AddCollider(bozzi);

            foreach (var jaji in instanciatedPlayer.GetComponent<Player>().handle)
                jott.magmaParticle.trigger.AddCollider(jaji);
        }


    }
}
