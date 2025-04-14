using UnityEngine;

public class StoneDragger : MonoBehaviour
{
    private Camera mainCamera;
    private bool dragging = false;
    private Rigidbody rb;
    private bool isRotating = false;
    private float dragDistance; // カメラからの距離
    public float moveSpeed = 5f;

    public static StoneDragger currentDraggedStone = null;
    private Vector3 grabOffset;

        void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void OnMouseDown()
    {
        dragging = true;
        currentDraggedStone = this;
        rb.isKinematic = true;

        // 📌 ここで dragDistance を記録（超重要！）
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.position);
        dragDistance = screenPoint.z;

        // オフセット補正（省略してもOK）
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = dragDistance;
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(mousePos);
        grabOffset = transform.position - worldPoint;
    }



    void OnMouseUp()
    {
        dragging = false;
        currentDraggedStone = null;
        rb.isKinematic = false;

        ScoreManager.instance.AddStone(this.gameObject); // スコア追加！
    }

    void Update()
    {
        if (!dragging || StoneModeManager.currentMode != StoneMode.Stack) return;
        if (StoneModeManager.currentMode == StoneMode.Throw) return; // ←投げモードでは移動だけさせて、離す処理は無効


        // この下にマウス追従・上下・回転処理などが続く


        Vector3 mousePos = Input.mousePosition;
        mousePos.z = dragDistance;
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(mousePos);
        transform.position = Vector3.Lerp(transform.position, worldPoint, 15f * Time.deltaTime);



        // 前後移動（W/Sキー）→ dragDistanceを調整してマウス追従先を変える
        if (Input.GetKey(KeyCode.W))
            dragDistance += moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            dragDistance -= moveSpeed * Time.deltaTime;


        // ホイール回転（Shift / Altで軸変更）
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.001f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                transform.Rotate(Vector3.right, scroll * 200f);
            else if (Input.GetKey(KeyCode.LeftAlt))
                transform.Rotate(Vector3.forward, scroll * 200f);
            else
                transform.Rotate(Vector3.up, scroll * 200f);
        }

        // 右ドラッグで上下操作
        if (Input.GetMouseButton(1))
        {
            float deltaY = Input.GetAxis("Mouse Y") * moveSpeed;
            float smoothedY = Mathf.Lerp(0f, -deltaY, Time.deltaTime * 10f);
            transform.position += new Vector3(0f, smoothedY, 0f);
        }
    }

}
