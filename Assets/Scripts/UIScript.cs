using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    private TextMeshProUGUI roundText;

    private void Start()
    {
        roundText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        roundText.text = "Round: " + GameControllerScript.RaidLevel;
    }
}
