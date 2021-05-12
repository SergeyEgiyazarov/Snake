using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void SnakeEvents();

public class SnakeController : MonoBehaviour
{
    public float speed;
    public float forwardSpeed;
    public List<TailController> tailBones;
    public GameObject tailPrefab;
    public Color snakeColor;

    public event SnakeEvents SnakeEatingHuman;
    public event SnakeEvents SnakeEatingCristal;
    public event SnakeEvents EndFaver;

    private InputManager playerInput;
    private bool isAlive;
    private Rigidbody snakeRb;
    private Quaternion snakeRotation;
    private bool isPower = false;
    private LayerMask floorMask;

    private Vector3 prevPos;

    private void Awake()
    {
        playerInput = GetComponent<InputManager>();
        snakeRb = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
    }

    void Start()
    {
        isAlive = true;
        snakeRotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        if (!isPower)
        {
            MoveAndTurn();
        }
        else
        {
            PowerActive();
        }
    }

    private void MoveAndTurn()
    {
        if (!isAlive || isPower) return;

        Vector3 touchPosition = playerInput.GetTouchPosition();

        if (playerInput.GetTouchPress())
        {
            Ray rayCast = Camera.main.ScreenPointToRay(touchPosition);

            RaycastHit raycastHit;

            if (Physics.Raycast(rayCast, out raycastHit, 100, floorMask))
            {
                Vector3 lookPosition = new Vector3(raycastHit.point.x, transform.position.y, transform.position.z + 1f);
                snakeRotation = Quaternion.LookRotation(lookPosition - transform.position);
                snakeRb.MoveRotation(snakeRotation);
            }
        }
        else
        {
            snakeRotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.1f);
            snakeRb.MoveRotation(snakeRotation);
        }

        prevPos = transform.position;
        snakeRb.MovePosition(transform.position + Vector3.forward * forwardSpeed * Time.deltaTime + transform.forward * speed * Time.deltaTime);
        TailMove();

    }

    private void TailMove()
    {
        Vector3 currentPos = prevPos;
        Vector3 prevBonePos;


        foreach (var bone in tailBones)
        {
            prevBonePos = bone.transform.position;

            bone.target = currentPos;

            currentPos = prevBonePos;
        }
    }

    public void ChangeScore()
    {
        SnakeEatingHuman();
    }

    public void ChangeCristal()
    {
        SnakeEatingCristal();
    }

    public void StartFaver()
    {
        StartCoroutine(PowerTime());
    }

    private void PowerActive()
    {
        prevPos = transform.position;
        snakeRb.MoveRotation(Quaternion.identity);
        snakeRb.MovePosition(new Vector3(0, transform.position.y, transform.position.z) + Vector3.forward * (speed + forwardSpeed) * 3 * Time.deltaTime);
        TailMove();
    }

    private IEnumerator PowerTime()
    {
        isPower = true;
        yield return new WaitForSeconds(5f);
        isPower = false;
        EndFaver();
    }

    public void AddTile()
    {
        GameObject newTail = Instantiate(tailPrefab);
        newTail.GetComponent<MeshRenderer>().material.color = gameObject.GetComponent<MeshRenderer>().material.color;
        tailBones.Add(newTail.GetComponent<TailController>());
    }

    public void StopGame(bool isGameOver)
    {
        isAlive = false;
        if (isGameOver)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            GameManager.Instance.Finish();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FinishZone"))
        {
            StopGame(false);
        }
        else if (collision.gameObject.CompareTag("Bomb"))
        {
            if (isPower)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                StopGame(true);
            }
        }
    }
}
