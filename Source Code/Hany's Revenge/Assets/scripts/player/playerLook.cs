using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    public float mouseSens = 100f;
    public Transform player;
    float xRotation = 0f;
    public static float mouseX;
    public static float mouseY;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
         mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
         mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        if (HUD.canMove)
        {
            player.Rotate(Vector3.up * mouseX);
        }
        

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        if (HUD.canMove)
        {
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
    }
}
