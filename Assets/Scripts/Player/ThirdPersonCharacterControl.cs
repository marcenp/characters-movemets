using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterControl : MonoBehaviour
{
  public float speed;
  Animator animatorController;

  void Awake()
  {
    animatorController = GetComponent<Animator>();
  }

  void Update()
  {
    Movement();
  }

  void Movement()
  {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    float auxSpeed = vertical < 0.5f ? speed * 0.1f : speed;
    animatorController.SetFloat("speedVertical", vertical * speed);
    animatorController.SetFloat("speedHorizontal", horizontal * speed);
    Vector3 playerMovement = new Vector3(horizontal * 0.75f, 0f, vertical) * auxSpeed * Time.deltaTime;
    transform.Translate(playerMovement, Space.Self);
  }
}