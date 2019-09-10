using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float moveSpeed = 6f;
    public float rotateSpeed = 10f;

    Rigidbody rb;
    Vector3 moveDirection;
    float inputAmount;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        moveDirection = Vector3.zero;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 combinedInput = new Vector3(horizontal, 0, vertical);

        moveDirection = new Vector3(combinedInput.normalized.x, 0, combinedInput.normalized.z);

        float inputMagnitude = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        inputAmount = Mathf.Clamp01(inputMagnitude);

        if (moveDirection != Vector3.zero) {
            Quaternion rot = Quaternion.LookRotation(moveDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * inputAmount * rotateSpeed);
            transform.rotation = targetRotation;
        }
    }

    private void FixedUpdate() {
        rb.velocity = (moveDirection * moveSpeed * inputAmount);
    }
}