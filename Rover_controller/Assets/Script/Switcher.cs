using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{

    public GameObject demoWorld, gameWorld, demoPanel, gamePanel;



    // Start is called before the first frame update
    void Start()
    {
        gameWorld.SetActive(false);
        gamePanel.SetActive(false);
        demoWorld.SetActive(true);
        demoWorld.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void demoModeFunction()
    {
        demoWorld.SetActive(true);
        gameWorld.SetActive(false);
        demoPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void gameModeFunction()
    {
        demoWorld.SetActive(false);
        gameWorld.SetActive(true);
        demoPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
}
