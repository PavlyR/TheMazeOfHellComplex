using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour
{
    public AnimationCurve openDoor = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1, 0, 0), new Keyframe(0.8f, 1, 0, 0), new Keyframe(1, 0, 0, 0) });
    public float openDoorSpeed = 2.0f;
    public float openDoorAngle = 90.0f;

    bool open = false;
    bool enter = false;

    float defaultRotationAngle;
    float currentRotationAngle;
    float openTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;

        GetComponent<Collider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openDoorSpeed * openDoor.Evaluate(openTime);
        }

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? openDoorAngle : 0), openTime), transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.E) && enter)
        {
            open = !open;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = false;
        }
    }
}
