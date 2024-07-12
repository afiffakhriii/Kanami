using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public Text questionNumberText;
    public Text questionText;
    public Button buttonA;
    public Button buttonB;
    public Button buttonC;
    public Button buttonD;
    public Text resultText;
    public Text answerStatusText;
    public Button nextButton;
    public Button previousButton;
    public Button finishButton;
    public Text quizFinishedText;

    public VideoPlayer correctVideoPlayer;
    public VideoPlayer wrongVideoPlayer;
    public GameObject correctVideoPanel;
    public GameObject wrongVideoPanel;
    public VideoClip correctVideo;
    public VideoClip wrongVideo;

    public Image questionImage;
    public Sprite[] questionImages;

    // Audio sources for correct and wrong answers
    public AudioSource correctAudioSource;
    public AudioSource wrongAudioSource;

    private int currentQuestionIndex = 0;
    private List<Question> questions = new List<Question>();
    private int totalQuestions = 25;
    private int correctAnswersCount = 0;
    private Dictionary<int, string> userAnswers = new Dictionary<int, string>();

    // Warna untuk teks jawaban benar dan salah
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;

    void Start()
    {
        // Inisialisasi soal-soal (dalam contoh ini hanya menampilkan beberapa soal untuk keperluan demo)
        InitializeQuestions();

        // Assign button click listeners
        buttonA.onClick.AddListener(() => OnAnswerSelected("A"));
        buttonB.onClick.AddListener(() => OnAnswerSelected("B"));
        buttonC.onClick.AddListener(() => OnAnswerSelected("C"));
        buttonD.onClick.AddListener(() => OnAnswerSelected("D"));

        // Atur listener untuk tombol Next
        nextButton.onClick.AddListener(ShowNextQuestion);

        // Atur listener untuk tombol Previous
        previousButton.onClick.AddListener(ShowPreviousQuestion);

        // Sembunyikan tombol Next dan Previous secara default
        nextButton.gameObject.SetActive(false);
        previousButton.gameObject.SetActive(false);

        // Sembunyikan panel video
        correctVideoPanel.SetActive(false);
        wrongVideoPanel.SetActive(false);

        // Sembunyikan teks "Kuis Telah Selesai!"
        quizFinishedText.gameObject.SetActive(false);

        // Atur listener untuk tombol Finish
        finishButton.onClick.AddListener(EndQuiz);

        // Sembunyikan tombol Finish secara default
        finishButton.gameObject.SetActive(false);

        // Tampilkan soal pertama
        ShowQuestion();
    }

    void InitializeQuestions()
    {
        // Inisialisasi soal-soal
        questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar pertama?",
            options = new string[] { "A. はまみ。", "B. はなみ。", "C. はさみ。", "D. はたみ。" },
            correctAnswer = "B"
        });
        questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar ke-2?",
            options = new string[] { "A. さくら。", "B. さきら。", "C. さから。", "D. さぶら。" },
            correctAnswer = "A"
        });
        questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar ke-3?",
            options = new string[] { "A. とちゃ。", "B. こうちゃ。", "C. おちゃ。", "D. あちゃ。" },
            correctAnswer = "C"
        });
        questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar ke-4?",
            options = new string[] { "A. だんとう。", "B. あんとう。", "C. べんとう。", "D. ばんとう。" },
            correctAnswer = "C"
        });
        questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar ke-5?",
            options = new string[] { "A. 多ッシュ。", "B. シャツ。", "C. でッシュ。", "D. ティッシュ。" },
            correctAnswer = "D"
        });
    
        questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar ke-6?",
            options = new string[] { "A. おにがら。", "B. おにぎり。", "C. おにかり。", "D. おにぎら。" },
            correctAnswer = "B"
        });
        questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar ke-7?",
            options = new string[] { "A. いす。", "B. いさ。", "C. いち。", "D. いぬ。" },
            correctAnswer = "A"
        });
        questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar ke-8?",
            options = new string[] { "A. はおし。", "B. はおり。", "C. ほうし。", "D. ぼうし。" },
            correctAnswer = "D"
        });
        questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar ke-9?",
            options = new string[] { "A. ぬがれ。", "B. ぬかれ。", "C. めがね。", "D. めがれ。" },
            correctAnswer = "C"
        });
        questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar ke-10?",
            options = new string[] { "A. ごみばこ。", "B. ごみはこ。", "C. ごにはこ。", "D. ごにばこ。" },
            correctAnswer = "A"
        });
        questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar ke-11?",
            options = new string[] { "A. みズ。", "B. にず。", "C. みず。", "D. ミズ。" },
            correctAnswer = "C"
        });
         questions.Add(new Question
        {
            text = "Apa bahasa Jepang dari gambar ke-12?",
            options = new string[] { "A. はんこち。", "B. ハンカチ。", "C. ハンチカ。", "D. はんちこ。" },
            correctAnswer = "B"
        });
        questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-13?",
        options = new string[] { "A. サンドイチ。", "B. さんうぃち。", "C. さんどいっち。", "D. サンドイッチ。" },
        correctAnswer = "D"
        });
        questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-14?",
        options = new string[] { "A. ぱん。", "B. パソ。", "C. パン。", "D. はん。" },
        correctAnswer = "C"
        });
        questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-15?",
        options = new string[] { "A. けき。", "B. ケキ。", "C. ケーキ。", "D. けーき。" },
        correctAnswer = "C"
        });
        questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-16?",
        options = new string[] { "A. かみざら。", "B. かざみら。", "C. かみざち。", "D. かみちざ。" },
        correctAnswer = "A"
        });
        questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-17?",
        options = new string[] { "A. コヒー。", "B. コピー。", "C. コーヒー。", "D. コピ。" },
        correctAnswer = "C"
        });
        questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-18?",
        options = new string[] { "A. のみます。", "B. たべもの。", "C. くだもの。", "D. みもの。" },
        correctAnswer = "D"
        });
        questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-19?",
        options = new string[] { "A. ほん。", "B. はん。", "C. ぱん。", "D. ほおん。" },
        correctAnswer = "A"
        });
         questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-20?",
        options = new string[] { "A. のみます。", "B. たべもの。", "C. くだもの。", "D. のみもの。" },
        correctAnswer = "C"
        });
         questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-21?",
        options = new string[] { "A. たべる。", "B. さんぽする。", "C. よむ。", "D. のむ。" },
        correctAnswer = "A"
        });
        questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-22?",
        options = new string[] { "A. たべる。", "B. さんぽする。", "C. よむ。", "D. のむ。" },
        correctAnswer = "C"
        });
        questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-23?",
        options = new string[] { "A. たべる。", "B. のむ。", "C. よむ。", "D. さんぽする。" },
        correctAnswer = "B"
        });
        questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-24?",
        options = new string[] { "A. のむ。", "B. さんぽする。", "C. よむ。", "D. おさべりする。" },
        correctAnswer = "D"
        });
         questions.Add(new Question
        {
        text = "Apa bahasa Jepang dari gambar ke-25?",
        options = new string[] { "A. あそぶ。", "B. さんぽする。", "C. よむ。", "D. のむ。" },
        correctAnswer = "A"
        });
    }

    void ShowQuestion()
    {
        // Reset result text
        resultText.text = "";

        // Reset answer status text
        answerStatusText.text = "";

        // Tampilkan nomor soal
        questionNumberText.text = (currentQuestionIndex + 1).ToString();

        // Tampilkan soal dan opsi
        Question currentQuestion = questions[currentQuestionIndex];
        questionText.text = currentQuestion.text;
        buttonA.GetComponentInChildren<Text>().text = currentQuestion.options[0];
        buttonB.GetComponentInChildren<Text>().text = currentQuestion.options[1];
        buttonC.GetComponentInChildren<Text>().text = currentQuestion.options[2];
        buttonD.GetComponentInChildren<Text>().text = currentQuestion.options[3];

        // Tampilkan gambar yang sesuai dengan soal
        if (currentQuestionIndex < questionImages.Length)
        {
            questionImage.sprite = questionImages[currentQuestionIndex];
        }

        // Cek apakah pengguna telah menjawab pertanyaan ini sebelumnya
        if (userAnswers.ContainsKey(currentQuestionIndex))
        {
            // Tampilkan status jawaban sebelumnya
            string previousAnswer = userAnswers[currentQuestionIndex];
            if (previousAnswer == currentQuestion.correctAnswer)
            {
                answerStatusText.text = $"Jawaban {previousAnswer} Benar!";
                answerStatusText.color = correctColor; // Teks berwarna hijau untuk jawaban benar
            }
            else
            {
                answerStatusText.text = $"Jawaban {previousAnswer} Salah!";
                answerStatusText.color = wrongColor; // Teks berwarna merah untuk jawaban salah
            }

            // Nonaktifkan tombol jawaban agar tidak bisa mengubah jawaban
            buttonA.interactable = false;
            buttonB.interactable = false;
            buttonC.interactable = false;
            buttonD.interactable = false;

            // Tampilkan tombol Previous dan sembunyikan tombol Next jika bukan soal terakhir
            previousButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(currentQuestionIndex < questions.Count - 1);

            // Aktifkan tombol Finish hanya di soal terakhir
            finishButton.gameObject.SetActive(currentQuestionIndex == questions.Count - 1);

            // Tampilkan teks "Kuis Telah Selesai!" jika ini adalah soal terakhir
            quizFinishedText.gameObject.SetActive(currentQuestionIndex == questions.Count - 1);
        }
        else
        {
            // Reset interaksi tombol jawaban
            buttonA.interactable = true;
            buttonB.interactable = true;
            buttonC.interactable = true;
            buttonD.interactable = true;

            // Sembunyikan tombol Next dan Previous
            nextButton.gameObject.SetActive(false);
            previousButton.gameObject.SetActive(currentQuestionIndex > 0);

            // Sembunyikan tombol Finish dan teks "Kuis Telah Selesai!"
            finishButton.gameObject.SetActive(false);
            quizFinishedText.gameObject.SetActive(false);
        }
    }

    void OnAnswerSelected(string selectedAnswer)
    {
        // Simpan jawaban pengguna
        userAnswers[currentQuestionIndex] = selectedAnswer;

        // Periksa jawaban
        Question currentQuestion = questions[currentQuestionIndex];
        string correctAnswer = currentQuestion.correctAnswer;

        if (selectedAnswer == correctAnswer)
        {
            // Tampilkan teks "Jawaban [selectedAnswer] Benar!"
            answerStatusText.text = $"Jawaban {selectedAnswer} Benar!";
            answerStatusText.color = correctColor; // Warna teks hijau untuk jawaban benar
            correctAnswersCount++; // Tambah skor jika jawaban benar
            PlayVideo(true); // Mainkan video benar
        }
        else
        {
            // Tampilkan teks "Jawaban [selectedAnswer] Salah!"
            answerStatusText.text = $"Jawaban {selectedAnswer} Salah!";
            answerStatusText.color = wrongColor; // Warna teks merah untuk jawaban salah
            PlayVideo(false); // Mainkan video salah
        }

        // Nonaktifkan tombol jawaban setelah menjawab
        buttonA.interactable = false;
        buttonB.interactable = false;
        buttonC.interactable = false;
        buttonD.interactable = false;
    }

    void PlayVideo(bool isCorrect)
    {
        if (isCorrect)
        {
            // Tampilkan panel video benar
            correctVideoPanel.SetActive(true);
            correctVideoPlayer.clip = correctVideo;
            correctVideoPlayer.Play();
            correctAudioSource.Play(); // Mainkan suara benar

            // Tambahkan listener untuk mengatur panel agar disembunyikan setelah video selesai
            correctVideoPlayer.loopPointReached += OnVideoEndCorrect;
        }
        else
        {
            // Tampilkan panel video salah
            wrongVideoPanel.SetActive(true);
            wrongVideoPlayer.clip = wrongVideo;
            wrongVideoPlayer.Play();
            wrongAudioSource.Play(); // Mainkan suara salah

            // Tambahkan listener untuk mengatur panel agar disembunyikan setelah video selesai
            wrongVideoPlayer.loopPointReached += OnVideoEndWrong;
        }
    }

    void OnVideoEndCorrect(VideoPlayer vp)
    {
        // Sembunyikan panel video
        correctVideoPanel.SetActive(false);
        correctVideoPlayer.loopPointReached -= OnVideoEndCorrect; // Hapus listener setelah selesai
        correctAudioSource.Stop(); // Hentikan suara benar

        // Aktifkan tombol Next setelah video selesai, kecuali jika ini soal terakhir
        if (currentQuestionIndex < questions.Count - 1)
        {
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            // Ini adalah soal terakhir, aktifkan tombol Finish
            finishButton.gameObject.SetActive(true);
            // Tampilkan teks "Kuis Telah Selesai!"
            quizFinishedText.gameObject.SetActive(true);
        }
    }

    void OnVideoEndWrong(VideoPlayer vp)
    {
        // Sembunyikan panel video
        wrongVideoPanel.SetActive(false);
        wrongVideoPlayer.loopPointReached -= OnVideoEndWrong; // Hapus listener setelah selesai
        wrongAudioSource.Stop(); // Hentikan suara salah

        // Aktifkan tombol Next setelah video selesai, kecuali jika ini soal terakhir
        if (currentQuestionIndex < questions.Count - 1)
        {
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            // Ini adalah soal terakhir, aktifkan tombol Finish
            finishButton.gameObject.SetActive(true);
            // Tampilkan teks "Kuis Telah Selesai!"
            quizFinishedText.gameObject.SetActive(true);
        }
    }

    void ShowNextQuestion()
    {
        // Lanjut ke soal berikutnya atau akhiri kuis jika sudah mencapai total soal
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            // Sembunyikan teks "Kuis Telah Selesai!" jika ada
            quizFinishedText.gameObject.SetActive(false);
            ShowQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    void ShowPreviousQuestion()
    {
        // Kembali ke soal sebelumnya jika bukan soal pertama
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            ShowQuestion();
        }

        // Selalu tampilkan tombol Next setelah kembali ke soal sebelumnya
        nextButton.gameObject.SetActive(true);

        // Tampilkan tombol Finish hanya jika ini adalah soal terakhir
        finishButton.gameObject.SetActive(currentQuestionIndex == questions.Count - 1);

        // Sembunyikan tombol Previous jika kembali ke soal pertama
        previousButton.gameObject.SetActive(currentQuestionIndex > 0);
    }

    void EndQuiz()
    {
        // Simpan nilai akhir menggunakan PlayerPrefs
        PlayerPrefs.SetInt("FinalScore", correctAnswersCount * 4);
        PlayerPrefs.Save();

        // Pindah ke scene hasil
        SceneManager.LoadScene("Scene14");
    }
}

[System.Serializable]
public class Question
{
    public string text;
    public string[] options;
    public string correctAnswer;
}