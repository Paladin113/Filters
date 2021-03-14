using UnityEngine;
using UnityEngine.UI;

public class ButtonLanguage : MonoBehaviour
{
    [SerializeField] private Main main = null;
    [SerializeField] private Text text = null;
    public void ChangeLanguage()
    {
        if(main.language == "ru")
        {
            main.language = "en";
            text.text = "English";
        }
        else
        {
            main.language = "ru";
            text.text = "Русский";
        }
    }
}
