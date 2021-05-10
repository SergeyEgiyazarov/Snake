using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    private MeshRenderer snakeRenderer;
    private SnakeController snake;

    void Start()
    {
        snakeRenderer = GetComponentInParent<MeshRenderer>();
        snake = GetComponentInParent<SnakeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Eat"))
        {
            EatingEat(other.gameObject);
            Destroy(other.gameObject, 0.2f);
        }
        else if (other.CompareTag("Cristal"))
        {
            EatingCristal(other.gameObject);
        }
    }

    private void EatingEat(GameObject other)
    {
        MeshRenderer eatRender = other.GetComponent<MeshRenderer>();

        other.transform.position = Vector3.Slerp(other.transform.position, transform.position, 0.1f);

        if (eatRender.material.color.linear == snakeRenderer.material.color.linear)
        {
            snake.AddTile();
            snake.ChangeScore();
            Debug.Log("Eat");
        }
        else
        {
            GameOver();
        }
    }

    private void EatingCristal(GameObject other)
    {
        snake.ChangeCristal();
        Destroy(other);
        Debug.Log("Cristal");
    }

    private void GameOver()
    {
        snake.StopGame(true);
    }
}
