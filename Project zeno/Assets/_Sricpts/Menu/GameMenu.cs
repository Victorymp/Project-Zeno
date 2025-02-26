using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public PlayerInput playerInput;
    public ShipManager shipManager;
    public GameObject ControllerMessage;

    public GameObject startMessage;

    public GameObject Enemy;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        shipManager = GetComponent<ShipManager>();
        menu.SetActive(false);
        AudioManager.instance.PlayAudio("Background");
        StartCoroutine(ShowStartMessage());
    }

    // Update is called once per frame
    void Update()
    {
        // check if controller is connectd
        if (Gamepad.current != null)
        {
            // if the controller is connected, switch to the BattleshipMoving action map
            // playerInput.SwitchCurrentActionMap("BattleshipMoving");
            ControllerMessage.SetActive(false);
        }
        else
        {
            // if the controller is not connected, switch to the KeyboardMouse action map
            // playerInput.SwitchCurrentActionMap("KeyboardMouse");
            ControllerMessage.SetActive(true);
        }
        // check if the game is over
        EndGame();
    }

    public void OnMap(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            // If the map is open, close it and switch to the BattleshipMoving action map
            if(shipManager.mapOpen)
            {
                shipManager.mapOpen = false;
                menu.SetActive(false);
                playerInput.SwitchCurrentActionMap("BattleshipMoving");
            }
        }
    }

    public void OnNextShip(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            shipManager.next();
        }
    }

    public void OnPreviousShip(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            shipManager.previous();
        }
    }

    public void OnSettings(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SceneManager.LoadSceneAsync("Settings");
        }
    }

    public void OnQuit(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Application.Quit();
        }
    }

    public void EndGame()
    {
        if(Enemy.GetComponent<AiShipcontroller>().checkHealth() == 0)
        {
            SceneManager.LoadSceneAsync("EndGame");
        }
        if(shipManager.ships[shipManager.currentShipIndex].GetComponent<BattleShip>().health <= 0)
        {
            SceneManager.LoadSceneAsync("EndGame");
        }
    }

    IEnumerator ShowStartMessage()
    {
        startMessage.SetActive(true);
        yield return new WaitForSeconds(5);
        startMessage.SetActive(false);
    }
}
