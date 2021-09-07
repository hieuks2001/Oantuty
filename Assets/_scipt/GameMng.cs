using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{
    private float timeRemain;
    public bool timerIsRunning = false;

    public GameObject gtxt, btnStart,btnReGame;

    public Text txt;

    private string myChoice = "";
    public string ReplaceText = "";
    public string[] choices;
    public Sprite rock, paper, scissor;
    public Image AIqm, qm;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        timeRemain = Const.TIMEREMAINING;
        Time.timeScale = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemain > 0)
            {
                timeRemain -= Time.deltaTime;
                txt.text = "You have " + (int)timeRemain + " seconds to choose!\n If you dont choose, you lose";               
            }
            else
            {
                if (myChoice == "")
                {
                    txt.text = "You Lose!";
                }              
                btnReGame.SetActive(true);             
                timeRemain = 0;
                timerIsRunning = false;
            }
        }
      
    }
        public void setSelect(string mychoice)
    {
        if (gtxt.active == true && btnReGame.active==false)
        {
            this.myChoice = mychoice;
            StartCoroutine(Play(myChoice));
            Debug.Log(myChoice);
            switch (myChoice)
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
   
    public void startGame()
    {
        Time.timeScale = 1;
        btnStart.SetActive(false);
        gtxt.SetActive(true);
    }
    public void Regame()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator Play(string myChoices)
    {  
       

                yield return new WaitForSeconds(timeRemain);

                string randomChoices = choices[Random.Range(0, choices.Length)];

                switch (randomChoices)
                {
                    case "Rock":
                        switch (myChoices)
                        {
                            case "Rock":
                                txt.text = "Draw!";                           
                                qm.sprite = rock;
                                break;
                            case "Paper":                           
                                txt.text = "You Win!";
                                qm.sprite = paper;
                                break;

                            case "Scissor":
                                txt.text = "You Lose!";                           
                                qm.sprite = scissor;
                                break;
                        }                
                        AIqm.sprite = rock;
                        break;
                    case "Paper":
                        switch (myChoices)
                        {
                            case "Rock":
                                txt.text = "You Lose!";
                                qm.sprite = rock;
                                break;
                            case "Paper":
                                txt.text = "Draw!";
                                qm.sprite = paper;
                                break;
                            case "Scissor":
                                txt.text = "You Win!";
                                qm.sprite = scissor;
                                break;
                        }
                        AIqm.sprite = paper;
                        break;

                    case "Scissor":
                        switch (myChoices)
                        {
                            case "Rock":
                                txt.text = "You Win!";
                                qm.sprite = rock;
                                break;
                            case "Paper":
                                txt.text = "You Lose!";
                                qm.sprite = paper;
                                break;

                            case "Scissor":
                                txt.text = "Draw!";
                                qm.sprite = scissor;
                                break;
                        }
                        AIqm.sprite = scissor;
                        break;

                }

          
        }
        
    
}
