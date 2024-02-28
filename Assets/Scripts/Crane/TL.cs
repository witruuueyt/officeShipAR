using UnityEngine; 

public class TL : MonoBehaviour 
{
    public float maxRotationSpeed = 10f; 
    public float accelerationRate = 10f; 
    public float decelerationRate = 10f; 
    public string moveData; 
    public float targetAngleMin = -50f; 
    public float targetAngleMax = 20f; 
    private bool shouldStop = false; 
    private string previousMoveData = "0"; 
    [SerializeField]
    private float currentRotationSpeed = 0f; 

   
    public void Rotate1()
    {
        moveData = "1";
        shouldStop = false; 
    }

    public void Rotate2()
    {
        moveData = "2";
        shouldStop = false;
    }

    public void StopRotation()
    {
        moveData = "0";
    }

    void Update()
    {
        Vector3 rotationDirection = Vector3.zero; // ��ת����������ʼ��Ϊ������
        float targetRotationSpeed; // Ŀ����ת�ٶ�

        if (moveData.Equals("1"))
        {
            rotationDirection = Vector3.left; 
            targetRotationSpeed = maxRotationSpeed; // ����Ŀ����ת�ٶ�Ϊ�����ת�ٶ�
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
            targetRotationSpeed = 0f; // û���ƶ�����ʱ��Ŀ����ת�ٶ�Ϊ��

            // ������һ���ƶ�����ȷ����ת����
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

        //Debug.LogWarning(GetEffectiveRotationX()); 

        // ����Ƿ񳬳�ָ���Ƕȷ�Χ���������ֹͣ��ת���̶��ڷ�Χ�߽���
        float effectiveRotationX = GetEffectiveRotationX();
        if (effectiveRotationX > targetAngleMax)
        {
            transform.localEulerAngles = new Vector3(targetAngleMax, transform.localEulerAngles.y, transform.localEulerAngles.z);
            currentRotationSpeed = 0f;
            moveData = "0";
            shouldStop = true; 
        }
        else if (effectiveRotationX < targetAngleMin)
        {
            transform.localEulerAngles = new Vector3(targetAngleMin, transform.localEulerAngles.y, transform.localEulerAngles.z);
            currentRotationSpeed = 0f;
            moveData = "0"; 
            shouldStop = true;
        }
    }

    // ��ȡ��Ч�� x ����ת�Ƕȷ���
    float GetEffectiveRotationX()
    {
        float effectiveRotation = transform.localEulerAngles.x; // ��ȡ x ����ת�Ƕ�
        if (transform.localRotation.eulerAngles.x > 180) // ����Ƕȴ���180��
        {
            effectiveRotation -= 360; // ������180�ȵĽǶ�ת��Ϊ����
        }
        return effectiveRotation; // ������Ч�� x ����ת�Ƕ�
    }
}



