using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    [SerializeField] private Main main = null;
    [SerializeField] private Button button = null;
    [SerializeField] private Sprite spriteOn = null;
    [SerializeField] private Sprite spriteOff = null;

    public void Click()
    {
        if(main.info)
        {
            main.info = false;
            button.image.sprite = spriteOff;
        }
        else
        {
            main.info = true;
            button.image.sprite = spriteOn;
        }     
    }
}
