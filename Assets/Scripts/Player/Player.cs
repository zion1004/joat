using UnityEngine;
using System.Runtime.CompilerServices;
using System.Linq;

public class Player : MonoBehaviour {

    public enum Type {
        Normal,
        Water,
        Fire,
        Poison
    }

    public enum Weapon {
        Katana,
        DemonicSword,
        Slayer,
        Fang,
        CrescentBlade
    }

    const int SLICETHRESHOLD = 10;

    [SerializeField] public Weapon weapon = Weapon.Katana;

    [SerializeField] public float maxChargeTime = 1f;
    [SerializeField] public float minJumpForce = 0.3f;
    [SerializeField] public float maxJumpForce = 0.8f;
    [SerializeField] public float minSpinForce = 4f;
    [SerializeField] public float maxSpinForce = 8f;
    [SerializeField] public float jumpSideForce = 4f;
    [SerializeField] public float jumpCooldown = 2f;

    [SerializeField] public float doubleJumpForce = 1f;
    [SerializeField] public float doubleJumpSpinSpeed = 4f;
    [SerializeField] public float doubleJumpSideForce = 6f;
    [SerializeField] public float maxAngularVelocity = 20f;

    [SerializeField] public Collider[] blade;
    [SerializeField] public Collider[] handle;

    [SerializeField] public float movementAngularRotationForce = 122f;
    [SerializeField] public float maxMovementAngularRotationForce = 1f;

    [SerializeField] public Vector3 playerInitialRotation = new Vector3(0f, 0f, 0f);
    [SerializeField] public Vector3 playerInitialScale = new Vector3(1f, 1f, 1f);
    [SerializeField] public Type type = Type.Normal;

    [SerializeField] public int attack = 10;

    [SerializeField] public float damageInterval = 1f;

    [SerializeField] public HUD hud;

    [SerializeField] float hitTimeCooldown = 1.5f;
    private float lastHitTime = -1f;

    public Rigidbody rb;
    public bool isGrounded = false;
    private bool canDoubleJump = false;

    private float chargeTime = 0f;
    private bool isCharging = false;
    private float groundToJumpThreshhold = 0.5f;
    private float lastJumpTime = -1f;

    private bool isNotMoving = false;

    private float rotationSpeed = 0f;
    private float linearSpeed = 0f;

    private float lastDamageTime = 0f;


    public bool GetIsNotMoving() {
        return isNotMoving;
    }

    public bool GetIsCharging() {
        return isCharging;
    }

    public float GetChargeTime() {
        return chargeTime;
    }

    public float GetMaxChargeTime() {
        return maxChargeTime;
    }

    void Start() {
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        rb.maxAngularVelocity = maxAngularVelocity;
        rb.centerOfMass = Vector3.zero;
        rb.inertiaTensorRotation = Quaternion.identity;
    }

