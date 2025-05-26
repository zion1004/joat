using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI attack;
    [SerializeField] TextMeshProUGUI coin;

    [SerializeField] RawImage water;
    [SerializeField] RawImage fire;
    [SerializeField] RawImage poison;


    private void Update() {
        GameManager gm = GameManager.Instance;

        SetHealth(gm.durability, gm.maxDurability);
        SetAttack(gm.player.attack);
        SetCoin(gm.coins);
        SetElement(gm.type);
    }

    void SetHealth(int currHealth, int maxHealth) {
        health.text = currHealth.ToString();
        slider.value = (float)currHealth / (float)maxHealth;
    }

    void SetAttack(int val) {
        attack.text = val.ToString();
    }

    void SetCoin(int val){ 
        coin.text = val.ToString();
    }

    void SetElement(Player.Type type){
        if(type == Player.Type.Normal) {
            water.enabled = false;
            fire.enabled = false;
            poison.enabled = false;
        }
        else if(type == Player.Type.Water) {
            water.enabled = true;
            fire.enabled = false;
            poison.enabled = false;
        }
        else if(type == Player.Type.Fire) {
            water.enabled = false;
            fire.enabled = true;
            poison.enabled = false;
        }
        else if(type == Player.Type.Poison) {
            water.enabled = false;
            fire.enabled = false;
            poison.enabled = true;
        }
    }
}
