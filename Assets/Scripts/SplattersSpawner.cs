using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplattersSpawner : MonoBehaviour
{
	[SerializeField] Color[] colors = new Color[2];
	[SerializeField] GameObject splatterPrefab;
	[SerializeField] Sprite[] splatterSprites;


	public void AddSplatter(Transform obstacle, Vector3 pos, Color color)
	{
		GameObject splatter = Instantiate(
								  splatterPrefab,
								  pos,
								  Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-320f, 320f))),
								  obstacle
							  );

		SpriteRenderer sr = splatter.GetComponent<SpriteRenderer>();
		sr.color = color;
		sr.sprite = splatterSprites[Random.Range(0, splatterSprites.Length)];
	}
}
