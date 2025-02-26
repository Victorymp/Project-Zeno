using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject ship_prefab;
    bool shipCreated;
    float speed = 2.0f;
    public Vector3 pos; 
    public GameObject BattleShip;
    public GameObject Cruiser;
    public GameObject Destroyer;
    public GameObject Corvette;
    public GameMenu gameMenu;
    public CameraManager cameraManager;
    private BattleShipTemplate currentShip;

    public MoveCamera FPScamera;
    int currentItem = 0;

    public bool mapOpen;

    public float sens = 3f;
    public AudioClip shootSound;

    public List<GameObject> ships = new List<GameObject>();
    public int currentShipIndex = 0;

    public AudioSource audioSource;

    public Transform[] spawnPoints;

    public static ShipManager instance;

    public Text text;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        playerInput = GetComponent<PlayerInput>();
        gameMenu = GetComponent<GameMenu>();
        // Initialize the cameraManager object
        cameraManager = FindObjectOfType<CameraManager>();

        // Add the ships to the list
        ships.Add(BattleShip); // 0
        ships.Add(Cruiser);// 1
        // ships.Add(Destroyer);
        ships.Add(Corvette);// 2

        
        // Activate the first ship and deactivate the others
        for (int i = 0; i < ships.Count; i++)
        {
            ships[i].SetActive(i == currentShipIndex);
            // ships[i].GetComponent<BattleShip>().SetSensitivity(sens);
        }
        int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        ships[currentShipIndex].transform.position = spawnPoints[spawnIndex].position;
        // get the audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if(PlayerPrefs.HasKey("Sensitivity"))
        {
            if (PlayerPrefs.GetFloat("Sensitivity") == 0)
            {
                PlayerPrefs.SetFloat("Sensitivity", 3f);
            }
            sens = PlayerPrefs.GetFloat("Sensitivity");
        }
        // load the data of the current ship if it exists
        // LoadBattleShipData();


    }

    // Update is called once per frame
    void Update()
    {
        //changeShip();
        //ships[currentShipIndex].GetComponent<BattleShip>().SetCurrentship(currentShipIndex);
        transform.position = ships[currentShipIndex].transform.position;
    }


    public void ChangeActiveShip()
    {
        // Deactivate the current ship and disable its camera
        
        

        // Move to the next ship and camera position
        inRange();


        // Activate the new current ship and enable its camera
        ships[currentShipIndex].SetActive(true);
        
        // set the sensortivity of the camera
        //ships[currentShipIndex].GetComponent<BattleShip>().SetSensitivity(sens);

        // Set the spawn point of the ship
        int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        ships[currentShipIndex].transform.position = spawnPoints[spawnIndex].position;

        cameraManager.ChangeCamera(currentShipIndex);

        cameraManager.SetSensitivity(sens);

    }


    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mapOpen = !mapOpen;
            if (mapOpen)
            {
                gameMenu.menu.SetActive(true);
                playerInput.SwitchCurrentActionMap("BattleshipMenu");
            } else
            {
                gameMenu.menu.SetActive(false);
                playerInput.SwitchCurrentActionMap("BattleshipMoving");
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ships[currentShipIndex].GetComponent<BattleShip>().OnMove(context);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ships[currentShipIndex].GetComponent<BattleShip>().OnLook(context,cameraManager.Move(context.ReadValue<Vector2>()));
            
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        ships[currentShipIndex].GetComponent<BattleShip>().OnShoot(context);
        if (context.performed){
            AudioManager.instance.PlaySFXAudio("Shoot");
        }
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ships[currentShipIndex].GetComponent<BattleShip>().OnBoost(context);
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        ships[currentShipIndex].GetComponent<BattleShip>().OnReload(context);   
        Debug.Log("Reload");
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        ships[currentShipIndex].GetComponent<BattleShip>().OnAim(context);
        cameraManager.OnAim(context);
    }

    public void next()
    {
        inRange();
        ships[currentShipIndex].SetActive(false);
        currentShipIndex++;
        ChangeActiveShip();
    }

    public void previous()
    {
        inRange();
        ships[currentShipIndex].SetActive(false);
        currentShipIndex--;
        ChangeActiveShip();
    }

    public void Settings()
    {
        // Open the settings menu
        ships[currentShipIndex].GetComponent<BattleShip>().SaveBattleshipData();
        SceneManager.LoadScene("Settings");
    }

    public void LoadBattleShipData()
    {
        // check if the current ship index is valid
        if (currentShipIndex < 0 || currentShipIndex >= ships.Count)
        {
            return;
        }
        // Load the data of the current ship
        ships[currentShipIndex].GetComponent<BattleShip>().LoadBattleshipData();
    }

    public BattleShip getShip(int index)
    {
        return ships[index].GetComponent<BattleShip>();
    }

    public BattleShip GetShip()
    {
        return ships[currentShipIndex].GetComponent<BattleShip>();
    }

    public ShipManager GetShipManager()
    {
        return this;
    }


    private void inRange()
    {
        if(currentShipIndex >= ships.Count)
        {
            currentShipIndex = 0;
        }
        else if(currentShipIndex < 0)
        {
            currentShipIndex = ships.Count - 1;
        }
    }

    // remove if health is 0
    private void RemoveShip()
    {
        if (ships[currentShipIndex].GetComponent<BattleShip>().GetHealth() <= 0)
        {
            ships[currentShipIndex].SetActive(false);
            ships.RemoveAt(currentShipIndex);
            currentShipIndex = 0;
        }
    }

}
