using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float startPositionY;

    public GameObject Obstacle;

    public Transform LastObstacle;

    private float widthCamera;

    private Vector3[] positions = { new Vector3(-1.4f, 0, 0), new Vector3(1.4f, 0, 0) };

    private Camera camera;
    private SpriteRenderer obstacleSprite;

    private void Start()
    {
        camera = Camera.main;
        obstacleSprite = Obstacle.GetComponent<SpriteRenderer>();
        SetPositions();

        int countSpawnObstacle = Random.Range(15, 20);
        for (int i = 0; i < countSpawnObstacle; i++)
        {
            Vector3 pos = positions[Random.Range(0, positions.Length)];
            pos.y = startPositionY + 4.2f * i;
            LastObstacle = Instantiate(Obstacle, pos, Quaternion.identity, transform).transform;
        }
    }

    private void SetPositions()
    {
        widthCamera = camera.ScreenToWorldPoint(Vector3.one * camera.pixelWidth).x;
        positions[0] = new Vector3(-(widthCamera - obstacleSprite.size.x / 2 - 0.1f), 0, 0);
        positions[1] = new Vector3(widthCamera - obstacleSprite.size.x / 2 - 0.1f, 0, 0);
    }
}
