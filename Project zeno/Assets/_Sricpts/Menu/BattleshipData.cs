using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BattleshipData class to store the data of the battleship
// This class is used to store the data of the battleship
[System.Serializable]
public class BattleshipData
{

    public float RocketNo;

    public float spread;

    public float bulletSpeed;

    public float fireRate;

    public float groundDrag;

    public float playerHeight;

    public bool grounded;

    public float speed;

    public float[] position;

    public int _currentShipIndex;

    public float _HEALTH;

    public float _SPEED;

    public float[] direction;


    public BattleshipData(BattleShip battleShip)
    {
        groundDrag = battleShip.groundDrag;
        playerHeight = battleShip.playerHeight;
        speed = battleShip.speed;
        position = new float[3];
        position[0] = battleShip.transform.position.x;
        position[1] = battleShip.transform.position.y;
        position[2] = battleShip.transform.position.z;
        direction = new float[3];
        Vector3 dir = battleShip.GetDirection();
        direction[0] = dir.x;
        direction[1] = dir.y;
        direction[2] = dir.z;
        _currentShipIndex = battleShip.getCurrentShipIndex();
        _HEALTH = battleShip.GetHealth();
        _SPEED = battleShip.GetSpeed();
        RocketNo = battleShip.rockets;
    }


}
