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
        //�ִϸ����� �Ķ���� ������Ʈ
        animator.SetFloat("moveX", horizontal);
        animator.SetFloat("moveY", vertical);

        //�����̴� ������ �Ǵ�
        animator.SetBool("isMoving", (horizontal != 0 || vertical != 0));
        animator.SetBool("moveX0", horizontal == 0);
        animator.SetBool("moveY0", vertical == 0);

    }
}
