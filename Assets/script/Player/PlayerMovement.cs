using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private GameObject player;

    [Header("player setting")]
    [SerializeField] private float speed = 3f;

    private PlayerAnimation playerAnimation;


    private void Start()
    {
        player = this.gameObject;
        player.transform.position = new Vector3(-6,3,5);
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space)&&GameManager.Instance.IsPlayerInteraction())//�̺κ� ���� �ʿ� -> �Ƹ� is near + �����̽��� ������?
        {
            playerAnimation.UpdateAnimation_action();
            return;
        }
        Vector3 moveVelocity = Vector3.zero;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveVelocity = new Vector3(horizontal, vertical, 0f).normalized;
        transform.position += moveVelocity * speed * Time.deltaTime;
        playerAnimation.UpdateAnimation_walk(horizontal, vertical);
    }
}
