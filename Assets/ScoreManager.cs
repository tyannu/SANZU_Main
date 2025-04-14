using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ScoreManager : MonoBehaviour
{
    public float heightMultiplier = 10f;
    public TextMeshProUGUI scoreText;


    private int stoneCount = 0;
    private float maxHeight = 0f;

    public static ScoreManager instance;

    void Awake()
    {
        instance = this; // 他のスクリプトから使えるように
    }

    public void AddStone(GameObject stone)
    {
        stoneCount++;

        float y = stone.transform.position.y;
        if (y > maxHeight) maxHeight = y;

        UpdateScore();
    }

    void UpdateScore()
    {
        float score = stoneCount * 100 + maxHeight * heightMultiplier;
        int finalScore = Mathf.RoundToInt(score);
        scoreText.text = $"スコア: {finalScore}\n石の数: {stoneCount}\n高さ: {maxHeight:F2}";

    }
}
