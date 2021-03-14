using UnityEngine;
using UnityEngine.UI;

public class ButtonTree : MonoBehaviour
{
    [SerializeField] private Main main = null;
    [SerializeField] private ButtonAdressBar adressBarComponent = null;
    [SerializeField] private Item itemPrefab = null;
    [SerializeField] private GameObject jsonParser = null;
    [SerializeField] private GameObject jsonMenu = null;
    [SerializeField] private GameObject buttonFilter = null;
    [SerializeField] private GameObject buttonInfo = null;
    [SerializeField] private GameObject deep = null;
    private Item item = null;

    public void Click()
    {
        main.GetTreeElements();
        main.GetAvailableFilters();
  
        main.filterManager.gameObject.SetActive(true);
        jsonMenu.SetActive(false);
        jsonParser.SetActive(true);
        buttonFilter.SetActive(true);
        buttonInfo.SetActive(true);

        ButtonAdressBar barComponent = Instantiate(adressBarComponent);
        barComponent.deep = 0;
        barComponent.name = $"Main";
        barComponent.transform.SetParent(main.adressbarContent.transform, false);
        main.adressbarContent.GetComponent<RectTransform>().sizeDelta = new Vector3(-650, 25);
        barComponent.GetComponent<RectTransform>().anchoredPosition = new Vector3(50, 0, 0);
        barComponent.gameObject.transform.GetChild(0).GetComponent<Text>().text = $"Main";
        main.adressBarComponentsList.Add(barComponent);

        GameObject deepd = Instantiate(deep);
        deepd.name = "deep0";
        deepd.transform.SetParent(main.content.transform, false);
        main.deepList.Add(deepd);

        main.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 15 + 35 * main.treeBound.childs.Count);
        barComponent.sizeContent = 15 + 35 * main.treeBound.childs.Count;

        item = Instantiate(itemPrefab);
        item.transform.SetParent(deepd.transform, false);
        item.rectTransform.anchoredPosition = new Vector3(140, -20, 0);
        item.treeBound = main.treeBound.childs[2];
        item.treeElement = main.treeElements[main.treeBound.childs[2].name];
        item.oldItem = item;
        item.deepNumber++;
        item.name = main.treeBound.childs[2].name;
        item.textInButton = item.gameObject.transform.GetChild(0).GetComponent<Text>();
        item.textInButton.text = main.treeBound.childs[2].name;
    }
}
