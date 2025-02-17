using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    // Метод для запуска игры
    public void StartGame()
    {
        // Загружаем сцену с игрой (замените "GameScene" на имя вашей игровой сцены)
        SceneManager.LoadScene("SampleScene");
    }

    // Метод для открытия настроек
    public void OpenSettings()
    {
        // Загружаем сцену с настройками (замените "SettingsScene" на имя вашей сцены настроек)
        SceneManager.LoadScene("SettingsScene");
    }

    // Метод для выхода из игры
    public void QuitGame()
    {
        // Выход из игры (работает только в собранной версии игры)
        Application.Quit();

        // Для выхода в редакторе Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}