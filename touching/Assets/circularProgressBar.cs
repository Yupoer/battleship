using UnityEngine;

public class RingFillController : MonoBehaviour
{
    public Material ringMaterial;  // ���Ϊ��󪺧���

    void Update()
    {
        // ��s���Χ��誺�i�װѼơA�q ProgressManager ���o�i��
        //Debug.Log("Progress: " + ProgressManager.Instance.Progress);
        ringMaterial.SetFloat("_Progress", ProgressManager.Instance.Progress);
    }
}
