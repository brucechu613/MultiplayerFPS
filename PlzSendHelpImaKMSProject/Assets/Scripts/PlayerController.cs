using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float lookSensitivity = 5f;

	[SerializeField]
	private float thrusterForce = 1300f;

	[SerializeField]
	private float thrusterFuelBurnSpeed = 1f;
	[SerializeField]
	private float thrusterFuelRegenSpeed = 1f;
	private float thrusterFuelAmount = 1f;

	public float GetThrusterFuelAmount ()
	{
		return thrusterFuelAmount;
	}

	[SerializeField]
	private LayerMask environmentMask;

	[Header("Spring settings:")]
	[SerializeField]
	private float jointSpring = 20f;
	[SerializeField]
	private float jointMaxForce = 40f;

	// Component caching
	private PlayerMotor motor;
	private ConfigurableJoint joint;
	private Animator animator;

	void Start ()
	{
		motor = GetComponent<PlayerMotor>();
		joint = GetComponent<ConfigurableJoint>();
		animator = GetComponent<Animator>();

		SetJointSettings(jointSpring);
	}

	void FixedUpdate ()
	{
		if (PauseMenu.IsOn)
		{
			if (Cursor.lockState != CursorLockMode.None)
				Cursor.lockState = CursorLockMode.None;

			motor.Move(Vector3.zero);
			motor.Rotate(Vector3.zero);
			motor.RotateCamera(0f);

			return;
		}

		if (Cursor.lockState != CursorLockMode.Locked)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		//Setting target position for spring
		//This makes the physics act right when it comes to
		//applying gravity when flying over objects
		RaycastHit _hit;
		if (Physics.Raycast (transform.position, Vector3.down, out _hit, 100f, environmentMask))
		{
			joint.targetPosition = new Vector3(0f, -_hit.point.y, 0f);
		} else
		{
			joint.targetPosition = new Vector3(0f, 0f, 0f);
		}

		//Calculate movement velocity as a 3D vector
		float _xMov = Input.GetAxis("Horizontal");
		float _zMov = Input.GetAxis("Vertical");

		Vector3 _movHorizontal = transform.right * _xMov;
		Vector3 _movVertical = transform.forward * _zMov;

		// Final movement vector
		Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

		// Animate movement
		//animator.SetFloat("ForwardVelocity", _zMov);

		//Apply movement

        /*if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            _velocity += _zMov * speed * 0.3f * transform.forward;
        }*/
		motor.Move(_velocity);

		//Calculate rotation as a 3D vector (turning around)
		float _yRot = Input.GetAxisRaw("Mouse X");

		Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

		//Apply rotation
		motor.Rotate(_rotation);

		//Calculate camera rotation as a 3D vector (turning around)
		float _xRot = Input.GetAxisRaw("Mouse Y");

		float _cameraRotationX = _xRot * lookSensitivity;

		//Apply camera rotation
		motor.RotateCamera(_cameraRotationX);

		// Calculate the thrusterforce based on player input
		Vector3 _thrusterForce = Vector3.zero;
		if (Input.GetButton ("Jump") && thrusterFuelAmount > 0f)
		{
			thrusterFuelAmount -= thrusterFuelBurnSpeed * Time.deltaTime;

			if (thrusterFuelAmount >= 0.01f)
			{
				_thrusterForce = Vector3.up * thrusterForce;
				SetJointSettings(0f);
			}
		} else
		{
			thrusterFuelAmount += thrusterFuelRegenSpeed * Time.deltaTime;
			SetJointSettings(jointSpring);
		}

		thrusterFuelAmount = Mathf.Clamp(thrusterFuelAmount, 0f, 1f);

		// Apply the thruster force
		motor.ApplyThruster(_thrusterForce);

	}

	private void SetJointSettings (float _jointSpring)
	{
		joint.yDrive = new JointDrive {
			positionSpring = _jointSpring,
			maximumForce = jointMaxForce
		};
	}

}
