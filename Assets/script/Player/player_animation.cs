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

    public void UpdateAnimation_walk(float horizontal, float vertical)
    {
        //�ִϸ����� �Ķ���� ������Ʈ
        animator.SetFloat("moveX", horizontal);
        animator.SetFloat("moveY", vertical);

        //�����̴� ������ �Ǵ�
        animator.SetBool("isMoving", (horizontal != 0 || vertical != 0));
        animator.SetBool("moveX0", horizontal == 0);
        animator.SetBool("moveY0", vertical == 0);
    }

    public void UpdateAnimation_action()//��ȣ�ۿ����
    {

        animator.SetBool("Src", GameManager.Instance.IsInteractionActive(InteractionType.Src));
        animator.SetBool("Heal", GameManager.Instance.IsInteractionActive(InteractionType.Heal));
        animator.SetBool("Steer", GameManager.Instance.IsInteractionActive(InteractionType.Steer));
        animator.SetBool("Electric", GameManager.Instance.IsInteractionActive(InteractionType.Electric));
    }

    public void TriggerDeathAnimation()
    {
        animator.SetTrigger("isDead"); 
    }
}
