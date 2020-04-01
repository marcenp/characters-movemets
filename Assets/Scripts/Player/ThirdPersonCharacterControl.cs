using UnityEngine;

public class ThirdPersonCharacterControl : MonoBehaviour, IMovements
{
  public float speed = 6f;
  private bool isGrounded;
  private Animator animatorController;
  private Rigidbody rigidBody;
  void Awake()
  {
    animatorController = GetComponent<Animator>();
    rigidBody = GetComponent<Rigidbody>();
  }
  private void Update()
  {
    VerticalHorizontalMove();
  }

  public void VerticalHorizontalMove()
  {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    float auxSpeed = vertical < 0.5f ? speed * 0.1f : speed;
    animatorController.SetFloat("speedVertical", vertical * speed);
    animatorController.SetFloat("speedHorizontal", horizontal * speed);
    Vector3 playerMovement = new Vector3(horizontal * 0.75f, 0f, vertical) * auxSpeed * Time.deltaTime;
    transform.Translate(playerMovement, Space.Self);
  }

  public void JumpMove() { }

  void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag == "Ground") isGrounded = true;
  }

  void OnCollisionExit(Collision other)
  {
    if (other.gameObject.tag == "Ground") isGrounded = false;
  }

}