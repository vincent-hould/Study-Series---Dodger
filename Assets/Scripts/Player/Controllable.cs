﻿using UnityEngine;

public class Controllable : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private float move;
    private float spriteWidth;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        Camera cam = Camera.main;
        float minX = cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + (spriteWidth / 2);
        float maxX = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - (spriteWidth / 2);
        float newX = Mathf.Clamp(transform.position.x + (move * speed * Time.fixedDeltaTime), minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
