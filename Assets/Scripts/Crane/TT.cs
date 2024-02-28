using UnityEngine;

public class TT : MonoBehaviour
{
    public float maxTranslationSpeed = 10f; // 最大移动速度
    public float accelerationRate = 10f; // 加速度
    public float decelerationRate = 10f; // 减速度
    public float minXPosition = -5.87f; // 最小 x 轴位置
    public float maxXPosition = -0.37f; // 最大 x 轴位置
    private bool shouldStop = false; // 是否应该停止移动
    private string previousMoveData = "0"; // 上一个移动数据
    public string moveData;

    [SerializeField]
    private float currentTranslationSpeed = 0f; // 当前移动速度

    // 向前移动
    public void MoveForward()
    {
        moveData = "1";
        shouldStop = false; // 重新开始移动时重置 shouldStop
    }

    // 向后移动
    public void MoveBackward()
    {
        moveData = "2";
        shouldStop = false; // 重新开始移动时重置 shouldStop
    }

    // 停止移动
    public void StopMovement()
    {
        moveData = "0";
    }

    // 更新方法，每帧执行
    void Update()
    {
        Vector3 translationDirection = Vector3.zero;
        float targetTranslationSpeed;

        // 根据移动数据确定移动方向和目标移动速度
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

        // 根据加速度调整当前移动速度
        currentTranslationSpeed = Mathf.MoveTowards(currentTranslationSpeed, targetTranslationSpeed, accelerationRate * Time.deltaTime);

        // 根据当前移动速度和方向进行移动
        transform.Translate(translationDirection * currentTranslationSpeed * Time.deltaTime);

        // 检查是否超出指定位置范围，如果是则停止移动并固定在范围边界上
        float effectiveXPosition = transform.localPosition.x;
        if (effectiveXPosition > maxXPosition)
        {
            transform.localPosition = new Vector3(maxXPosition, transform.localPosition.y, transform.localPosition.z);
            currentTranslationSpeed = 0f;
            moveData = "0"; // 停止移动
            shouldStop = true; // 设置 shouldStop 为 true，表示已经停止移动
        }
        else if (effectiveXPosition < minXPosition)
        {
            transform.localPosition = new Vector3(minXPosition, transform.localPosition.y, transform.localPosition.z);
            currentTranslationSpeed = 0f;
            moveData = "0"; // 停止移动
            shouldStop = true; // 设置 shouldStop 为 true，表示已经停止移动
        }
    }
}
