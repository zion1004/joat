using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gauge : MonoBehaviour {
    [SerializeField] Image image;
    [SerializeField] Image bg;

    [SerializeField] Slider gauge;
    [SerializeField] Color red;
    [SerializeField] Color green;
    [SerializeField] Color white;

    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        player = GameManager.Instance.player;
        if(player == null) {
            player = GameManager.Instance.player;
            return;
        }
        if(player.isGrounded && player.CanJump()){ 
            image.color = green;
            bg.color = white;
        }
    }

    // Update is called once per frame
    void Update() {
        if(player == null){
            player = GameManager.Instance.player;
            return;
        }

        float min = player.GetChargeTime();
        float max = player.GetMaxChargeTime();
        float totalGauge = 0;
        if(player.GetIsCharging()) {
            totalGauge = min / max;
        }
        else {
            totalGauge = 0;
        }
        gauge.value = totalGauge;
        if(player.isGrounded && player.CanJump()){
            image.color = green;
            bg.color = white;
        }
        else{
            image.color = red;
            bg.color = red;
        }
    }
}
