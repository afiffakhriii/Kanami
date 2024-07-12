using UnityEngine;
using TMPro; // Tambahkan ini untuk menggunakan TextMeshPro
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataDiriManager : MonoBehaviour
{
    public TMP_InputField inputFieldNama; // Ganti InputField dengan TMP_InputField
    public Button submitButton;
    public Button nextButton; // Tambahkan referensi ke tombol NextButton
    public Text resultText;

    void Start()
    {
        // Sembunyikan resultText dan nextButton saat scene dimulai
        resultText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        // Tambahkan listener untuk tombol submit
        submitButton.onClick.AddListener(OnSubmit);

        // Tambahkan listener untuk tombol nextButton jika ingin berpindah scene
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    void OnSubmit()
    {
        // Ambil data dari input fields
        string namaLengkap = inputFieldNama.text;

        // Validasi input
        if (string.IsNullOrEmpty(namaLengkap))
        {
            // Tampilkan pesan kesalahan
            resultText.text = "Nama lengkap harus diisi!";
            resultText.color = Color.red; // Set warna teks menjadi merah untuk pesan kesalahan
            resultText.gameObject.SetActive(true); // Tampilkan resultText
            return;
        }

        // Simpan data menggunakan PlayerPrefs
        PlayerPrefs.SetString("NamaLengkap", namaLengkap);
        PlayerPrefs.Save();

        // Tampilkan pesan sukses
        resultText.text = "Data berhasil disimpan!";
        resultText.color = Color.green; // Set warna teks menjadi hijau untuk pesan sukses
        resultText.gameObject.SetActive(true); // Tampilkan resultText

        // Tampilkan nextButton setelah data berhasil disimpan
        nextButton.gameObject.SetActive(true);
    }

    void OnNextButtonClicked()
    {
        // Ganti "NextSceneName" dengan nama scene yang ingin Anda tuju
        SceneManager.LoadScene("SceneMenu");
    }
}
