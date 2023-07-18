using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    [SerializeField] float fadeTime = 0.4f;
    SpriteRenderer mySpriteRenderer;

    void Awake() 
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator SlowFadeRoutine()
    {
        float elapsedTime = 0;
        float startValue = mySpriteRenderer.color.a;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            mySpriteRenderer.color = new Color(
                mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b,
                Mathf.Lerp(startValue, 0f, elapsedTime / fadeTime)
            );
            yield return null;
        }
        Destroy(transform.parent.gameObject);
    }
}
