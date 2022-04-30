using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameField;
    public static string playerName;

    public void Play()
    {
        playerName = playerNameField.text;
        SceneManager.LoadScene("Lobby");
    }
}
