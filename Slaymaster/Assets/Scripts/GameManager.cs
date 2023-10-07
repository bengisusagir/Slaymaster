using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.Globalization;
using System.Security.Cryptography;
using System.Linq;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Player")]
    public string[] playerPrefabLocation;
    public Transform[] spawnPoints;
    public Movement[] players;
    private int playersInGame;
    private Timer timer;
    public GameObject roomCam;



    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        timer = GetComponent<Timer>();
    }
    void Start()
    {
        players = new Movement[PhotonNetwork.PlayerList.Length];
        photonView.RPC("ImInGame", RpcTarget.All);
    }
    [PunRPC]
    public void ImInGame()
    {
        playersInGame++;

        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();
            timer.enabled(true);
        }
    }
    public void SpawnPlayer()
    {
        roomCam.SetActive(false);
        GameObject playerObj = (GameObject)PhotonNetwork.Instantiate(playerPrefabLocation[Random.Range(0, playerPrefabLocation.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        PlayerSetup.instance.IsLocalPlayer();
        playerObj.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName);
        playerObj.GetComponent<Health>().isLocalPlayer = true;
        PhotonNetwork.LocalPlayer.NickName = PlayerSetup.instance.nickname;

    }
    [PunRPC]
    public void GameOver()
    {
        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "Menu");
        Destroy(NetworkManager.instance.gameObject);
    }

    public Movement GetPlayer(int playerId)
    {
        return players.First(x => x.id == playerId);
    }
    public Movement GetPlayer(GameObject playerObj)
    {
        return players.First(x => x.gameObject == playerObj);
    }
    public void OnGoBackButton()
    {
        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "Menu");
        Destroy(NetworkManager.instance.gameObject);
    }
}
