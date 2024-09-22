using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] public SO_Ball sO_Ball;

    void Start()
    {
        if (sO_Ball != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sO_Ball.ballSprite;
        }
    }
}