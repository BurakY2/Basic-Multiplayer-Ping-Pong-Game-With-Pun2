using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    float speed = 5f;
    public Rigidbody2D rb;

    private float movement;
    private PhotonView myPV;

    public int playerscore = 0;
    public int playerscore2 = 0;
    int score;
    int score2;

    public ExitGames.Client.Photon.Hashtable myCustomProps;
    public ExitGames.Client.Photon.Hashtable myCustomProps2;

    public static PlayerScript Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        myCustomProps = new ExitGames.Client.Photon.Hashtable();
        myCustomProps2 = new ExitGames.Client.Photon.Hashtable();
        myPV = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (myPV.IsMine)
        {
            movement = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, movement * speed);
        }

        myCustomProps["goal1"] = playerscore;
        myCustomProps2["goal2"]= playerscore2;
        Debug.Log(playerscore);
        Debug.Log(playerscore2);

    }
    




}
