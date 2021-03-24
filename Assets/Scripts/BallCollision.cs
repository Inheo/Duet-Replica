using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public CheckResults CheckResults;
    public PlayerMovement PlayerMovement;
    public SplattersSpawner SplattersSpawner;

    private ParticleSystem explosionFx;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        explosionFx = transform.GetChild(0).GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            CheckResults.isGameover = true;

            explosionFx.Play();
            SplattersSpawner.AddSplatter(collision.transform, collision.contacts[0].point, spriteRenderer.color);

           PlayerMovement.Restart();
        }
    }
}
