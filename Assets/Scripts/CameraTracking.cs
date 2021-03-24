using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
	public Transform target;

	private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
	{
        Vector3 pos = offset + target.position;
        pos.z = -10;
        transform.position = pos;
	}
}
