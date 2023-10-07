using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviour
{
    public Movement movement;

    public GameObject camera;

    public string nickname="unnamed";
    public int can=100;
    public int kill=0;
    public TextMeshPro nameUI;
    public Slider slid;
    public Transform TPHolder;
    public Transform parentTransform;
    public Transform childTransform;
    private PhotonView pv;


    public static PlayerSetup instance;
    private void Awake()
    {
        instance = this;

    }
    [PunRPC]
    public void IsLocalPlayer()
    {
        TPHolder.gameObject.SetActive(false);
        movement.enabled = true;
        camera.SetActive(true);

    }


        [PunRPC]
    public void SetNickname(string _name)
    {
        nickname = _name; 
        nameUI.text = _name;
    }



    [PunRPC]
    public void SetTPWeapon(int _weaponIndex)
    {
        foreach (Transform _weapon in TPHolder)
        {
            _weapon.gameObject.SetActive(false);

        }

        TPHolder.GetChild(_weaponIndex).gameObject.SetActive(true);

        //parentTransform.gameObject.SetActive(false);
    }

    [PunRPC]
    public void SetHealthBar(int health)
    {
        can = health;
        slid.value = health;
    }



    private void Update()
    {
        childTransform.parent = parentTransform;

    }
}
