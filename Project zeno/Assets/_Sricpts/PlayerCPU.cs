using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using UnityEngine.SceneManagement;

public class PlayerPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonHandlerPlay()
    {
        SceneManager.LoadSceneAsync("Terrain");
    }
}
