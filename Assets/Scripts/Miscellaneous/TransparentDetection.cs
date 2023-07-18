using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float transparencyAmount = 0.6f;
    [SerializeField] float fadeTime = 0.4f;
    IEnumerator coroutine;
    SpriteRenderer mySpriteRenderer;
    Tilemap myTilemap;
    bool isTilemap;

    private void Awake() 
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myTilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player") && gameObject.activeSelf)
        {
            if (mySpriteRenderer)
            {
                CheckCurrentRoutine(FadeRoutine(mySpriteRenderer.color.a, transparencyAmount, fadeTime, mySpriteRenderer));
            }
            else if (myTilemap)
            {
                CheckCurrentRoutine(FadeRoutine(myTilemap.color.a, transparencyAmount, fadeTime, myTilemap));
            }
        }
    }

    void CheckCurrentRoutine(IEnumerator routine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = routine;
        StartCoroutine(coroutine);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player") && gameObject.activeSelf)
        {
            if (mySpriteRenderer)
            {
                CheckCurrentRoutine(FadeRoutine(mySpriteRenderer.color.a, 1, fadeTime * 2, mySpriteRenderer));
            }
            else if (myTilemap)
            {
                CheckCurrentRoutine(FadeRoutine(myTilemap.color.a, 1, fadeTime * 2, myTilemap));
            }
        }
    }

    IEnumerator FadeRoutine(float startValue, float targetValue, float routineTime, SpriteRenderer sr)
    {
        float elapsedTime = 0;
        while (elapsedTime < routineTime)
        {
            elapsedTime += Time.deltaTime;
            sr.color = new Color(
                sr.color.r, sr.color.g, sr.color.b,
                Mathf.Lerp(startValue, targetValue, elapsedTime / routineTime)
            );
            yield return null;
        }
    }

    IEnumerator FadeRoutine(float startValue, float targetValue, float routineTime, Tilemap tm)
    {
        float elapsedTime = 0;
        while (elapsedTime < routineTime)
        {
            elapsedTime += Time.deltaTime;
            tm.color = new Color(
                tm.color.r, tm.color.g, tm.color.b,
                Mathf.Lerp(startValue, targetValue, elapsedTime / routineTime)
            );
            yield return null;
        }
    }
}
