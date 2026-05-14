using UnityEngine;
using TMPro;

public class VeggieEffect : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public float displayDuration = 2f;
    private float timer = 0f;
    private bool isShowing = false;

    void Update()
    {
        if (isShowing)
        {
            timer -= Time.deltaTime;
            popupText.alpha = timer / displayDuration;

            if (timer <= 0f)
            {
                isShowing = false;
                popupText.alpha = 0f;
            }
        }
    }

    public void Show(VeggieType type)
    {
        switch (type)
        {
            case VeggieType.Carrot:
                popupText.text = "SPEED UP!";
                popupText.color = new Color(1f, 0.5f, 0f); // orange
                break;
            case VeggieType.Eggplant:
                popupText.text = "HEALTH RESTORED!";
                popupText.color = new Color(0.6f, 0f, 0.8f); // purple
                break;
            case VeggieType.Mushroom:
                popupText.text = "JUMP BOOST!";
                popupText.color = new Color(0.2f, 0.8f, 0.2f); // green
                break;
            default:
                popupText.text = "POWER UP!";
                popupText.color = Color.white;
                break;
        }

        popupText.alpha = 1f;
        timer = displayDuration;
        isShowing = true;
    }
}