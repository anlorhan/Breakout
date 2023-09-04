using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Action<Ball> OnBallDeath;

    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject, 1);
    }
}
