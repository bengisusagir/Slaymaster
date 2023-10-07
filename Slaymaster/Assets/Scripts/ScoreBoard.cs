using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public float refreshRate = 1f;
    public GameObject[] slots;
    [Space]
    public TextMeshProUGUI[] scoreTexts; 
    public TextMeshProUGUI[] nameTexts;

    private void Start()
    {
        InvokeRepeating(nameof(Refresh),1f,refreshRate);
    }
    public static int GetScore(this Player player)
    {
        object score;
        if (player.CustomProperties.TryGetValue("score", out score))
        {
            return (int)score;
        }
        return 0; // Varsayýlan deðer
    }
    public void Refresh()
    {
        foreach (var slot in slots)
        {
            slot.SetActive(false);
        }

        List<Player> sortedPlayers = PhotonNetwork.PlayerList.OrderByDescending(player => player.GetScore()).ToList();

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
}
