using UnityEngine;
using TMPro;

public class UIHintManager : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI text;

    void Start()
    {
        panel.SetActive(false);
    }

    public void Show(string msg)
    {
        panel.SetActive(true);
        text.text = msg;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}