using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeController : MonoBehaviour
{
    public float speed;
    public float sensitivity;
    public Camera mainCamera;
    public List<TailController> tailBones;
    public GameObject tailPrefab;
    public Color snakeColor;
    public Text scoreText;
    public Text cristalText;

    private InputManager playerInput;
    private bool isAlive;
    private Rigidbody snakeRb;
    private Quaternion snakeRotation;
    private Vector3 offsetCamera;
    private int score = 0;
    private int cristals = 0;
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
        offsetCamera = mainCamera.transform.position - transform.position;
    }

    void Update()
    {

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

    private void LateUpdate()
    {
        CameraMove();
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
        snakeRb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        TailMove();

    }

    private void CameraMove()
    {
        Vector3 newCamPosition = new Vector3(0.0f, 0.0f, transform.position.z);
        mainCamera.transform.position = newCamPosition + offsetCamera;
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
        score++;
        scoreText.text = "" + score;
    }

    public void ChangeCristal()
    {
        cristals++;
        cristalText.text = "" + cristals;
        if (cristals > 3)
        {
            isPower = true;
            StartCoroutine(PowerTime());
        }
    }

    private void PowerActive()
    {
        prevPos = transform.position;
        snakeRb.MoveRotation(Quaternion.identity);
        snakeRb.MovePosition(new Vector3(0, transform.position.y, transform.position.z) + Vector3.forward * speed * 3 * Time.deltaTime);
        TailMove();
    }

    private IEnumerator PowerTime()
    {
        yield return new WaitForSeconds(5f);
        isPower = false;
        cristals = 0;
        cristalText.text = "" + cristals;
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
            GameManager.Instance.Finish(score);
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
