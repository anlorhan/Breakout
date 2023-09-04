using System;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Paddle")
        {
            ApplyEffect();
        }

        if (collision.tag == "Paddle" || collision.tag=="Death")
        {
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyEffect();
    
}
