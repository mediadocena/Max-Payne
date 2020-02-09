using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntuacion : MonoBehaviour
{
    public int score = 0;
    public Text points;
    void Update()
    {
        points.text = "" + score;
    }
    public void addScore() {
        score++;
    }
}
