using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelInfo : MonoBehaviour
{
    [SerializeField] private Main main = null;
    [SerializeField] private RectTransform rectTransform = null;
    [SerializeField] private Camera cam = null;
    [SerializeField] private Text textName = null;
    [SerializeField] private Text textFilterName = null;
    [SerializeField] private Text textFilterItems = null;
    private Vector2 leftUpAngle = new Vector2();
    private float width;
    private float height;

    /// <summary>
    /// Заполняет информацию о директории и выводит её
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="FilterItems"></param>
    public void SetInfo(string Name, Dictionary<string, List<string>> FilterItems)
    {
        foreach (Transform child in transform)
        {
            if (child.name != "Image") Destroy(child.gameObject);
        }
        int maxNumElements = 0;
        foreach (var filterItem in FilterItems)
        {
            if (maxNumElements < FilterItems[filterItem.Key].Count) maxNumElements = FilterItems[filterItem.Key].Count;
        }
        if (FilterItems.Count != 0)
        {
            width = 130 * FilterItems.Count;
            height = 75 + maxNumElements * 25;
        }
        else
        {
            width = 130;
            height = 25;
        }

        rectTransform.sizeDelta = new Vector2(width, height);

        Text text = Instantiate(textName);
        text.transform.SetParent(gameObject.transform, false);
        text.text = Name;
        text.rectTransform.anchoredPosition = new Vector3(width / 2, -25);

        int sizeX = 0;
        foreach (var filterItem in FilterItems)
        {
            text = Instantiate(textFilterName);
            text.transform.SetParent(gameObject.transform, false);
            text.text = main.availableFilters[filterItem.Key].caption[main.language];
            text.rectTransform.anchoredPosition = new Vector3(65 + 130 * sizeX, -50);

            int sizeY = 0;
            foreach (var item in FilterItems[filterItem.Key])
            {                
                text = Instantiate(textFilterItems);
                text.transform.SetParent(gameObject.transform, false);
                text.text = main.availableFilters[filterItem.Key].values[item][main.language];
                text.rectTransform.anchoredPosition = new Vector3(65 + 130 * sizeX, -75 - 25 * sizeY);
                sizeY++;
            }
            sizeX++;
        }
    }

    private void Update()
    {
        leftUpAngle.x = Input.mousePosition.x - cam.pixelWidth / 2 + rectTransform.sizeDelta.x / 2;
        leftUpAngle.y = Input.mousePosition.y - cam.pixelHeight / 2 - rectTransform.sizeDelta.y / 2;
        rectTransform.localPosition = leftUpAngle;
    }
}
