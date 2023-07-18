using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject slashAnimPrefab;
    [SerializeField] Transform slashAnimationSpawner;
    [SerializeField] Transform weaponCollider;
    [SerializeField] WeaponInfoSO weaponInfoSO;
    Animator myAnimator;
    GameObject slashAnim;
    readonly int ATTACK_HASH = Animator.StringToHash(StringsDefinitions.ATTACK);

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH);
        weaponCollider.gameObject.SetActive(true);

        slashAnim = Instantiate(slashAnimPrefab, slashAnimationSpawner.transform.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public WeaponInfoSO GetWeaponInfo() => weaponInfoSO;

    public void DeactivateWeaponCollider() => weaponCollider.gameObject.SetActive(false);

    public void SwingUpFlipAnim()
    {
        slashAnim.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {
        slashAnim.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void FixedUpdate()
    {
        AdjustWeaponFacingDirection();
    }

    void AdjustWeaponFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.parent.parent.position);

        float left = mousePos.x < playerScreenPoint.x ? -180 : 0;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        transform.parent.rotation = Quaternion.Euler(0, left, angle);
        weaponCollider.transform.rotation = Quaternion.Euler(0, left, 0);
    }
}
