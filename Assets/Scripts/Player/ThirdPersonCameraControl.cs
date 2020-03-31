using UnityEngine;
using System.Collections;

public class ThirdPersonCameraControl : MonoBehaviour
{
  public float rotationSpeed = 1;
  public Transform targetCamera, player;
  Transform obstruction;
  float mouseX, mouseY;
  float zoomSpeed = 2f;

  void Start()
  {
    obstruction = targetCamera;
    Cursor.lockState = CursorLockMode.Locked;
  }

  private void LateUpdate()
  {
    CamControl();
    ViewObstructed();
  }

  void CamControl()
  {
    mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
    mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
    mouseY = Mathf.Clamp(mouseY, -20, 60);

    transform.LookAt(targetCamera);

    if (Input.GetKey(KeyCode.LeftShift))
    {
      targetCamera.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
    else
    {
      targetCamera.rotation = Quaternion.Euler(mouseY, mouseX, 0);
      player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
  }

  void ViewObstructed()
  {
    RaycastHit hit;
    if (!Physics.Raycast(transform.position, targetCamera.position - transform.position, out hit, 4.5f)) return;

    if (hit.collider.gameObject.tag != "Player")
    {
      obstruction = hit.transform;
      obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
      if (Vector3.Distance(obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, targetCamera.position) >= 1.5f)
        transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
    }

  }

  IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration)
  {
    for (float t = 0f; t < duration; t += Time.deltaTime)
    {
      transform.position = Vector3.Lerp(pos1, pos2, t / duration);
      yield return 0;
    }
    transform.position = pos2;
  }
}