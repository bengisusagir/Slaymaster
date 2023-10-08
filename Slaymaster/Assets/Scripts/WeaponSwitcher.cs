using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    public GameObject player;
    private int selectedWeapon = 0;
    public GameObject spidergun;
    public GameObject bluegun;
    void Start()
    {
        PV = player.GetComponent<PhotonView>();
        
    }
    [PunRPC]
    void SetGun(string gun)
    {
        spidergun.SetActive(false);
        bluegun.SetActive(false);

        if(gun=="spidergun")
            spidergun.SetActive(true);
        if (gun == "bluegun")
            bluegun.SetActive(true);

    }
    [PunRPC]
    void TakeGun(string gun)
    {

        GameObject gobje = GameObject.Find(gun);
        if (gobje != null)
        {
            gobje.SetActive(false);
            if(gobje.name=="spiderg")
            {
            spidergun.SetActive(true);
            PV.RPC("SetGun", RpcTarget.All, "spidergun"); 
            }
            else if (gobje.name == "blueg")
            {
                bluegun.SetActive(true);
                PV.RPC("SetGun", RpcTarget.All, "bluegun");
            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gun")
        {
            PV.RPC("TakeGun", RpcTarget.AllBuffered, other.gameObject.name);

        }

    }
    void Update()
    {
        if (!PV.IsMine)
            return;


    }
}
