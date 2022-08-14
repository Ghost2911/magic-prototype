using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public float speed = 1f;
    public float sensivity = 1f;
    public bool useCamera = false;
    public Transform shield;

    private Camera _camera;
    private CharacterController _characterController;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            shield.gameObject.SetActive(true);
        if (Input.GetKeyUp(KeyCode.Space))
            shield.gameObject.SetActive(false);


        if (Input.GetKey(KeyCode.Mouse1) && useCamera)
        {
            float newRotationX = _camera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensivity;
            float newRotationY = _camera.transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensivity;
            _camera.transform.localEulerAngles = new Vector3(newRotationY, 0f, 0f);
            transform.Rotate(new Vector3(0f, newRotationX, 0f), Space.World);
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        Move(movement);
    }

    private void Move(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            Vector3 horizontal = Input.GetAxis("Horizontal")* transform.right;
            Vector3 vertical = Input.GetAxis("Vertical") * transform.forward;

            _characterController.SimpleMove((horizontal+vertical).normalized*speed);
        }
    }
}
