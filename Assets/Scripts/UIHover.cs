using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIHover : MonoBehaviour
{
    [SerializeField] private Button currentButton;
    [SerializeField] private Image currentOutline;

    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    [SerializeField] TextMeshProUGUI playButtonText;
    [SerializeField] TextMeshProUGUI settingsButtonText;
    [SerializeField] TextMeshProUGUI quitButtonText;
    private Dictionary<Button, TextMeshProUGUI> buttonToText;

    private AudioSource audioSource;
    public AudioClip hoverSound;
    private void Start()
    {
        currentButton = null;
        currentOutline = null;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        buttonToText = new Dictionary<Button, TextMeshProUGUI>();
        buttonToText.Add(playButton, playButtonText);
        buttonToText.Add(quitButton, quitButtonText);
    }
    public void MouseEnter(Button button)
    {
        currentButton = button;
        currentOutline = button.GetComponentInChildren<Image>();
        currentOutline.enabled = true;

        if (hoverSound != null)
        {
            audioSource.clip = hoverSound;
            audioSource.Play();
        }
        MakeTextBig();
    }


    public void MouseLeave()
    {
        currentOutline.enabled = false;
        MakeTextNormal();
        currentOutline = null;
        currentButton = null;
    }

    void MakeTextBig()
    {
        if (buttonToText.TryGetValue(currentButton, out TextMeshProUGUI buttonText))
        {
            Debug.Log("Text Is Big");
            buttonText.color = Color.white;
            Vector3 scale = buttonText.transform.localScale;
            scale *= 1.2f;
            buttonText.transform.localScale = scale;
        }
    }


    void MakeTextNormal()
    {
        if (currentButton != null && buttonToText.TryGetValue(currentButton, out TextMeshProUGUI buttonText))
        {
            Debug.Log("Text Is Normal");
            Color defaultUIColor = new Color(0.85f, 0.84f, 0.84f);
            buttonText.color = defaultUIColor;
            Vector3 scale = buttonText.transform.localScale;
            scale /= 1.2f;
            buttonText.transform.localScale = scale;
        }
    }
}