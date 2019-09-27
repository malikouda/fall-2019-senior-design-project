using UnityEngine;

public class Character : MonoBehaviour {

    public float moveSpeed = 6f;
    public float rotateSpeed = 10f;

    Rigidbody rb;
    Vector3 moveDirection;
    float inputAmount;
    bool touching = false;
    GameObject interactable;

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

        if (touching && Input.GetKeyDown(KeyCode.E) && interactable) {
            interactable.GetComponent<Interactable>().Activate(this.gameObject);
        }


    }

    private void FixedUpdate() {
        rb.velocity = (moveDirection * moveSpeed * inputAmount);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Interactable") {
            touching = true;
            interactable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Interactable") {
            touching = false;
            interactable = null;
        }
    }
}