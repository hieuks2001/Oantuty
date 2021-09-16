using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField Name;
    public GameObject circleProgresbar,btnAi, btnMulti;
  

    // Start is called before the first frame update
    public void MultiPlayer()
    {
       
        circleProgresbar.SetActive(true);
        btnAi.SetActive(false);
        btnMulti.SetActive(false);

        PhotonNetwork.ConnectUsingSettings(); 
    }
    private void Update()
    {
        if(circleProgresbar.activeSelf == true)
        {
            Name.enabled = false;
        }
    }

    public override void OnConnectedToMaster()
    {
        if (PhotonNetwork.JoinLobby())
        {
            circleProgresbar.SetActive(false);
        }
    }

    public void Computer()
    {
        SceneManager.LoadScene("AImode");

    }

    public override void OnJoinedLobby()
    {
        if (Name.text != "")
        {
            PhotonNetwork.NickName = Name.text; ;
        }
        else
        {
            PhotonNetwork.NickName = "UnknowPlayer";
        }
        SceneManager.LoadScene("Lobby");
    }
}
 