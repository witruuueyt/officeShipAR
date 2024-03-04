using UnityEngine; 

public class TL : MonoBehaviour 
{
    public float maxRotationSpeed = 10f; 
    public float accelerationRate = 10f; 
    public float decelerationRate = 10f; 
    public string moveData; 
    public float targetAngleMin = -50f; 
    public float targetAngleMax = 20f; 
    private string previousMoveData = "0"; 
    [SerializeField]
    private float currentRotationSpeed = 0f; 

   
    public void Rotate1()
    {
        moveData = "1";
    }

    public void Rotate2()
    {
        moveData = "2";
    }

    public void StopRotation()
    {
        moveData = "0";
    }

    void Update()
    {
        Vector3 rotationDirection = Vector3.zero; // 旋转方向向量初始化为零向量
        float targetRotationSpeed; // 目标旋转速度

        if (moveData.Equals("1"))
        {
            rotationDirection = Vector3.left; 
            targetRotationSpeed = maxRotationSpeed; // 设置目标旋转速度为最大旋转速度
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
            targetRotationSpeed = 0f; // 没有移动数据时，目标旋转速度为零

            // 根据上一个移动数据确定旋转方向
            if (previousMoveData.Equals("1"))
            {
                rotationDirection = Vector3.left; 
            }
            else if (previousMoveData.Equals("2"))
            {
                rotationDirection = Vector3.right; 
            }
        }

        // 根据加速度调整当前旋转速度
        currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed, targetRotationSpeed, accelerationRate * Time.deltaTime);

        // 根据当前旋转速度和方向进行旋转
        transform.Rotate(rotationDirection, currentRotationSpeed * Time.deltaTime);

        //Debug.LogWarning(GetEffectiveRotationX()); 

        // 检查是否超出指定角度范围，如果是则停止旋转并固定在范围边界上
        float effectiveRotationX = GetEffectiveRotationX();
        if (effectiveRotationX > targetAngleMax)
        {
            transform.localEulerAngles = new Vector3(targetAngleMax, transform.localEulerAngles.y, transform.localEulerAngles.z);
            currentRotationSpeed = 0f;
            moveData = "0";
        }
        else if (effectiveRotationX < targetAngleMin)
        {
            transform.localEulerAngles = new Vector3(targetAngleMin, transform.localEulerAngles.y, transform.localEulerAngles.z);
            currentRotationSpeed = 0f;
            moveData = "0"; 
        }
    }

    // 获取有效的 x 轴旋转角度方法
    float GetEffectiveRotationX()
    {
        float effectiveRotation = transform.localEulerAngles.x; // 获取 x 轴旋转角度
        if (transform.localRotation.eulerAngles.x > 180) // 如果角度大于180度
        {
            effectiveRotation -= 360; // 将大于180度的角度转换为负数
        }
        return effectiveRotation; // 返回有效的 x 轴旋转角度
    }
}



