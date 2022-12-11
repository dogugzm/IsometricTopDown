using UnityEngine;

using UnityEngine;

public class IsometricCharacterMovement : MonoBehaviour
{
    // The speed at which the character moves
    public float movementSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        // Get input from the horizontal and vertical axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the direction the character should move in
        Vector3 movementDirection = new Vector3(horizontalInput, verticalInput, 0).normalized;

        // Move the character in the calculated direction
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
    }
}