    private void Update() {
        //if(rb.linearVelocity.sqrMagnitude > 1) {
        //    //smoothness of the slowdown is controlled by the 0.99f, 
        //    //0.5f is less smooth, 0.9999f is more smooth
        //    rb.linearVelocity *= 0.99f;
        //}

        HandleStatus();
        HandleInput();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void HandleStatus() {
        linearSpeed = rb.linearVelocity.magnitude;
        rotationSpeed = rb.angularVelocity.magnitude;
        isNotMoving = linearSpeed < 0f && rotationSpeed < 0f;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void HandleInput() {
        if(Input.GetKeyDown(KeyCode.R)) {
            rb.MovePosition(new Vector3(-5f, 3f, 0f));
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            HandleJump();
        }

        if(Input.GetKey(KeyCode.Space) && isCharging) {
            IsHoldingJump();
        }

        if(Input.GetKeyUp(KeyCode.Space) && isCharging) {
            HoldJump();
        }

        if(isGrounded) {
            bool canRotate = Mathf.Abs(rotationSpeed) < maxMovementAngularRotationForce;
            if(Input.GetKey(KeyCode.LeftArrow) && canRotate) {
                rb.AddTorque(movementAngularRotationForce * Vector3.forward, ForceMode.Force);
            }
            if(Input.GetKey(KeyCode.RightArrow) && canRotate) {
                rb.AddTorque(-movementAngularRotationForce * Vector3.forward, ForceMode.Force);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void HandleJump() {
        if(isGrounded) {
            HoldJumpStart();
        }
        else if(canDoubleJump) {
            DoubleJump();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CanJump() {
        return Time.time - lastJumpTime > jumpCooldown || isNotMoving;
;    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void HoldJumpStart() {
        if(CanJump()) {
            isCharging = true;
            chargeTime = 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void IsHoldingJump() {
        chargeTime += Time.deltaTime;
        chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void HoldJump() {
        float xDirection = 1f;
        float spinDirection = -1f;

        if(Input.GetKey(KeyCode.LeftArrow)) {
            xDirection = -1f;
            spinDirection = 1f;
        }
        else if(!Input.GetKey(KeyCode.RightArrow)) {
            spinDirection = (rb.angularVelocity.z != 0) ? Mathf.Sign(rb.angularVelocity.z) : 1f;
            xDirection = -spinDirection;
        }

        float totalJumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, chargeTime / maxChargeTime);
        float totalSpinForce = Mathf.Lerp(minSpinForce, maxSpinForce, chargeTime / maxChargeTime);
        rb.AddForce(new Vector3(xDirection * jumpSideForce, totalJumpForce, 0), ForceMode.Impulse);
        rb.AddTorque(Vector3.forward * spinDirection * totalSpinForce, ForceMode.Impulse);

        isCharging = false;
        isGrounded = false;
        canDoubleJump = true;
        lastJumpTime = Time.time;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void DoubleJump() {
        float xDirection = 0f;
        float spinDirection = Mathf.Sign(rb.angularVelocity.z);

        if(Input.GetKey(KeyCode.LeftArrow)) {
            xDirection = -1f;
            spinDirection = 1f;
        }
        else if(Input.GetKey(KeyCode.RightArrow)) {
            xDirection = 1f;
            spinDirection = -1f;
        }

        //rb.angularVelocity = Vector3.zero;
        rb.AddForce(new Vector3(xDirection * doubleJumpSideForce, doubleJumpForce, 0), ForceMode.Impulse);
        rb.AddTorque(Vector3.forward * spinDirection * doubleJumpSpinSpeed, ForceMode.Impulse);

        canDoubleJump = false;
    }

    private void OnTriggerEnter(Collider other) {

    }

    private void OnTriggerExit(Collider other) {

    }

    private void OnTriggerStay(Collider collision) {
        if(collision.gameObject.layer == 4) {
            if(type != Type.Water && GameManager.Instance.durability > 1) {
                if(Time.time >= lastDamageTime + damageInterval) {
                    GameManager.Instance.durability -= 1;
                    lastDamageTime = Time.time;
                }
            }
        }
        if(collision.gameObject.layer == 7) {
            GameManager.Instance.coins += 1;
            collision.enabled = false;
        }
        if(collision.gameObject.layer == 8){
            collision.enabled = false;
            Ore ore = collision.gameObject.GetComponent<Ore>();
            if(ore != null){
                Type type = ore.type;
                if(type == Type.Water){
                    GameManager.Instance.oreinventory[0] += 1;
                }
                else if(type == Type.Fire){
                    GameManager.Instance.oreinventory[1] += 1;
                }
                else if(type == Type.Poison){
                    GameManager.Instance.oreinventory[2] += 1;
                }
            }
        }
        if(collision.gameObject.layer == 9){
            collision.enabled = false;
            Blueprint blueprint = collision.gameObject.GetComponent<Blueprint>();
            if(blueprint != null) {
                Blueprint.Weapon weapon = blueprint.weapon;
                if(weapon == Blueprint.Weapon.DemonicSword) {
                    GameManager.Instance.blueprintinventory[0] = 1;
                }
                else if(weapon == Blueprint.Weapon.Slayer) {
                    GameManager.Instance.blueprintinventory[1] = 1;
                }
                else if(weapon == Blueprint.Weapon.Fang) {
                    GameManager.Instance.blueprintinventory[2] = 1;
                }
                else if(weapon == Blueprint.Weapon.CrescentBlade){
                    GameManager.Instance.blueprintinventory[3] = 1;
                }
            }
        }
    }

    //private void OnTriggerExit(Collider collision) {

    //}

    private void OnCollisionStay(Collision collision) {
        isGrounded = true;

        if(Time.time - lastJumpTime > groundToJumpThreshhold) {
            canDoubleJump = false;
        }
    }

    private void OnCollisionExit(Collision collision) {
        isGrounded = false;
    }


    private void OnCollisionEnter(Collision collision) {

        if(collision.gameObject.layer == 3) {
            if(collision.contacts.Length <= 0) {
                return;
            }

            Collider col = collision.GetContact(0).thisCollider;
            if(col == null) {
                return;
            }
            Vector3 hitDirection = collision.GetContact(0).normal;
            Vector3 pivotPoint = new Vector3(0, 0, hitDirection.x > 0 ? 1 : -1);
            // Mathf.Abs(rotationSpeed) >= SLICETHRESHOLD || Mathf.Abs(linearSpeed) >= 1) &&
            if((blade.Contains(col))) {
                //Physics.IgnoreCollision(collision.collider, blade);
                //Physics.IgnoreCollision(collision.collider, handle);
                Destroyable destroyable = collision.gameObject.GetComponent<Destroyable>();
                if(destroyable != null) {
                    if(Time.time - lastHitTime > hitTimeCooldown) {
                        lastHitTime = Time.time;
                        GameManager.Instance.durability -= 1;
                        destroyable.GetSliced(attack, pivotPoint);
                    }
                }
            }
        }
    }
}
