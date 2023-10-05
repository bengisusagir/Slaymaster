using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviourPun
{
    public int damage;
    public float fireRate;

    public Camera camera;
    public GameObject spi;
    private float nextFire;
    public AudioSource au;

    public PhotonView pv;

    [Header("VFX")]
    public GameObject hitVFX;

    // Update is called once per frame
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (!pv.IsMine)
            return;

        if (nextFire > 0)
            nextFire -= Time.deltaTime;
        if (Input.GetButton("Fire1") && nextFire <= 0)
        {
            nextFire = 1 / fireRate;
            Fire();
            au.Play();
            pv.RPC("PlaySoundsForAll", RpcTarget.All);
        }
    }
    [PunRPC]
    public void PlaySoundsForAll()
    {
        au.Play();

    }
    void Fire()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray.origin,ray.direction,out hit, 100f))
        {
            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);
            

            if (hit.transform.gameObject.GetComponent<Health>())
            {
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All,damage);
            }
        }
    }
}
