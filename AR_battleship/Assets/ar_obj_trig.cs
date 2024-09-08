using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ar_obj_trig : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void OnTriggerEnter(Collider other)
    {
        // 檢查進入的物體是否是魔術方塊
        // if (other.gameObject.CompareTag("RubiksCube"))
        // {
            Debug.Log("魔術方塊已進入觸發區域！");
        //     // 執行觸發事件
            TriggerEvent();
        // }
    }

    private void TriggerEvent()
    {
        // 這裡放入你想要的功能邏輯
        Debug.Log("觸發事件執行！");
    }
    private void OnCollisionEnter(Collision collision)
    {
       
            Debug.Log("魔術方塊已經發生碰撞！");
            TriggerEvent();
    }


}
