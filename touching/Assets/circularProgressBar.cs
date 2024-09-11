using UnityEngine;

public class RingFillController : MonoBehaviour
{
    public Material ringMaterial;  // 環形物件的材質

    void Update()
    {
        // 更新環形材質的進度參數，從 ProgressManager 取得進度
        //Debug.Log("Progress: " + ProgressManager.Instance.Progress);
        ringMaterial.SetFloat("_Progress", ProgressManager.Instance.Progress);
    }
}
