using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Ball")]
    public GameObject ballPrefab;

    [Header("Player 1")]
    public GameObject playerPrefab;
    public GameObject player1Goal;

    [Header("Player 2")]
    public GameObject player2Prefab;
    public GameObject player2Goal;
    
    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;

    public int Player1Score;

    public int Player2Score;
    
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    TMP_Text winnerText;
    
    bool GameOver;
    public GameObject activeball;

  

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(7f, 0f), Quaternion.identity);
            PhotonNetwork.LocalPlayer.SetCustomProperties(PlayerScript.Instance.myCustomProps);
            SpawnBall();
        }
        else
        {
            GameObject player2 = PhotonNetwork.Instantiate(player2Prefab.name, new Vector2(-7f, 0f), Quaternion.identity);
            PhotonNetwork.LocalPlayer.SetCustomProperties(PlayerScript.Instance.myCustomProps2);
        }
        

    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
        }
        
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("Winner", RpcTarget.All);
        

    }
   
    
    public void SpawnBall()
    {
        if (!PhotonNetwork.IsMasterClient) return;

            activeball = PhotonNetwork.Instantiate(ballPrefab.name, new Vector2(0f, 0f), Quaternion.identity);
       
    }
    
    public void MainMenu()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
    
    [PunRPC]
    public void Winner()
    {

        if (!GameOver)
        {
            if (PlayerScript.Instance.playerscore >= 6)
            {

                GameOver = true;
                winnerText.text = "Player 1 wins you can prees Escape button for build";
                gameOverPanel.SetActive(true);
                PhotonNetwork.Destroy(activeball);
            }

            else if (PlayerScript.Instance.playerscore2 >= 6)
            {
                GameOver = true;
                winnerText.text = "Player 2 wins you can prees Escape button for build";
                gameOverPanel.SetActive(true);
                PhotonNetwork.Destroy(activeball);
            }
        }

    }
  
    
}

    



