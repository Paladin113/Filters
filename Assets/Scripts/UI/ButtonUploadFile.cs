using UnityEngine;
using LitJson;
using UnityEngine.UI;
using System.IO;

public class ButtonUploadFile : MonoBehaviour
{
    [SerializeField] private Main main = null;
    [SerializeField] private Text textPrompt = null;
    [SerializeField] private InputField inputFieldText = null;

    public void StartParseWhithPathToFile()
    {
        try
        {
            string path = Path.Combine(inputFieldText.text);
            StreamReader reader = new StreamReader(path);
            string jsonResponse = reader.ReadToEnd();
            main.itemData = JsonMapper.ToObject(jsonResponse);
            main.CloseMenu();
        }
        catch
        {
            textPrompt.text = "Проверьте путь к файлу";
        }        
    }
}
