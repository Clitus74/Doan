using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : MonoBehaviour
{
    
    [SerializeField] Transform cameraHolder;
    [SerializeField] float mouseSensitivity;
    float verticalLookRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Look();

    }
    
    public void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;

    }
}
