using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public float zoomSpeed = 10f;

    private float yaw = 0f;
    private float pitch = 0f;

    private bool draggingWithLeftClick = false;

    void Update()
    {
        if (ModeManager.isInStoneMode || StoneDragger.currentDraggedStone != null) return;


        // 視点回転（右ドラッグ or 石以外の左クリック中）
        if (Input.GetMouseButton(1) || draggingWithLeftClick)
        {
            yaw += Input.GetAxis("Mouse X") * lookSpeed;
            pitch -= Input.GetAxis("Mouse Y") * lookSpeed;
            pitch = Mathf.Clamp(pitch, -90f, 90f);
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }

        // 左クリックで石じゃないときにドラッグ開始
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit) || hit.collider.tag != "Stone")
            {
                draggingWithLeftClick = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            draggingWithLeftClick = false;
        }

        // WASD移動＋上下
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * v + transform.right * h;

        if (Input.GetKey(KeyCode.Space)) move += Vector3.up;
        if (Input.GetKey(KeyCode.LeftControl)) move += Vector3.down;

        transform.position += move * moveSpeed * Time.deltaTime;

        // ホイールズーム
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.position += transform.forward * scroll * zoomSpeed;
    }
}
