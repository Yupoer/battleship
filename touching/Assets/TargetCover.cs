using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomObserverEventHandler : DefaultObserverEventHandler
{
    public Slider loadingBar; //  UI
    public float loadingTime = 1f; // loading time in seconds
    private bool isTrackingLost = false;
    private float timer = 0f;
    private float lastLoggedProgress = 0f;
    private bool stopRepeatLoop = false;

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        Debug.Log("Target lost");
        isTrackingLost = true;
        loadingBar.gameObject.SetActive(true); 
        timer = 0f;
        lastLoggedProgress = 0f; // reset
        stopRepeatLoop = false;
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        isTrackingLost = false;
        Debug.Log("Target Found, starting loading bar...");
    }

    private void Update()
    {
        if (!isTrackingLost && !stopRepeatLoop)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / loadingTime);
            loadingBar.value = progress;

            // 將進度更新到 ProgressManager
            ProgressManager.Instance.UpdateProgress(progress);

            // new +20% 
            if (progress - lastLoggedProgress >= 0.1f)
            {
                Debug.Log("Loading progress: " + (progress * 100).ToString("F0") + "%");
                lastLoggedProgress = progress;
            }

            if (timer >= loadingTime)
            {
                // end of loading
                Debug.Log("Loading complete, triggering event...");
                TriggerCustomEvent();
                loadingBar.gameObject.SetActive(false); 
                stopRepeatLoop = true;
            }
        }
    }

    private void TriggerCustomEvent()
    {
        Debug.Log("event start");
    }
}
