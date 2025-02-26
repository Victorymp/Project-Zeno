using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleShipMovement : MonoBehaviour
{
    BattleShipInputs battleShipInput;

    Vector2 currentMovementInput;
    bool isBoosting;
    bool isMoving;

    Rigidbody rb;
    void awake()
    {
        battleShipInput = new BattleShipInputs();

        battleShipInput.BattleshipMoving.Move.performed += ctx => 
        {
            currentMovementInput = ctx.ReadValue<Vector2>();
            isMoving = currentMovementInput != Vector2.zero;
        };
        battleShipInput.BattleshipMoving.Boost.performed += ctx => isBoosting = ctx.ReadValueAsButton();
    }
    void FixedUpdate()
    {
        handleMovement();
    }

    void OnEnable()
    {
        battleShipInput.Enable();
    }

    void OnDisable()
    {
        battleShipInput.Disable();
    }

    void handleMovement()
    {
        if(isMoving || !isBoosting)
        {
            moveShip();
        }
        if(!isMoving && isBoosting)
        {
            boostShip();
        }
        if(!isMoving && !isBoosting)
        {
            stopShip();
        }
        
        Vector3 direction = new Vector3(currentMovementInput.x, 0, currentMovementInput.y);
        rb.AddForce(direction.normalized * 13f, ForceMode.Force);
    }

    public void moveShip()
    {
        Vector3 direction = new Vector3(currentMovementInput.x, 0, currentMovementInput.y);
        rb.AddForce(direction.normalized * 13f, ForceMode.Force);
    }

    void boostShip()
    {
        Vector3 direction = new Vector3(currentMovementInput.x, 0, currentMovementInput.y);
        rb.AddForce(direction.normalized * 13f, ForceMode.Force);
    }

    void stopShip()
    {
        rb.velocity = Vector3.zero;
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

}
