using UnityEngine;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour
{
    public Button toggleButton;
    public Image targetImage;

    private bool isImageVisible = false;

    void Start()
    {
        // Sembunyikan gambar pada awalnya
        targetImage.gameObject.SetActive(false);

        // Tambahkan listener untuk tombol
        toggleButton.onClick.AddListener(ToggleImageVisibility);
    }

    void Update()
    {
        // Periksa input klik kiri mouse
        if (isImageVisible && Input.GetMouseButtonDown(0))
        {
            // Sembunyikan gambar
            targetImage.gameObject.SetActive(false);
            isImageVisible = false;
        }
    }

    void ToggleImageVisibility()
    {
        // Tampilkan gambar ketika tombol ditekan
        targetImage.gameObject.SetActive(true);
        isImageVisible = true;
    }
}
