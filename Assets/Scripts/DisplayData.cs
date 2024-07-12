using UnityEngine;
using UnityEngine.UI;

public class DisplayData : MonoBehaviour
{
    public Text displayNama;
    public Text displayNama2;

    void Start()
    {
        // Ambil data dari PlayerPrefs
        string namaLengkap = PlayerPrefs.GetString("NamaLengkap", "Nama tidak ditemukan");
        string namaLengkap2 = PlayerPrefs.GetString("NamaLengkap", "Nama tidak ditemukan");

        // Tampilkan data
        displayNama.text = "Selamat Datang " + namaLengkap;
        displayNama2.text = namaLengkap; 
    }
}
