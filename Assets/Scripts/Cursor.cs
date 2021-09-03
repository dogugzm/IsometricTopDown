using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cursor : MonoBehaviour
{
    public static Cursor instance;
    [HideInInspector] public Vector3 pointToLook;

    private void Awake()
    {
        instance = this;

    }

    private void Update()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, Player.closestPosition.y, 0));
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            pointToLook = cameraRay.GetPoint(rayLength);
            transform.position = pointToLook;
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
        }
    }
}
