using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public static bool isInStoneMode = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInStoneMode = !isInStoneMode;
            Debug.Log("モード切替: " + (isInStoneMode ? "石操作モード" : "カメラモード"));
        }
    }
}