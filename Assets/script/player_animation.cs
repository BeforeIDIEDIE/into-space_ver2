using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateAnimation(float horizontal, float vertical)
    {
        //애니메이터 파라미터 업데이트
        animator.SetFloat("moveX", horizontal);
        animator.SetFloat("moveY", vertical);

        //움직이는 중인지 판단
        animator.SetBool("isMoving", (horizontal != 0 || vertical != 0));
        animator.SetBool("moveX0", horizontal == 0);
        animator.SetBool("moveY0", vertical == 0);

    }
}
