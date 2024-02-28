using UnityEngine;

public class TL : MonoBehaviour
{
    public float maxRotationSpeed = 10f; // �����ת�ٶ�
    public float accelerationRate = 10f; // ���ٶ�
    public float decelerationRate = 10f; // ���ٶ�
    [SerializeField]
    private float currentRotationSpeed = 0f; // ��ǰ��ת�ٶ�

    private string previousMoveData = "0"; // ��һ���ƶ�����
    public string moveData; // �ƶ�����
    private bool shouldStop = false; // �Ƿ�Ӧ��ֹͣ��ת

    // ��ת�Ƕȷ�Χ
    public float targetAngleMin = -50f; // ��С�Ƕ�
    public float targetAngleMax = 20f; // ���Ƕ�

    // ������ת
    public void RotateRight()
    {
        moveData = "1";

        // �����Ҫ���¿�ʼ��ת���������ٶ�
        if (shouldStop == true)
        {
            currentRotationSpeed = maxRotationSpeed; // �����ٶ�Ϊ�����ת�ٶ�
        }
        shouldStop = false; // ����������תʱ���� shouldStop

    }

    // ������ת
    public void RotateLeft()
    {
        moveData = "2";

        // �����Ҫ���¿�ʼ��ת���������ٶ�
        if (shouldStop == true)
        {
            currentRotationSpeed = maxRotationSpeed; // �����ٶ�Ϊ�����ת�ٶ�
        }
        shouldStop = false; // ����������תʱ���� shouldStop

    }

    // ֹͣ��ת
    public void StopRotation()
    {
        moveData = "0";
    }

    // ���·�����ÿִ֡��
    void Update()
    {
        Vector3 rotationDirection = Vector3.zero;
        float targetRotationSpeed;

        // �����ƶ�����ȷ����ת�����Ŀ����ת�ٶ�
        if (moveData.Equals("1"))
        {
            rotationDirection = Vector3.left;
            targetRotationSpeed = maxRotationSpeed;
            previousMoveData = "1";

        }
        else if (moveData.Equals("2"))
        {
            rotationDirection = Vector3.right;
            targetRotationSpeed = maxRotationSpeed;
            previousMoveData = "2";
        }
        else
        {
            targetRotationSpeed = 0f;

            if (previousMoveData.Equals("1"))
            {
                rotationDirection = Vector3.left;
            }
            else if (previousMoveData.Equals("2"))
            {
                rotationDirection = Vector3.right;
            }
        }

        // ���ݼ��ٶȵ�����ǰ��ת�ٶ�
        currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed, targetRotationSpeed, accelerationRate * Time.deltaTime);

        // ���ݵ�ǰ��ת�ٶȺͷ��������ת
        transform.Rotate(rotationDirection, currentRotationSpeed * Time.deltaTime);

        Debug.LogWarning(GetEffectiveRotationX());

        // ����Ƿ񳬳�ָ���Ƕȷ�Χ���������ֹͣ��ת
        float effectiveRotationX = GetEffectiveRotationX();
        if (!(effectiveRotationX >= targetAngleMin && effectiveRotationX <= targetAngleMax) && !shouldStop)
        {
            currentRotationSpeed = 0f;
            moveData = "0"; // ֹͣ��ת
            shouldStop = true; // ���� shouldStop Ϊ true����ʾ�Ѿ�ֹͣ��ת
        }
    }

    // ��ȡ��Ч�� x ����ת�Ƕ�
    float GetEffectiveRotationX()
    {
        float effectiveRotation = transform.localEulerAngles.x;
        if (transform.localRotation.eulerAngles.x > 180)
        {
            effectiveRotation -= 360; // ������180�ȵĽǶ�ת��Ϊ����
        }
        return effectiveRotation;
    }
}
