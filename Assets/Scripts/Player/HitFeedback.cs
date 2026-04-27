using System.Collections;
using UnityEngine;

public class HitFeedback : MonoBehaviour
{
    [Header("闪烁设置")]
    public Renderer playerRenderer;      // 手动指定 Renderer（3D模型）
    public SpriteRenderer playerSprite;   // 手动指定 SpriteRenderer（2D精灵）
    public Color hitColor = Color.red;
    public float flashInterval = 0.1f;
    public int flashCount = 3;

    [Header("相机震动")]
    public bool enableCameraShake = true;
    public float shakeDuration = 0.15f;
    public float shakeMagnitude = 0.1f;

    private Color originalColor;
    private Health playerHealth;
    private Material cachedMaterial;
    private bool canFlash = true;
    private Coroutine flashCoroutine;

    void Awake()
    {
        playerHealth = GetComponent<Health>();
        if (playerHealth == null)
        {
            Debug.LogError("HitFeedback 需要同一物体上的 Health 组件");
            enabled = false;
            return;
        }

        // 必须手动拖拽 playerRenderer 或 playerSprite
        if (playerRenderer == null && playerSprite == null)
        {
            Debug.LogError("HitFeedback：请手动拖拽玩家模型的 Renderer 或 SpriteRenderer 到对应字段！");
            canFlash = false;
            return;
        }

        // 获取原始颜色并准备材质
        if (playerRenderer != null)
        {
            cachedMaterial = playerRenderer.material;
            if (cachedMaterial.HasProperty("_Color"))
            {
                originalColor = cachedMaterial.color;
            }
            else
            {
                Debug.LogWarning("材质没有 _Color 属性，无法闪烁");
                canFlash = false;
            }
        }
        else if (playerSprite != null)
        {
            originalColor = playerSprite.color;
        }

        // 监听受伤事件
        playerHealth.OnHealthChanged += OnHealthChanged;
    }

    void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int currentHP, int maxHP)
    {
        if (!canFlash) return;

        // 停止当前闪烁，重新开始（避免多次受伤时重叠）
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(FlashHit());

        if (enableCameraShake && CameraShake.Instance != null)
            CameraShake.Instance.Shake(shakeDuration, shakeMagnitude);
    }

    IEnumerator FlashHit()
    {
        for (int i = 0; i < flashCount; i++)
        {
            // 闪烁颜色
            if (playerRenderer != null && cachedMaterial != null)
                cachedMaterial.color = hitColor;
            else if (playerSprite != null)
                playerSprite.color = hitColor;

            yield return new WaitForSeconds(flashInterval);

            // 恢复原始颜色
            if (playerRenderer != null && cachedMaterial != null)
                cachedMaterial.color = originalColor;
            else if (playerSprite != null)
                playerSprite.color = originalColor;

            yield return new WaitForSeconds(flashInterval);
        }
        flashCoroutine = null;
    }
}