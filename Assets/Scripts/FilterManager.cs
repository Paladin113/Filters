using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Класс для работы с фильтрами
/// </summary>
public class FilterManager : MonoBehaviour
{
    [SerializeField] private Main main = null;
    [SerializeField] private Text textAvailableFilterPrefab = null;
    [SerializeField] private ToggleFilter toggleAvailableFilterPrefab = null;
    [SerializeField] private GameObject content = null;
    public GameObject availableFilters = null;
    public Dictionary<string, List<string>> filtersLists = null;

    /// <summary>
    /// Метод заполняет панель фильтров отоносительно активных элементов
    /// </summary>
    public void RefreshAvailableFilters()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        Item[] items = FindObjectsOfType<Item>();
        GetFilters(items);
        availableFilters.SetActive(true);
        int size = 0;       
        foreach (var filter in filtersLists)
        {
            size++;
            Text text = Instantiate(textAvailableFilterPrefab);
            text.transform.SetParent(content.transform, false);
            text.text = main.availableFilters[filter.Key].caption[main.language];
            text.name = main.availableFilters[filter.Key].caption[main.language];
            text.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -30 * size);

            foreach (var filterValue in filtersLists[filter.Key])
            {
                size++;
                ToggleFilter toggle = Instantiate(toggleAvailableFilterPrefab);
                toggle.transform.SetParent(content.transform, false);
                toggle.key = filter.Key;
                toggle.value = filterValue;
                toggle.gameObject.transform.GetChild(0).GetComponent<Text>().text = main.availableFilters[filter.Key].values[filterValue][main.language];
                toggle.name = main.availableFilters[filter.Key].values[filterValue][main.language];
                toggle.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -30 * size);
                if (main.activeFilters.ContainsKey(filter.Key))
                {
                    if (main.activeFilters[filter.Key].Contains(filterValue))
                    {
                        toggle.toggle.isOn = true;
                    }
                }
            }
        }
        content.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 20 + size * 30);
    }

    /// <summary>
    /// Метод заполняет filtersLists доступными фильтрами, относительно масва элементов, подаваемых на вход
    /// </summary>
    /// <param name="items"></param>
    public void GetFilters(Item[] items)
    {
        filtersLists = new Dictionary<string, List<string>>();
        for (int i = items.Length - 1; i >= 0; i--)
        {
            FindFilter(items[i].treeBound);
        }
    }

    /// <summary>
    /// Рекурсивный метод заполняющий filtersLists относительно treeBound
    /// </summary>
    /// <param name="treeBound"></param>
    public void FindFilter(TreeBound treeBound)
    {
        if (main.treeElements[treeBound.name].filters.Count != 0)
        {
            Dictionary<string, List<string>> filter = new Dictionary<string, List<string>>();
            foreach (var filterTreeElement in main.treeElements[treeBound.name].filters)
            {
                List<string> filterValues = new List<string>();
                main.treeElements[treeBound.name].filters[filterTreeElement.Key].ForEach((item) =>
                {
                    filterValues.Add(item);
                });
                filter.Add(filterTreeElement.Key, filterValues);
            }


            AddFilter(filter);
        }
        if (treeBound.childs.Count != 0)
        {
            for (int i = 0; i < treeBound.childs.Count; i++)
            {
                FindFilter(treeBound.childs[i]);
            }
        }
    }

    /// <summary>
    /// Метод добавляет элемент в filtersLists
    /// </summary>
    /// <param name="filter"></param>
    public void AddFilter(Dictionary<string, List<string>> filter)
    {
        foreach (var item in filter)
        {
            if (filtersLists.ContainsKey(item.Key))
            {
                for (int i = 0; i < filter[item.Key].Count; i++)
                {
                    if (!filtersLists[item.Key].Contains(filter[item.Key][i]))
                    {
                        filtersLists[item.Key].Add(filter[item.Key][i]);
                    }
                }
            }
            else if (!filtersLists.ContainsKey(item.Key))
            {
                filtersLists.Add(item.Key, filter[item.Key]);
            }
        }
    }

    /// <summary>
    /// Метод убирает все элементы неподходящие под выбраные фильтры на опредетённом уровне глубины
    /// </summary>
    /// <param name="deep"></param>
    public void DestroyNotFit(int deep)
    {
        List<Item> items = Resources.FindObjectsOfTypeAll<Item>().ToList();

        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].deepNumber != deep || items[i].main == null) items.Remove(items[i]);
        }

        foreach (var item in items)
        {
            int points = 0;
            GetFilters(new Item[1] { item });
            if (main.activeFilters.Count != 0)
            {
                foreach (var activeFilter in main.activeFilters)
                {
                    for (int i = 0; i < main.activeFilters[activeFilter.Key].Count; i++)
                    {
                        if (filtersLists[activeFilter.Key].Contains(main.activeFilters[activeFilter.Key][i]))
                        {
                            points++;
                            break;
                        }
                    }
                }
            }
            if (points != main.activeFilters.Count)
            {
                item.gameObject.SetActive(false);
            }
            else
            {
                item.gameObject.SetActive(true);
            }
        }
        List<Item> activeItems = new List<Item>();
        int size = 0;
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].gameObject.activeSelf)
            {
                activeItems.Add(items[i]);
                items[i].rectTransform.anchoredPosition = new Vector3(140, -20 + size * -35, 0);
                size++;
            }
        }
        GetFilters(activeItems.ToArray());
        foreach (var activeFilter in main.activeFilters)
        {
            for (int i = 0; i < main.activeFilters[activeFilter.Key].Count; i++)
            {
                if (!filtersLists[activeFilter.Key].Contains(main.activeFilters[activeFilter.Key][i]))
                {
                    main.activeFilters[activeFilter.Key].Remove(main.activeFilters[activeFilter.Key][i]);
                }
            }
        }
        if (activeItems.Count == 1 && main.deepList.Count > 1)
        {
            if (activeItems[0].button.interactable) activeItems[0].Click();
        }
    }
}
