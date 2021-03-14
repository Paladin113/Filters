using UnityEngine;

public class ButtonAdressBar : MonoBehaviour
{
    [SerializeField] private Main main;
    public int sizeContent;
    public int deep;

    public void Click()
    {
        while (main.deepList.Count - 1 != deep)
        {
            main.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, sizeContent);
            DestroyImmediate(main.adressBarComponentsList[main.adressBarComponentsList.Count - 1].gameObject);
            main.adressBarComponentsList.RemoveRange(main.deepList.Count - 1, 1);
            Item[] items = Resources.FindObjectsOfTypeAll<Item>();
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].deepNumber == deep + 1)
                {
                    items[i].isOpen = false;
                    items[i].textInButton.text = items[i].textInButton.text;
                }
            }
            DestroyImmediate(main.deepList[main.deepList.Count - 1].gameObject);
            main.deepList.RemoveRange(main.deepList.Count - 1, 1);
            main.deepList[main.deepList.Count - 1].SetActive(true);
        }
        main.adressbarContent.GetComponent<RectTransform>().sizeDelta = new Vector3(-650 + (main.deepList.Count - 1) * 100, 25);
        if (main.filterManager.gameObject.activeSelf) main.filterManager.DestroyNotFit(main.deepList.Count);
        if (main.filterManager.availableFilters.activeSelf) main.filterManager.RefreshAvailableFilters();
    }

    private void Start()
    {
        main = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
    }
}
