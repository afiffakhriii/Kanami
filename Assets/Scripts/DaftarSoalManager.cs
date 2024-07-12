using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DaftarSoalManager : MonoBehaviour
{
    public Button kembaliButton;

    void Start()
    {
        // Atur listener untuk tombol Kembali
        kembaliButton.onClick.AddListener(GoBackToQuiz);
    }

    void GoBackToQuiz()
    {
        // Pindah ke scene kuis
        SceneManager.LoadScene("QuizScene");
    }
}
