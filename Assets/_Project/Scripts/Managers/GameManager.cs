using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;
    private bool _isWin = false;

    [SerializeField] private UnityEvent _onGameOver;
    [SerializeField] private UnityEvent _onWin;

    public bool GetIsGameOver() => _isGameOver;
    public bool GetIsWin() => _isWin;

    public void GameOver()
    {
        if (_isGameOver) return;

        _isGameOver = true;
        Invoke(nameof(InvokeGameOver), 1);
    }

    public void InvokeGameOver()
    {
        _onGameOver?.Invoke();
        SoundManager.Instance.PlaySFXSound("Game Over");
        SoundManager.Instance.StopBackgroundMusic();
    }

    public void Win()
    {
        if (_isWin) return;

        _isWin = true;
        Invoke(nameof(InvokeWin), 1);
    }

    public void InvokeWin()
    {
        _onWin?.Invoke();
        SoundManager.Instance.PlaySFXSound("Win");
        SoundManager.Instance.StopBackgroundMusic();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
