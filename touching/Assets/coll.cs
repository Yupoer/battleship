using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject cylinderTarget; // 指定你的 Cylinder Target
    public GameObject imageTarget; // 指定你的 Image Target

    private bool isColliding = false;

    // 當另一個物體進入觸發器時調用
    private void OnTriggerEnter(Collider other)
    {
        // 檢查是否是指定的 Image Target
        if (other.gameObject == imageTarget && !isColliding)
        {
            Debug.Log("Cylinder Target & Image Target coll");
            isColliding = true;
            TriggerEvent();
        }
    }

    // 當另一個物體離開觸發器時調用
    private void OnTriggerExit(Collider other)
    {
        // 當物體不再碰撞時，重置狀態
        if (other.gameObject == imageTarget && isColliding)
        {
            isColliding = false;
        }
    }

    // 觸發的事件
    private void TriggerEvent()
    {
        // 在這裡處理你的事件邏輯，例如開始讀條
        Debug.Log("event start");
    }
}
