using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomObserverEventHandler : DefaultObserverEventHandler
{
    public Slider loadingBar; // Ū���� UI ����
    public float loadingTime = 1f; // Ū������ɶ�
    private bool isTrackingLost = false;
    private float timer = 0f;
    private float lastLoggedProgress = 0f;

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        Debug.Log("Target lost, starting loading bar...");
        isTrackingLost = true;
        loadingBar.gameObject.SetActive(true); // ���Ū��
        timer = 0f;
        lastLoggedProgress = 0f; // ���m
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

            // �s�� 10% �W�q
            if (progress - lastLoggedProgress >= 0.1f)
            {
                Debug.Log("Loading progress: " + (progress * 100).ToString("F0") + "%");
                lastLoggedProgress = progress;
            }

            if (timer >= loadingTime)
            {
                // Ū�������AĲ�o�۩w�q�ƥ�
                Debug.Log("Loading complete, triggering event...");
                TriggerCustomEvent();
                isTrackingLost = false;
                loadingBar.gameObject.SetActive(false); // ����Ū��
            }
        }
    }

    private void TriggerCustomEvent()
    {
        // �b�o��Ĳ�o�A�۩w�q���ƥ�
        Debug.Log("event start");
    }
}
