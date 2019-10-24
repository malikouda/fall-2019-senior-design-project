using UnityEngine;
using System.Collections;
using com.gargore.InputManager;

public class ExampleVeryBasic: MonoBehaviour {
	
	public Rigidbody bodyRigidbody = null;
	public GameObject cameraGameObject = null;
	public Rigidbody shootRigidbody = null;
	public float walkForce = 10f;
	public float jumpForce = 20f;
	
	private Quaternion bodyRigidbodyRotation = Quaternion.identity;
	private Quaternion cameraGameObjectRotation = Quaternion.identity;
	private float valueLookX = 0f;
	private float multLookX = 5f;
	private float valueLookY = 0f;
	private float multLookY = 5f;
	
	void Start() {
		// define the axes and buttons interfaces and configuration
		GInput.registerInterface("Mouse X", new GInputInterfaceExample());
		GInput.configInterface("Mouse X", "source=Mouse X;debug=true");
		GInput.registerInterface("Mouse Y", new GInputInterfaceExample());
		GInput.configInterface("Mouse Y", "source=Mouse Y;debug=true");
		GInput.registerInterface("Horizontal", new GInputInterfaceExample());
		GInput.configInterface("Horizontal", "source=Horizontal;debug=true");
		GInput.registerInterface("Vertical", new GInputInterfaceExample());
		GInput.configInterface("Vertical", "source=Vertical;debug=true");
		GInput.registerInterface("Jump", new GInputInterfaceExample());
		GInput.configInterface("Jump", "source=Jump;debug=true");
	}

	void Update() {
		// accumulate the rotation angle
		valueLookX += GInput.GetAxis("Mouse X") * multLookX;
		valueLookY += -GInput.GetAxis("Mouse Y") * multLookY;
		
		// apply angles
		bodyRigidbodyRotation.eulerAngles = new Vector3(0f, valueLookX, 0f);
		bodyRigidbody.transform.rotation = bodyRigidbodyRotation;
		cameraGameObjectRotation.eulerAngles = new Vector3(valueLookY, 0f, 0f);
		cameraGameObject.transform.localRotation = cameraGameObjectRotation;
		
		// apply forces
		bodyRigidbody.AddForce(bodyRigidbody.transform.forward * GInput.GetAxis("Vertical") * walkForce + bodyRigidbody.transform.right * GInput.GetAxis("Horizontal") * walkForce);
		if (GInput.GetButton("Jump")) bodyRigidbody.AddForce(jumpForce * Vector3.up);
	}
}
