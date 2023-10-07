using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public GameObject playersHolder;
    public float refreshRate = 1f;
    public GameObject[] slots;
    [Space]
    public TextMeshProUGUI[] scoreTexts; 
    public TextMeshProUGUI[] nameTexts;

    private void Start()
    {
        InvokeRepeating(nameof(Refresh),1f,refreshRate);
    }
    public void Refresh()
    {
        foreach (var slot in slots)
        {
            slot.SetActive(false);
        }

        var sortedPlayers = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player);

        int i = 0;
        foreach (var player in sortedPlayers)
        {
            slots[i].SetActive(true);

            if (player.NickName == "")
                player.NickName = "unnamed";

            nameTexts[i].text = player.NickName;
            scoreTexts[i].text = player.GetScore().ToString();

            i++;
        }
    }

    private void Update()
    {
        playersHolder.SetActive(Input.GetKey(KeyCode.Tab));
    }
}
