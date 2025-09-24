using TMPro;
using UnityEngine;

public class GoldValueUI : MonoBehaviour
{
    private PlayerPropertiesScript playerProperties;
    private TextMeshProUGUI goldText;

    private void Start()
    {
        playerProperties = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPropertiesScript>();
        goldText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        goldText.text = "Coin: " + playerProperties.Gold;
    }
}
