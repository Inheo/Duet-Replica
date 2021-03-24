using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CheckResults CheckResults;
    public ObstacleSpawner ObstacleSpawner;
    
    public float Speed;
    public float RotateSpeed = 350;

    [SerializeField] CircleCollider2D redBallCollider;
    [SerializeField] CircleCollider2D blueBallCollider;

    private float touchPosX = 0f;

    private Vector3 startPosition;

    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        startPosition = transform.position;

        rigidbody2D = GetComponent<Rigidbody2D>();

        MoveUp();
    }

    void Update()
    {
        if (!CheckResults.isGameover && !CheckResults.isGameComplete)
        {
            Rotate();
        }
        else if (CheckResults.isGameover)
        {
            ResetValues();
        }
        if(ObstacleSpawner.LastObstacle.position.y + 10 < transform.position.y)
        {
            CheckResults.isGameComplete = true;
            rigidbody2D.velocity = Vector2.MoveTowards(rigidbody2D.velocity, Vector2.zero, 6 * Time.deltaTime);
            rigidbody2D.rotation = Vector3.MoveTowards((rigidbody2D.rotation) * Vector3.forward, Vector3.zero, Speed).z;
            if (rigidbody2D.velocity == Vector2.zero && rigidbody2D.rotation == 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void ResetValues()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, Speed * Time.deltaTime * 10);
        rigidbody2D.rotation = Vector3.MoveTowards((rigidbody2D.rotation) * Vector3.forward, Vector3.zero, Speed * 5).z;
        if (transform.position == startPosition && rigidbody2D.rotation == 0)
        {
            CheckResults.isGameover = false;
            MoveUp();
            EnableBallCollider(true);
        }
    }

    private void MoveUp()
    {
        rigidbody2D.velocity = Vector2.up * Speed;
    }

    private void Rotate()
    {
        #region for mobile
        if (Input.GetMouseButtonDown(0))
            touchPosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        if (Input.GetMouseButton(0))
        {
            if (touchPosX > 0.01f)
                Rotate(-1);
            else
                Rotate(1);
        }
        #endregion
        #region for PC
        if (Input.GetKey(KeyCode.LeftArrow))
            Rotate(1);
        else if (Input.GetKey(KeyCode.RightArrow))
            Rotate(-1);
        #endregion

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetMouseButtonUp(0))
            rigidbody2D.angularVelocity = 0f;
    }
    private void Rotate(int direction)
    {
        rigidbody2D.angularVelocity = RotateSpeed * direction;
    }

    public void Restart()
    {
        EnableBallCollider(false);
        ResetVelocity();
    }

    private void EnableBallCollider(bool value)
    {
        redBallCollider.enabled = value;
        blueBallCollider.enabled = value;
    }

    private void ResetVelocity()
    {
        rigidbody2D.angularVelocity = 0f;
        rigidbody2D.velocity = Vector2.zero;
    }
}
