using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //UI text display for score
    public Text _scoreDisplay;
    //Current score value
    private int _score = 0;

    private void Start()
    {
        //Reset the score value to zero
        ResetScore();
    }

    private void Update()
    {
        //Update score display
        _scoreDisplay.text = _score.ToString();
    }

    public void ResetScore()
    {
        //Set score to 0
        _score = 0;
        //Display new 0 score
        _scoreDisplay.text = _score.ToString();
    }

    public void AddPoints(int pointVal)
    {
        //Add points to score value
        _score += pointVal;
    }

    public void RemovePoints(int pointVal)
    {
        //Subtract points from score value
        _score -= pointVal;
    }
}
