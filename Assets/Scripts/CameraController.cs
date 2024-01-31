using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public float sensitivityX;                 // This variable stores the sensitivity within the x-axis
    [SerializeField] public float sensitivityY;                 // This variable stores the sensitivity within the y-axis
    public Transform direction;
    [SerializeField] public float rotationX;                    // This variable stores the angle of the camera within the x-axis
    [SerializeField] public float rotationY;                    // This variable stores the angle of the camera within the y-axis

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;             // The float variable mouseX stores its values that it takes from the mouse movement and multiplies it with the sensitivity within the x-axis
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;             // The float variable mouseX stores its values that it takes from the mouse movement and multiplies it with the sensitivity within the x-axis

        rotationY += mouseX;                                                                    
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);                                          // Makes sure that the camera can move within the x-asix in all angles

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        direction.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
