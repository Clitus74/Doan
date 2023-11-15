using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float posStep = 0.01f;
    public float maxPosStep = 0.06f;
    public float rotationStep = 4f;
    public float maxRotationStep = 5f;

    public float smooth = 10f;
    Vector3 swayPos;
    Vector3 swayEulerRot;
    Vector3 lookInput;
    

    void Update()
    {
        lookInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        SwayRot();
        PosAndRot();
    }
    
    void SwayRot()
    {
        Vector3 invertLook = lookInput * -rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);
        swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }
    void PosAndRot()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRot), Time.deltaTime*smooth);
    }
}
