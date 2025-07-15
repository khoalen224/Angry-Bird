using UnityEngine;
using UnityEngine.UI;
public class IconHandler : MonoBehaviour
{
    [SerializeField] private Image[] iconImage;
    [SerializeField] private Color[] usedColors;

    public void UseBird(int birdNumber)
    {
        int index = birdNumber - 1;
        if (index < 0 || index >= iconImage.Length || index >= usedColors.Length)
        {
            Debug.LogWarning($"IconHandler: birdNumber {birdNumber} is out of range.");
            return;
        }

        if (iconImage[index] != null)
        {
            iconImage[index].color = usedColors[index];
        }
        else
        {
            Debug.LogWarning($"IconHandler: iconImage[{index}] is null or destroyed.");
        }
    }
}
