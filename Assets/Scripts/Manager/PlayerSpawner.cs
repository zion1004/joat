using UnityEngine;
using UnityEngine.UIElements;


public class PlayerSpawner : MonoBehaviour
{
    public Player player;
    public GameObject playercam;
    public Vector3 offset = new Vector3(0, 10, 0);


    void Start() {
        GameManager gm = GameManager.Instance;
        Vector3 spawnPoint = gm.returnPosition + offset;
        Player.Weapon selectedWeapon = gm.weapon;
        Player.Type selectedType = gm.type;

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


        GameObject instanciatedPlayer = Instantiate(newPlayer, spawnPoint, Quaternion.identity);
        gm.player = instanciatedPlayer.GetComponent<Player>();

        gm.playerTransform = instanciatedPlayer.transform;
        playercam.transform.position = spawnPoint;
        
        if(gm.isLoadingGame) {
            instanciatedPlayer.transform.SetPositionAndRotation(gm.playerPos, gm.playerRot);
            playercam.transform.position = gm.playerTransform.position;
            gm.isLoadingGame = false;
        }
    }
}
