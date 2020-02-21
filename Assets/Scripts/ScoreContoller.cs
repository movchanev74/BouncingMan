using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreContoller : MonoBehaviour
{
    public Text ScoreLabel;

    private void Start()
    {
        ScoreLabel.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void IncreaseScore()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
        ScoreLabel.text = PlayerPrefs.GetInt("Score").ToString();
    }
}
