using UnityEngine;

public class StoneThrower : MonoBehaviour
{
    public float maxPower = 20f;
    public float chargeSpeed = 10f;
    private float throwPower = 0f;

    private bool isCharging = false;
    private bool isSelected = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        if (StoneModeManager.currentMode != StoneMode.Throw) return;

        isSelected = true;
        isCharging = true;
        throwPower = 0f;

        // クリックされたこの石だけを「選択状態」に
        isSelected = true;
        isCharging = true;
        throwPower = 0f;
    }

    void OnMouseUp()
    {
        if (!isSelected) return;

        isCharging = false;
        isSelected = false;

        if (StoneModeManager.currentMode == StoneMode.Throw)
        {
            rb.isKinematic = false;

            Vector3 direction = Camera.main.transform.forward;
            rb.AddForce(direction * throwPower, ForceMode.Impulse);
        }
        else if (StoneModeManager.currentMode == StoneMode.Stack)
        {
            rb.isKinematic = false; // 積むときはそのまま落とすだけ
        }
    }

    void Update()
    {
        if (isCharging && isSelected)
        {
            throwPower += chargeSpeed * Time.deltaTime;
            throwPower = Mathf.Clamp(throwPower, 0f, maxPower);
        }
    }
}
