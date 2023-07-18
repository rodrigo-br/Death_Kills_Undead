using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] float dashSpeed = 4f;
    [SerializeField] float dashTime = 0.1f;
    [SerializeField] float dashCooldown = 3f;
    [SerializeField] TrailRenderer myTrailRenderer;
    public float defaultSpeed { get; private set; } = 1f;
    bool isDashing = false;

    public void PerformDash()
    {
        if (!isDashing)
        {
            isDashing = true;
            StartCoroutine(DashRoutine());
        }
    }

    IEnumerator DashRoutine()
    {
        defaultSpeed *= dashSpeed;
        myTrailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        myTrailRenderer.emitting = false;
        defaultSpeed = 1;
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }
}
