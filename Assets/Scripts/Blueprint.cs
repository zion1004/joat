using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public enum Weapon {
        DemonicSword,
        Slayer,
        Fang,
        CrescentBlade
    }

    [SerializeField] public Weapon weapon;
}
