using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Player Setup")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int moveSpeed;

    private bool isMove;

    protected override void Update()
    {
        base.Update();
        Joystick();

        if (isMove != true && target != null)
        {
            RotateToTarget();
            Attack();
        }
    }

    private void Joystick()
    {
        if (Input.GetMouseButton(0))
        {
            rb.velocity = JoystickController.direct * moveSpeed + rb.velocity.y * Vector3.up;
            if (JoystickController.direct != Vector3.zero)
            {
                skin.forward = JoystickController.direct;
                ChangeAnim(CacheString.ANIM_RUN);
                isMove = true;
                if (target != null)
                {
                    StopCoroutine(nameof(DelayShoot));
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            rb.velocity = Vector3.zero;
            ChangeAnim(CacheString.ANIM_IDLE);
            isMove = false;
        }
    }
}
