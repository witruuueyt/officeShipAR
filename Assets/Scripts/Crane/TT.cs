using UnityEngine;

public class TT : MonoBehaviour
{
    public float maxTranslationSpeed = 10f; 
    public float accelerationRate = 10f;
    public float decelerationRate = 10f; 
    public float minXPosition = -5.87f;
    public float maxXPosition = -0.37f;
    private string previousMoveData = "0"; 
    public string moveData;

    [SerializeField]
    private float currentTranslationSpeed = 0f; 


    public void MoveForward()
    {
        moveData = "1";
    }

 
    public void MoveBackward()
    {
        moveData = "2";
    }

   
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
            translationDirection = Vector3.forward; //因为父级物体的角度以及自己的角度，反正是沿着x轴移动的
            targetTranslationSpeed = maxTranslationSpeed;
            previousMoveData = "1";
        }
        else if (moveData.Equals("2"))
        {
            translationDirection = Vector3.back; //因为父级物体的角度以及自己的角度，反正是沿着x轴移动的
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
            moveData = "0"; 
        }
        else if (effectiveXPosition < minXPosition)
        {
            transform.localPosition = new Vector3(minXPosition, transform.localPosition.y, transform.localPosition.z);
            currentTranslationSpeed = 0f;
            moveData = "0"; 
        }
    }
}
