using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleShipTemplate 
{
    public string type;
    public float groundDrag;
    public float playerHeight;

    public LayerMask whatIsGround;
    public bool grounded;
    public float speed;

    public float boostSpeed;

    public Transform ori;

    public float sensX;

    public float sensY;

    public float lookSens;

    public GameObject[] rocketLaunchers;

    public GameObject[] rocketBase;

    public GameObject mainCamera;

    public float rockets;

    public float reloadTime;

    public float health;

    public bool isReloading;

    public float xDirection;

    public float yDirection;

    public float xRotation;

    public float yRotation;

    public Vector3 direction;

    public Rigidbody rb;

    public bool shoot;

    public Vector2 currentMovementInput;

    public bool isBoosting;

    public bool isMoving;

    public float _SPEED;

    
}
