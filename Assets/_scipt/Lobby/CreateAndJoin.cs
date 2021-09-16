using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public InputField Idroom;
    public Text Nametxt;
    public Button createButton, joinButton;

    void Start()
    {
        Nametxt.text = "Xin chao "+ PhotonNetwork.NickName;
    }
   

    void Update()
    {
        if (Idroom.text != "")
        {
            createButton.interactable = true;
            joinButton.interactable = true;
        }
        else
        {
            createButton.interactable = false;
            joinButton.interactable = false;
        }
    }
    public void CreateRoom()
    {
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(Idroom.text,roomOption);
    }

    // Update is called once per frame
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(Idroom.text);
        Debug.Log(Idroom.text);

    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(3);
    }
}
