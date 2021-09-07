using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCtrl : MonoBehaviour
{

    public Text txt;
    public string[] choices;
    public Sprite rock, paper, scissor;
    public Image AIqm,qm;

    public float time;
    // Start is called before the first frame update
    private void Start()
    {
        time = Const.TIMEREMAINING;
    }


    public void Play(string myChoices)
    {
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
                while(time > -1)
                {
                    time -= Time.deltaTime;
                    if (time == 0)
                    {
                        AIqm.sprite = rock;
                    }
                }
                
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
                while(time > -1)
                {
                    time -= Time.deltaTime;
                    if (time == 0)
                    {
                        AIqm.sprite = paper;
                    }
                }
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
                while(time>-1)
                {
                    time -= Time.deltaTime;
                    if (time == 0)
                    {
                        AIqm.sprite = scissor;
                    }
                }
              
                break;

        }
    }
}
