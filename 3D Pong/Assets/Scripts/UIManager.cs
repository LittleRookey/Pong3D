using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform restartCanvas;
    [SerializeField] private TextMeshProUGUI restartText;

    private void OnEnable()
    {
        GameController.Instance.OnGameEnd += ShowRestartCanvas;
        GameController.Instance.OnGameRestart += RemoveCanvas;
    }

    private void OnDisable()
    {
        GameController.Instance.OnGameEnd -= ShowRestartCanvas;
        GameController.Instance.OnGameRestart -= RemoveCanvas;
    }

    
    void Start()
    {
        restartCanvas.gameObject.SetActive(false);
    }

    private void ShowRestartCanvas()
    {
        restartText.SetText($"Game Over! You scored {GameController.Instance.Score}.\nPress 'R' to restart!");
        restartCanvas.gameObject.SetActive(true);
    }

    private void RemoveCanvas()
    {
        restartCanvas.gameObject.SetActive(false);
    }
}
