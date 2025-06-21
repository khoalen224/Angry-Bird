using UnityEngine;
using UnityEngine.UI;
public class IconHandler : MonoBehaviour
{
    [SerializeField] private Image[] iconImage;
    [SerializeField] private Color[] usedColors;

    public void UseBird(int birdNumber)
    {
        for (int i = 0; i < iconImage.Length; i++)
        {
            if (birdNumber == i + 1) 
            {
                iconImage[i].color = usedColors[i];
                return;
            }
        }
    }
}
