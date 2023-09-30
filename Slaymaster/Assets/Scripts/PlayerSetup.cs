using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
    public Movement movement;

    public GameObject camera;

    public static PlayerSetup instance;
    private void Awake()
    {
        instance = this;
    }
    [PunRPC]
    public void IsLocalPlayer()
    {
        movement.enabled = true;
        camera.SetActive(true);
    }
    
}
