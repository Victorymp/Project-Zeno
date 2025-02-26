using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoScript : MonoBehaviour
{
    public Image[] images;

    public BattleShip BattleShip;

    float _ROCKET;

    public bool isReloading;

    // Start is called before the first frame update
    void Start()
    {
        _ROCKET = BattleShip.rockets;
    }

    // Update is called once per frame
    void Update()
    {
     for (int i = 0; i < images.Length; i++)
        {
            if (i < BattleShip.rockets)
            {
                images[i].enabled = true;
            }
            else
            {
                images[i].enabled = false;
            }
        }   
        
    }

    
}
