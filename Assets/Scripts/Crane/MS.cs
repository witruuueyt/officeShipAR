using UnityEngine;
using TMPro;

public class MS : MonoBehaviour
{
    public float maxRotationSpeed = 10f;
    public float accelerationRate = 10f;
    public float decelerationRate = 10f;
    private float currentRotationSpeed = 0f;
    public string moveData;
    private string previousMoveData = "0";

    public void RotateRight() 
    {
        moveData = "1";
    }

    public void RotateLeft() 
    {
        moveData = "2";
    }

    public void StopRotation()
    {
        moveData = "0";
    }

    void Update()
    {

        Vector3 rotationDirection = Vector3.zero;
        float targetRotationSpeed;

        if (moveData.Equals("1"))
        {
            rotationDirection = Vector3.right;
            targetRotationSpeed = maxRotationSpeed;
            previousMoveData = "1";

        }
        else if (moveData.Equals("2"))
        {
            rotationDirection = Vector3.left;
            targetRotationSpeed = maxRotationSpeed;
            previousMoveData = "2";

        }
        else 
        {
            targetRotationSpeed = 0f;

            if (previousMoveData.Equals("1"))
            {
                rotationDirection = Vector3.right;

            }

            else if (previousMoveData.Equals("2"))
            {
                rotationDirection = Vector3.left;

            }
        }
        currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed, targetRotationSpeed, accelerationRate * Time.deltaTime);

        transform.Rotate(rotationDirection, currentRotationSpeed * Time.deltaTime);
    }
}
