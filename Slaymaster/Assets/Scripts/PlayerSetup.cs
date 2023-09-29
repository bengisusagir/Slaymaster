using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public Movement movement;

    public GameObject camera;

    public static PlayerSetup instance;
    private void Awake()
    {
        instance = this;
    }
    public void IsLocalPlayer()
    {
        movement.enabled = true;
        camera.SetActive(true);
    }
    
}
