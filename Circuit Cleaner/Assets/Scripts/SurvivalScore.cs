using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class SurvivalScore : MonoBehaviour {

	void Start () {
        startGame();
	}

    DateTime startTime;

    public void addScore(int id, int hour, int minutes, int seconds)
    {
        string time = "";
        if (hour > 9)
            time += hour + "";
        else
            time += "0" + hour;
        time += ":";
        if (minutes > 9)
            time += minutes + "";
        else
            time += "0" + minutes;
        time += ":";
        if (seconds > 9)
            time += seconds + "";
        else
            time += "0" + seconds;
        string scoreData = "";
        try
        {
            scoreData = System.IO.File.ReadAllText(@"score.txt");
        }
        catch { }
        string finalData = id + " " + time + "\n" + scoreData;
        System.IO.File.WriteAllText("score.txt", finalData, Encoding.UTF8);

    }
    public void startGame()
    {
        startTime = DateTime.Now;
    }
    public void endGame()
    {
        TimeSpan t = DateTime.Now.Subtract(startTime);
        addScore(1, t.Hours, t.Minutes, t.Seconds); // put your ID here, in the first param
    }
}
