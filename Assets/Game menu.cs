using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    // ����� ��� ������� ����
    public void StartGame()
    {
        // ��������� ����� � ����� (�������� "GameScene" �� ��� ����� ������� �����)
        SceneManager.LoadScene("SampleScene");
    }

    // ����� ��� �������� ��������
    public void OpenSettings()
    {
        // ��������� ����� � ����������� (�������� "SettingsScene" �� ��� ����� ����� ��������)
        SceneManager.LoadScene("SettingsScene");
    }

    // ����� ��� ������ �� ����
    public void QuitGame()
    {
        // ����� �� ���� (�������� ������ � ��������� ������ ����)
        Application.Quit();

        // ��� ������ � ��������� Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}