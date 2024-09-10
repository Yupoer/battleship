using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject cylinderTarget; // ���w�A�� Cylinder Target
    public GameObject imageTarget; // ���w�A�� Image Target

    private bool isColliding = false;

    // ��t�@�Ӫ���i�JĲ�o���ɽե�
    private void OnTriggerEnter(Collider other)
    {
        // �ˬd�O�_�O���w�� Image Target
        if (other.gameObject == imageTarget && !isColliding)
        {
            Debug.Log("Cylinder Target & Image Target coll");
            isColliding = true;
            TriggerEvent();
        }
    }

    // ��t�@�Ӫ������}Ĳ�o���ɽե�
    private void OnTriggerExit(Collider other)
    {
        // ���餣�A�I���ɡA���m���A
        if (other.gameObject == imageTarget && isColliding)
        {
            isColliding = false;
        }
    }

    // Ĳ�o���ƥ�
    private void TriggerEvent()
    {
        // �b�o�̳B�z�A���ƥ��޿�A�Ҧp�}�lŪ��
        Debug.Log("event start");
    }
}
