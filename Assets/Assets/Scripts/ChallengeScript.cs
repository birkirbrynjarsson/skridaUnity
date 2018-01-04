using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DoozyUI;

public class ChallengeScript : MonoBehaviour {

    public List<Challenge> challenges = new List<Challenge>(){
        new Challenge(){
            question = "Hvaða ár var Skriðuklausturskirkjan vígð?",
            answers = new string[] {"1512"}
        }
    };

    public Text title;
    public Text question;
    public Image image;
    public InputField answerInput;
    public string[] answers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void tryAnswer(){
        string input = answerInput.text;
        foreach(string answer in answers){
            if(input == answer){
                correctAnswer();
                return;
            }
        }
        wrongAnswer();
        return;
    }

    void correctAnswer(){
        UIManager.ShowNotification("ItemNotification", 10f, true, "Fjársjóðsfundur!", itemName, item0);
    }

    void wrongAnswer(){
        
    }
}

public class Challenge {
    public string question;
    public string[] answers;
}