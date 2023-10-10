using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        camera.localEulerAngles = new Vector3(yAxis.Value, camera.localEulerAngles.y, camera.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }

}
