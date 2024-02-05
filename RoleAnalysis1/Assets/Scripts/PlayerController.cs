using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float sensitivity;
    [SerializeField] GameObject floor;
    private Camera _camera;
    private float rotX;
    private float rotY;

    private float gravity = 5f;
    private float verticalMomentum;
    private bool grounded;
    Vector3 m_NewForce;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _camera = gameObject.transform.GetChild(0).transform.gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        
        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, -60f, 60f);
        transform.Rotate(0, rotX, 0);
        _camera.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

        m_NewForce = new Vector3(rotX, rotY, 0);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                TeleportFloor(hit);
                Debug.Log("Floor Pos: " + floor.transform.position);
            }
        }
        if (!grounded)
        {
            verticalMomentum -= gravity * Time.deltaTime;
            GetComponent<CharacterController>().Move(transform.up * verticalMomentum * Time.deltaTime);
        }

        
    }

    void TeleportFloor(RaycastHit hit)
    {
        Debug.Log("Hit Point: " + hit.point);
        floor.transform.position = hit.point;
        floor.transform.position = new Vector3(floor.transform.position.x, 0, floor.transform.position.z);
    }
}
