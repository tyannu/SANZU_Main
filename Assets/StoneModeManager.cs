using UnityEngine;

public enum StoneMode { Stack, Throw }

public class StoneModeManager : MonoBehaviour
{
    public static StoneMode currentMode = StoneMode.Stack;

    void Update()
    {
        // Tabキーでモード切り替え
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentMode == StoneMode.Stack)
                currentMode = StoneMode.Throw;
            else
                currentMode = StoneMode.Stack;

            Debug.Log("モード切替: " + currentMode);
        }
    }
}
