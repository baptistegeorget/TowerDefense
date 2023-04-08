using TMPro;
using UnityEngine;

public class MoneyUi : MonoBehaviour
{
    public TextMeshProUGUI label;
    
    void Update()
    {
        label.text = Player.money.ToString();
    }
}
