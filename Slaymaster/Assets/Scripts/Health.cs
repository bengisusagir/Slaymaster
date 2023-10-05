using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;
    public PhotonView pv;
    public Slider _slider;

    [Header("UI")]
    public TextMeshProUGUI healthText;
    public AudioSource died;


    public static Health instance; 
    private void Awake()
    {

        instance = this;
    }

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        healthText.text = health.ToString();
        if (health <= 0)
        {
            died.Play();
            pv.RPC("PlayDieForAll", RpcTarget.All);
            Destroy(gameObject);

        }
        pv.RPC("UpdateHealth", RpcTarget.AllBuffered, health);
    }
    private void Start()
    {
        health = maxHealth;
    }
    [PunRPC]
    public void PlayDieForAll()
    {
        died.Play();


    }

    [PunRPC]
    private void UpdateHealth(int newHealth)
    {
        health = newHealth;
        _slider.value = newHealth;
    }
}
