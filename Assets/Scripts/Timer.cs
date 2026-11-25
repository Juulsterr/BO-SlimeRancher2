using UnityEngine;
using TMPro;

public class DayNightTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private float startMinutes = 14f;
    [SerializeField] private float startSeconds = 24f;

    void Start()
    {

    }

    void Update()
    {
        startMinutes += Time.deltaTime;
        clockText.text = Mathf.Floor(startMinutes).ToString();
    }
}

