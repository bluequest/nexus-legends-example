using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject BeginGameCanvas;

    private void Start()
    {
        PlayerPrefs.SetString("PlayerName", "ninjasNo1Fan");
        BeginGameCanvas.SetActive(true);
    }
}
