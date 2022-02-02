using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    [SerializeField] private float speed;

    private void FixedUpdate() {
        transform.position += Vector3.down * speed * Time.fixedDeltaTime;
    }
}
