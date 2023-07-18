using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Zombie, Ghost, ZombieFodao*/ //   Entidades
//variáveis ->                      componentes
// funções ->                       sistemas
public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float restoreDefaultMaterialTime = 0.2f;
    public float RestoreDefaultMaterialTime => restoreDefaultMaterialTime;
    private Material defaultMaterial;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = mySpriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        mySpriteRenderer.material = whiteFlashMaterial;
        yield return new WaitForSeconds(restoreDefaultMaterialTime);
        mySpriteRenderer.material = defaultMaterial;
    }
}
