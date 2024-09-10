using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomObserverEventHandler : DefaultObserverEventHandler
{
    public Slider loadingBar; // 讀條的 UI 元素
    public float loadingTime = 1f; // 讀條持續時間
    private bool isTrackingLost = false;
    private float timer = 0f;
    private float lastLoggedProgress = 0f;

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        Debug.Log("Target lost, starting loading bar...");
        isTrackingLost = true;
        loadingBar.gameObject.SetActive(true); // 顯示讀條
        timer = 0f;
        lastLoggedProgress = 0f; // 重置
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        isTrackingLost = false;
        Debug.Log("Target Found");
    }

    private void Update()
    {
        if (isTrackingLost)
        {
            timer += Time.deltaTime;
            float progress = timer / loadingTime;
            loadingBar.value = progress;

            // 新的 10% 增量
            if (progress - lastLoggedProgress >= 0.1f)
            {
                Debug.Log("Loading progress: " + (progress * 100).ToString("F0") + "%");
                lastLoggedProgress = progress;
            }

            if (timer >= loadingTime)
            {
                // 讀條結束，觸發自定義事件
                Debug.Log("Loading complete, triggering event...");
                TriggerCustomEvent();
                isTrackingLost = false;
                loadingBar.gameObject.SetActive(false); // 隱藏讀條
            }
        }
    }

    private void TriggerCustomEvent()
    {
        // 在這裡觸發你自定義的事件
        Debug.Log("event start");
    }
}
