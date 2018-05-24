using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour {

    public float moveVolume = 1.0f;
    public float moveSpeed = 0.2f;

    private Vector3 firstPosition;

    private void Awake()
    {
        firstPosition = transform.position;
    }

    public void MoveLift()
    {
        StartCoroutine(Move());
    }

    // 上下に動くコルーチン
    IEnumerator Move()
    {
        float lerpVolume = 0.0f;
        bool moveDirection = true;
        Vector3 targetPosition = firstPosition;
        targetPosition.y += moveVolume;

        while (true)
        {
            if (moveDirection)
                lerpVolume += Time.deltaTime * moveSpeed;
            else
                lerpVolume -= Time.deltaTime * moveSpeed;

            if (lerpVolume >= 1.0f)
            {
                moveDirection = false;
                lerpVolume = 1.0f;
            }
            else if (lerpVolume <= 0.0f)
            {
                moveDirection = true;
                lerpVolume = 0.0f;
            }

            transform.position = Vector3.Lerp(firstPosition, targetPosition, lerpVolume);

            yield return null;
        }
    }
}
