using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class Ball : MonoBehaviourPunCallbacks
{
    public float speed;
    public Rigidbody2D rb;
    public PhotonRigidbody2DView myPV;
    public PhotonView myView;
    public GameObject Player1Text;
    public GameObject Player2Text;


    void Start()
    {
        Player2Text = GameObject.FindWithTag("Player2Text");
        Player1Text = GameObject.FindWithTag("Player1Text");
        myView = GetComponent<PhotonView>();
        Launch();
        myPV = GetComponent<PhotonRigidbody2DView>();
    }
   

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }
    [PunRPC]
    public void Player1Scored()
    {

        PlayerScript.Instance.playerscore++;
        Player1Text.GetComponent<TextMeshProUGUI>().text = PlayerScript.Instance.playerscore.ToString();

        

    }
    [PunRPC]
    public void Player2Scored()
    {
        PlayerScript.Instance.playerscore2++;
        Player2Text.GetComponent<TextMeshProUGUI>().text = PlayerScript.Instance.playerscore2.ToString();

    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player2Goal"))
        {
            Debug.Log("Player 2 Scoreed...");
            myView.RPC("Player2Scored",RpcTarget.All);
            PhotonNetwork.Destroy(GameObject.Find("GameManager").GetComponent<GameManager>().activeball);
            GameObject.Find("GameManager").GetComponent<GameManager>().SpawnBall();
           
        }
        else if (collision.gameObject.CompareTag("Player1Goal"))
        {   
            Debug.Log("Player 1 Scoreed...");
            myView.RPC("Player1Scored", RpcTarget.All);
            PhotonNetwork.Destroy(GameObject.Find("GameManager").GetComponent<GameManager>().activeball);
            GameObject.Find("GameManager").GetComponent<GameManager>().SpawnBall();

        }
        
    }
    
    
    

}
