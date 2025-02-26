using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleShip : MonoBehaviour
{
    public float groundDrag;

    public float playerHeight;

    [SerializeField]
    public LayerMask whatIsGround;
    bool grounded;

    public float speed; 

    public float boostSpeed; // save

    public float sensitivity = 5; // save

    public float lookSens;

    public GameObject[] rocketLaunchers;

    public GameObject[] rocketBase;

    public GameObject mainCamera;

    [SerializeField]
    public float rockets;

    public float reloadTime = 2f;

    [SerializeField]
    public float health = 100f; // save

    public BattleshipHealth battleshipHealth;

    public Animator animator;

    public GameObject bullet;

    public Transform firePoint;

    bool isReloading;

    float xRotation;

    float yRotation;

    Vector3 direction; // save

    Rigidbody rb;

    public bool shoot;
    float _SPEED; // save

    float _ROCKET; // save
    float _RELOADING;
    float _HEALTH;
    int _currentShipIndex; // save

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        battleshipHealth.SetHealth(health);
        animator = GetComponent<Animator>();
        
        _SPEED = speed;
        _ROCKET = rockets;
        _RELOADING = 0;
        _HEALTH = health;
        foreach (GameObject turret in rocketLaunchers)
        {
            turret.GetComponent<GunsVertical>().bulletSpeed = 100f;
            turret.GetComponent<GunsVertical>().bullet = bullet;
            turret.GetComponent<GunsVertical>().spread = 0.1f;
            turret.GetComponent<GunsVertical>().firePoint = firePoint;
        }
    }

    void FixedUpdate()
    {
        // Check if the player is grounded by casting a ray from the player's position straight down to the ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f, whatIsGround);

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        
        if (isReloading){
            _RELOADING += Time.deltaTime;
            if (_RELOADING >= reloadTime)
            {
                rockets = _ROCKET;
                isReloading = false;
                _RELOADING = 0;
            }
        }
    }

    public void SetSensitivity(float sens)
    {
        sensitivity = sens;
    }

    private IEnumerator Recoil()
    {
        animator.Play("Battleship Shooting"+_currentShipIndex);
        yield return new WaitForSeconds(0.12f);
        animator.Play("New State");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Always walking in the direction you're looking 
        
                    
        Vector2 directionValue = context.ReadValue<Vector2>();
        addForceShip(context.ReadValue<Vector2>());

        // limit velocity if needed
        // if (flatVel.magnitude > speed)
        // {
        //     Vector3 limitedVel = flatVel.normalized * speed;
        //     rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        // }
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        bool isBoosting = context.ReadValueAsButton();
        // increase speed if boosting
        if (isBoosting)
        {
            speed = boostSpeed;
        }
        else
        {
            speed = _SPEED;
        }
    }

    public void OnLook(InputAction.CallbackContext context, Quaternion newRotation)
    {
        
        float lookValueX = context.ReadValue<Vector2>().x;
        float lookValueY = context.ReadValue<Vector2>().y;
        xRotation += lookValueX * lookSens;   
        yRotation -= lookValueY * lookSens;
        Transform ori = mainCamera.transform;
        foreach (GameObject turret in rocketLaunchers)
        {
            turret.GetComponent<GunsVertical>().rotateGun(ori);
        }
        
        foreach (GameObject baseTurret in rocketBase)
        {
            baseTurret.GetComponent<GunLogic>().rotateGun(xRotation);
        }
        float easing = 0.05f; // Change this value to control the speed of rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, easing);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started && rockets > 0)
        {
            shoot = true;
            rockets--;
            // Instantiate(bullet, firePoint.position, Quaternion.LookRotation(direction));
            // StartCoroutine(Recoil());
            // bullet.GetComponent<Bullet>().SetEnemy("Enemy");
            rocketLaunchers[0].GetComponent<GunsVertical>().setBullet(bullet);
            rocketLaunchers[0].GetComponent<GunsVertical>().Fire();
        }
        else if (context.canceled)
        {
            StartCoroutine(Recoil());
            shoot = false;
            foreach (GameObject turret in rocketLaunchers)
            {
                turret.GetComponent<GunsVertical>().isFiring = false;
            }
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speed = speed / 2;
        }
        else if (context.canceled)
        {
            speed = _SPEED;
        }
    }



    public void OnReload(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isReloading = true;
        }
        if(context.canceled)
        {
            isReloading = false;
        }
        
        // if(rockets < _ROCKET&& context.started)
        // {
        //     rockets += 1f * (float)context.duration;
        // }
    }

    void addForceShip(Vector2 directionValue)
    {

        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        direction = (transform.forward * directionValue.y * sensitivity)+ (transform.right * directionValue.x * sensitivity);
        rb.AddForce(transform.forward *  speed * 20f, ForceMode.Force);
        // add force to the ship in the direction it's facing

        // change direction of the ship
        rb.velocity = new Vector3(direction.x, rb.velocity.y, direction.z);


        // limit velocity if needed
        if (rb.velocity.magnitude > speed)
        {
            Vector3 limitedVel = rb.velocity.normalized * speed*20f;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }else if (rb.velocity.magnitude < speed)
        {
            Vector3 limitedVel = rb.velocity.normalized * speed*20f;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        } 
        
    }


    void Update()
    {
        var controller = Gamepad.current;
        if(controller == null)
        {
            Debug.Log("No controller found");
            return;
        }
        //Debug.Log("Controller: " + controller.name);
    }

    public void SetCurrentship(int index)
    {
        _currentShipIndex = index;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        battleshipHealth.SetHealth(health);
        if (health <= 0)
        {
            //Destroy(gameObject);
        }
        Debug.Log("BattleShip Health: "+health);
    }

    // public BattleshipData GetBattleshipData()
    // {
    //     BattleshipData battleshipData = new BattleshipData();
    //     battleshipData.rotationSpeed = 2f;
    //     battleshipData.currentRotationX = 0f;
    //     battleshipData.currentRotationY = 0f;
    //     battleshipData.bullet = bullet;
    //     battleshipData.spread = 0.1f;
    //     battleshipData.bulletSpeed = 100f;
    //     battleshipData.isFiring = false;
    //     battleshipData.firePoint = firePoint;
    //     return battleshipData;
    // }
/// <summary>
/// Set the battleship data from the battleship data object in the save system
/// </summary>
/// <param name="battleshipData"></param>
    public void SetBattleshipData(BattleshipData battleshipData)
    {
        speed = battleshipData.speed;
        _SPEED = battleshipData.speed;
        rockets = battleshipData.RocketNo;
        Debug.Log("Rockets: "+rockets);
        health = battleshipData._HEALTH;
        _HEALTH = battleshipData._HEALTH;
        _currentShipIndex = battleshipData._currentShipIndex;
        direction = new Vector3(battleshipData.direction[0], battleshipData.direction[1], battleshipData.direction[2]);
        Vector3 position = new Vector3(battleshipData.position[0], battleshipData.position[1], battleshipData.position[2]);
        transform.position = position;
    }

    public void SaveBattleshipData()
    {
        SaveSystem.SaveBattleshipData(this);
    }

    public void LoadBattleshipData()
    {
        BattleshipData battleshipData = SaveSystem.LoadBattleshipData();
        SetBattleshipData(battleshipData);
    }

    public BattleShip GetShip()
    {
        BattleshipData battleshipData = SaveSystem.LoadBattleshipData();
        SetBattleshipData(battleshipData);
        return this;
    }

    public float GetHealth()
    {
        return (float)health;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }
    
    public int getCurrentShipIndex()
    {
        return _currentShipIndex;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
