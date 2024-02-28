using UnityEngine;

public class TT : MonoBehaviour
{
    public float maxTranslationSpeed = 10f; // ����ƶ��ٶ�
    public float accelerationRate = 10f; // ���ٶ�
    public float decelerationRate = 10f; // ���ٶ�
    public float minXPosition = -5.87f; // ��С x ��λ��
    public float maxXPosition = -0.37f; // ��� x ��λ��
    private bool shouldStop = false; // �Ƿ�Ӧ��ֹͣ�ƶ�
    private string previousMoveData = "0"; // ��һ���ƶ�����
    public string moveData;

    [SerializeField]
    private float currentTranslationSpeed = 0f; // ��ǰ�ƶ��ٶ�

    // ��ǰ�ƶ�
    public void MoveForward()
    {
        moveData = "1";
        shouldStop = false; // ���¿�ʼ�ƶ�ʱ���� shouldStop
    }

    // ����ƶ�
    public void MoveBackward()
    {
        moveData = "2";
        shouldStop = false; // ���¿�ʼ�ƶ�ʱ���� shouldStop
    }

    // ֹͣ�ƶ�
    public void StopMovement()
    {
        moveData = "0";
    }

    // ���·�����ÿִ֡��
    void Update()
    {
        Vector3 translationDirection = Vector3.zero;
        float targetTranslationSpeed;

        // �����ƶ�����ȷ���ƶ������Ŀ���ƶ��ٶ�
        if (moveData.Equals("1"))
        {
            translationDirection = Vector3.forward;
            targetTranslationSpeed = maxTranslationSpeed;
            previousMoveData = "1";
        }
        else if (moveData.Equals("2"))
        {
            translationDirection = Vector3.back;
            targetTranslationSpeed = maxTranslationSpeed;
            previousMoveData = "2";
        }
        else
        {
            targetTranslationSpeed = 0f;

            if (previousMoveData.Equals("1"))
            {
                translationDirection = Vector3.forward;
            }
            else if (previousMoveData.Equals("2"))
            {
                translationDirection = Vector3.back;
            }
        }

        // ���ݼ��ٶȵ�����ǰ�ƶ��ٶ�
        currentTranslationSpeed = Mathf.MoveTowards(currentTranslationSpeed, targetTranslationSpeed, accelerationRate * Time.deltaTime);

        // ���ݵ�ǰ�ƶ��ٶȺͷ�������ƶ�
        transform.Translate(translationDirection * currentTranslationSpeed * Time.deltaTime);

        // ����Ƿ񳬳�ָ��λ�÷�Χ���������ֹͣ�ƶ����̶��ڷ�Χ�߽���
        float effectiveXPosition = transform.localPosition.x;
        if (effectiveXPosition > maxXPosition)
        {
            transform.localPosition = new Vector3(maxXPosition, transform.localPosition.y, transform.localPosition.z);
            currentTranslationSpeed = 0f;
            moveData = "0"; // ֹͣ�ƶ�
            shouldStop = true; // ���� shouldStop Ϊ true����ʾ�Ѿ�ֹͣ�ƶ�
        }
        else if (effectiveXPosition < minXPosition)
        {
            transform.localPosition = new Vector3(minXPosition, transform.localPosition.y, transform.localPosition.z);
            currentTranslationSpeed = 0f;
            moveData = "0"; // ֹͣ�ƶ�
            shouldStop = true; // ���� shouldStop Ϊ true����ʾ�Ѿ�ֹͣ�ƶ�
        }
    }
}
