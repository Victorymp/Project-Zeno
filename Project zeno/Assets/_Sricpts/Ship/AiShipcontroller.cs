using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class AiShipcontroller : MonoBehaviour
{
    public GameObject[] ships;

    [SerializeField]
    public Text CounterText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();   
    }

    public int checkHealth()
    {
        int counter = ships.Length;
        foreach (GameObject ship in ships)
        {
            if (ship.GetComponent<AiShip>().health <= 0)
            {
                counter--;
                Destroy(ship);
            }
        }
        CounterText.text = counter.ToString();
        return counter;
    }


}
