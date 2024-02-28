using UnityEngine;

public class TL : MonoBehaviour
{
    public float maxRotationSpeed = 10f; // 最大旋转速度
    public float accelerationRate = 10f; // 加速度
    public float decelerationRate = 10f; // 减速度
    [SerializeField]
    private float currentRotationSpeed = 0f; // 当前旋转速度

    private string previousMoveData = "0"; // 上一个移动数据
    public string moveData; // 移动数据
    private bool shouldStop = false; // 是否应该停止旋转

    // 旋转角度范围
    public float targetAngleMin = -50f; // 最小角度
    public float targetAngleMax = 20f; // 最大角度

    // 向右旋转
    public void RotateRight()
    {
        moveData = "1";

        // 如果需要重新开始旋转，则设置速度
        if (shouldStop == true)
        {
            currentRotationSpeed = maxRotationSpeed; // 设置速度为最大旋转速度
        }
        shouldStop = false; // 重新启动旋转时重置 shouldStop

    }

    // 向左旋转
    public void RotateLeft()
    {
        moveData = "2";

        // 如果需要重新开始旋转，则设置速度
        if (shouldStop == true)
        {
            currentRotationSpeed = maxRotationSpeed; // 设置速度为最大旋转速度
        }
        shouldStop = false; // 重新启动旋转时重置 shouldStop

    }

    // 停止旋转
    public void StopRotation()
    {
        moveData = "0";
    }

    // 更新方法，每帧执行
    void Update()
    {
        Vector3 rotationDirection = Vector3.zero;
        float targetRotationSpeed;

        // 根据移动数据确定旋转方向和目标旋转速度
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

        // 根据加速度调整当前旋转速度
        currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed, targetRotationSpeed, accelerationRate * Time.deltaTime);

        // 根据当前旋转速度和方向进行旋转
        transform.Rotate(rotationDirection, currentRotationSpeed * Time.deltaTime);

        Debug.LogWarning(GetEffectiveRotationX());

        // 检查是否超出指定角度范围，如果是则停止旋转
        float effectiveRotationX = GetEffectiveRotationX();
        if (!(effectiveRotationX >= targetAngleMin && effectiveRotationX <= targetAngleMax) && !shouldStop)
        {
            currentRotationSpeed = 0f;
            moveData = "0"; // 停止旋转
            shouldStop = true; // 设置 shouldStop 为 true，表示已经停止旋转
        }
    }

    // 获取有效的 x 轴旋转角度
    float GetEffectiveRotationX()
    {
        float effectiveRotation = transform.localEulerAngles.x;
        if (transform.localRotation.eulerAngles.x > 180)
        {
            effectiveRotation -= 360; // 将大于180度的角度转换为负数
        }
        return effectiveRotation;
    }
}
