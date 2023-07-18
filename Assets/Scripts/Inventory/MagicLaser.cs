using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] float laserGrowTime = 0.3f;
    [SerializeField] float laserRange = 8f;
    SpriteRenderer mySpriteRenderer;
    CapsuleCollider2D myCapsuleCollider;

    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {
        LaserFaceMouse();
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f;
        while (mySpriteRenderer.size.x < laserRange)
        {
            timePassed += Time.deltaTime;
            float linearTime = timePassed / laserGrowTime;

            mySpriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearTime), 1f);

            myCapsuleCollider.size = new Vector2(Mathf.Lerp(1f, laserRange, linearTime), myCapsuleCollider.size.y);
            myCapsuleCollider.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearTime)) / 2, myCapsuleCollider.offset.y);
            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.gameObject.GetComponent<Indestructible>())
        {
            laserRange = myCapsuleCollider.size.x;
        }
    }

    void LaserFaceMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }
}
