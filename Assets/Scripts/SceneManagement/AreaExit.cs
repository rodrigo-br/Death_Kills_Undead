using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaExit : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] string sceneTransitionName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIFade.Instance.FadeToValue(1);
            StartCoroutine(NewSceneRoutine());
        }
    }

    IEnumerator NewSceneRoutine()
    {
        yield return new WaitUntil(() => UIFade.Instance.endFading);
        ScenesManagment.SetTransitionName(sceneTransitionName);
        ScenesManagment.LoadSceneByName(sceneToLoad);
    }
}
