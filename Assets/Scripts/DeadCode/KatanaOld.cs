using UnityEngine;

public class KatanaOld : PlayerOldCode
{
    private int durability;
    private int attack;
    private Type type;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        

        PlayerControl();
    }
}
