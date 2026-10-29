using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Score and win condition for the "Orb Collector" lab goal. COMPLETED.
/// Create an empty GameObject named "GameManager" and attach this script.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("State")]
    [SerializeField] private int totalOrbs = 5;

    private int collected;

    void Awake()
    {
        // Simple singleton so the player can call GameManager.Instance.Collect().
        Instance = this;

        // TODO 6: count the orbs live instead of using a fixed number.
        GameObject[] orbs = GameObject.FindGameObjectsWithTag("Orb");
        totalOrbs = orbs.Length;
        Debug.Log("[GameManager] Orbs in scene: " + totalOrbs);
    }

    public void Collect()
    {
        collected++;
        Debug.Log($"[GameManager] Collected {collected}/{totalOrbs}");
    }

    void OnGUI()
    {
        // TODO 7: show a big centred win banner when every orb is collected.
        if (totalOrbs > 0 && collected >= totalOrbs)
        {
            GUIStyle winStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 64,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "YOU WIN!", winStyle);
            return;
        }

        GUI.Label(new Rect(12, 10, 400, 30), $"Orbs collected: {collected} / {totalOrbs}", new GUIStyle(GUI.skin.label) { fontSize = 20 });
    }
}
