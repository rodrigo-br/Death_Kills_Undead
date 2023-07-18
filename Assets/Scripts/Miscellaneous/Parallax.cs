using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parallaxOffset = -0.15f;
    Camera cam;
    Vector2 startPosition;
    Vector2 travel => (Vector2)cam.transform.position - startPosition;

    void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate() 
    {
        transform.position = startPosition + travel * parallaxOffset;
    }
}
