using UnityEngine;
using LitJson;
using UnityEngine.UI;
using System.Net;
using System.IO;

public class ButtonDemoFile : MonoBehaviour
{
    [SerializeField] private Main main= null;
    [SerializeField] private Text textPrompt = null;

    public void StartParseWhithDemoFile()
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://lb.rs.ceramic3d.com/cat_structure_demo.json");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            main.itemData = JsonMapper.ToObject(jsonResponse);
            main.CloseMenu();
        }
        catch
        {
            textPrompt.text = "Для демо файла нужен интеренет";
        }
    }
}
