using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    public GameObject player;
    private int selectedWeapon = 0;
    void Start()
    {
        PV = player.GetComponent<PhotonView>();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2;
        }

        if(previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
        
    }

     public void SelectWeapon()
    {
        PV.RPC("SetTPWeapon", RpcTarget.All, selectedWeapon);
        int i = 0;
        foreach (Transform _weapon in transform)
        {
            if( i == selectedWeapon)
            {
                _weapon.gameObject.SetActive(true);
            }
            else
            {
                _weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
