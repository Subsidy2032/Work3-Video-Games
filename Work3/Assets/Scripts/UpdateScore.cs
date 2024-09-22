using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UpdateScore : MonoBehaviour
{
    private BallHoleCollisionChannel ballHoleCollisionChannel;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        ballHoleCollisionChannel = Beacon.GetInstance().ballHoleCollisionChannel;
        ballHoleCollisionChannel.CollisionDetected += AddToScore;
    }

    void AddToScore(GameObject ball)
    {
        BallScript ballScript = ball.GetComponent<BallScript>();
        SO_Ball sO_Ball = ballScript.sO_Ball;
        score += sO_Ball.score;
        scoreText.text = "Your Score: " + score;
    }
}
