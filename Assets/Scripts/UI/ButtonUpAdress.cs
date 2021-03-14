using UnityEngine;

public class ButtonUpAdress : MonoBehaviour
{
    [SerializeField] private Main main = null;
    public void Click()
    {
        if (main.deepList.Count > 1)
        {
            DestroyImmediate(main.adressBarComponentsList[main.adressBarComponentsList.Count - 1].gameObject);
            main.adressBarComponentsList.RemoveRange(main.deepList.Count - 1, 1);
            main.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, main.adressBarComponentsList[main.adressBarComponentsList.Count - 1].sizeContent);

            Item[] items = Resources.FindObjectsOfTypeAll<Item>();
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].deepNumber == main.deepList.Count - 1)
                {
                    items[i].isOpen = false;
                    items[i].textInButton.text = items[i].textInButton.text;
                }
            }
            DestroyImmediate(main.deepList[main.deepList.Count - 1].gameObject);
            main.deepList.RemoveRange(main.deepList.Count - 1, 1);
            main.deepList[main.deepList.Count - 1].SetActive(true);
            main.adressbarContent.GetComponent<RectTransform>().sizeDelta = new Vector3(-650 + (main.deepList.Count - 1) * 100, 25);
            int fitItems = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].deepNumber == main.deepList.Count && items[i].gameObject.activeSelf)
                {
                    fitItems++;
                }
            }
            if (fitItems <= 1 && main.deepList.Count > 1) Click();
            if (main.filterManager.gameObject.activeSelf) main.filterManager.DestroyNotFit(main.deepList.Count);
            if (main.filterManager.availableFilters.activeSelf) main.filterManager.RefreshAvailableFilters();
        }
    }
}
