using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class MultiMng : MonoBehaviourPunCallbacks, IOnEventCallback
{
    private float timeRemain;
    public bool timerIsRunning = false;

    public GameObject btnStart, btnReGame;

    public Text txt, txtPlayer, OpPlayer, RoomCode;


    private string opChoice = "";
    private string myChoice = "";


    public string[] choices;
    public Sprite rock, paper, scissor;
    public Image AIqm, qm;

    private const byte eventCodeTimer = 1;
    private const byte eventCodeChoice = 2;
    private const byte eventCode = 3;



    static IDictionary<string, string> selectOfPlayer;
    int takeEventStart;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte receiveCoide = photonEvent.Code;
        if (receiveCoide == eventCodeTimer)
        {
            timerIsRunning = (bool)photonEvent.CustomData;
            //opChoice = (string)photonEvent.CustomData;
            Debug.Log("Is Timer Run? : " + timerIsRunning);
        }
        if (receiveCoide == eventCodeChoice)
        {
            opChoice = (string)photonEvent.CustomData;
            //opChoice = (string)photonEvent.CustomData;
            Debug.Log("Opposite : " + opChoice);
        }
        if (receiveCoide == eventCode)
        {
            SceneManager.LoadScene(eventCode);
            //opChoice = (string)photonEvent.CustomData;
            Debug.Log("Opposite : " + opChoice);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        txtPlayer.text = PhotonNetwork.NickName;
        if(PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
        }
        RoomCode.text = "Room:  "+ PhotonNetwork.CurrentRoom.Name;
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            OpPlayer.text = PhotonNetwork.LocalPlayer.GetNext().NickName;
        }        
        timerIsRunning = false;
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            btnStart.SetActive(false);
            if(timerIsRunning == false)
            {
                txt.text = "Waiting host start game...";
            }
        }
        timeRemain = Const.TIMEREMAINING;
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player count :  " + PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.Log("Players info :  " + PhotonNetwork.LocalPlayer.ActorNumber);
      
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                Player opposite = PhotonNetwork.LocalPlayer.GetNext();
                OpPlayer.text = opposite.NickName;
                Debug.Log("op player  " + opposite.NickName);
                if (timerIsRunning)
                {
                    if (timeRemain > 0)
                    {
                        timeRemain -= Time.deltaTime;
                        txt.text = "You have " + (int)timeRemain + " seconds to choose!\n If you dont choose, you lose";
                    }
                    else
                    {
                    if (PhotonNetwork.LocalPlayer.IsMasterClient)
                    {
                        btnReGame.SetActive(true);
                    }
                    if (myChoice == "" && opChoice == "")
                    {
                        txt.text = "Draw!";
                    }
                    if (myChoice == "" && opChoice != "")
                    {
                        txt.text = "You lose!";
                        switch (opChoice)
                        {
                            case "Rock":
                                AIqm.sprite = rock;
                                break;
                            case "Paper":
                                AIqm.sprite = paper;
                                break;
                            case "Scissor":
                                AIqm.sprite = scissor;
                                break;

                        }
                    }
                    timeRemain = 0;
                        timerIsRunning = false;
                    }
                }
            }
               
        if(PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            txt.text = "Waiting another people...";
        }
    }


    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            timerIsRunning = true;
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            PhotonNetwork.RaiseEvent(eventCodeTimer, timerIsRunning, raiseEventOptions, SendOptions.SendReliable);
            btnStart.SetActive(false);
           
        }     
    }

    public void SetSelect(string mychoice)
    {
        RaiseEventOptions raiseEvent = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others
        };
        PhotonNetwork.RaiseEvent(eventCodeChoice, mychoice, raiseEvent, SendOptions.SendReliable);

        if (btnReGame.activeSelf == false && timerIsRunning == true)
        {

            StartCoroutine(Play(mychoice));
            Debug.Log("You choice: " +mychoice);
            switch (mychoice)
            {
                case "Rock":
                    qm.sprite = rock;
                    break;
                case "Paper":
                    qm.sprite = paper;
                    break;
                case "Scissor":
                    qm.sprite = scissor;
                    break;

            }
        }

    }

    public void Regame()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent(eventCode, eventCode, raiseEventOptions, SendOptions.SendReliable);
        SceneManager.LoadScene(3);
    }

    public void backMenu()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator Play(string myChoices)
    {
        yield return new WaitForSeconds(timeRemain);
        if (myChoices != "" && opChoice == "")
        {
            txt.text = "You win!";
        }
       

        if (myChoices == opChoice)
        {
            txt.text = "Draw!";
            AIqm.sprite = qm.sprite;
        }
        else
            switch (myChoices)
            {
                case "Rock":
                    switch (opChoice)
                    {
                        case "Paper":
                            txt.text = "You Lose!";
                            AIqm.sprite = paper;
                            break;

                        case "Scissor":
                            txt.text = "You Win!";
                            AIqm.sprite = scissor;
                            break;
                    }
                    break;
                case "Paper":
                    switch (opChoice)
                    {
                        case "Rock":
                            txt.text = "You Win!";
                            AIqm.sprite = rock;
                            break;
                        case "Scissor":
                            txt.text = "You Lose!";
                            AIqm.sprite = scissor;
                            break;
                    }
                    break;

                case "Scissor":
                    switch (opChoice)
                    {
                        case "Rock":
                            txt.text = "You Lose!";
                            AIqm.sprite = rock;
                            break;
                        case "Paper":
                            txt.text = "You Win!";
                            AIqm.sprite = paper;
                            break;
                    }
                    break;

            }


    }


}
