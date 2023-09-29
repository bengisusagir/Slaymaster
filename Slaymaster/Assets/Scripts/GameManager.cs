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
    public string playerPrefabLocation;
    public Transform[] spawnPoints;
    public Movement[] players;
    private int playersInGame;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
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
        }
    }
    void SpawnPlayer()
    {
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        PlayerSetup.instance.IsLocalPlayer();
    }
    public Movement GetPlayer(int playerId)
    {
        return players.First(x => x.id == playerId);
    }
    public Movement GetPlayer(GameObject playerObj)
    {
        return players.First(x => x.gameObject == playerObj);
    }
}
