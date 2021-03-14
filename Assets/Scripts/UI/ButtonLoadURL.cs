using UnityEngine;
using LitJson;
using UnityEngine.UI;
using System.Net;
using System.IO;

public class ButtonLoadURL : MonoBehaviour
{
    [SerializeField] private Main main = null;
    [SerializeField] private Text textPrompt = null;
    [SerializeField] private InputField inputFieldText = null;

    public void StartParseWhithURL()
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(inputFieldText.text);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            main.itemData = JsonMapper.ToObject(jsonResponse);
            main.CloseMenu();
        }
        catch
        {
            textPrompt.text = "Проверьте правильность ссылки";
        }
    }
}
