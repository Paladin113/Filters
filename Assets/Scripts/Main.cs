using UnityEngine;
using System.Linq;
using LitJson;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net;
using System.IO;

/// <summary>
/// Главный класс, где находится вся нужная информация
/// </summary>
public class Main : MonoBehaviour
{
    [SerializeField] private GameObject menu = null;
    [SerializeField] private GameObject jsonMenu = null;
    public FilterManager filterManager = null;
    public GameObject content = null;
    public GameObject adressbarContent = null;
    public GameObject scrollView = null;
    public ItemPanelInfo itemPanelInfo = null;
    public JsonData itemData;
    public TreeBound treeBound;
    public List<GameObject> deepList = new List<GameObject>();
    public List<ButtonAdressBar> adressBarComponentsList = new List<ButtonAdressBar>();
    public Dictionary<string, TreeElement> treeElements = new Dictionary<string, TreeElement>();
    public Dictionary<string, AvailableFilter> availableFilters = new Dictionary<string, AvailableFilter>();
    public Dictionary<string, List<string>> activeFilters = new Dictionary<string, List<string>>();
    public string language = "ru";
    public bool info = true;

    public void CloseMenu()
    {
        treeBound = new TreeBound("main", itemData);
        menu.SetActive(false);
        jsonMenu.SetActive(true);
    }

    /// <summary>
    /// Метод заполняет массив availableFilters доступными фильтрами из данного JSON
    /// </summary>
    public void GetAvailableFilters()
    {
        availableFilters.Clear();
        TreeBound bound = null;
        foreach (var item in treeBound.childs)
        {
            if (item.name == "available_filters")
            {
                foreach (var itemChilds in item.childs)
                {
                    if (itemChilds.name == "items")
                    {
                        bound = itemChilds;
                    }
                }
            }
        }
        
        for (int i = 0; i < bound.childs.Count; i++)
        {
            Dictionary<string, string> caption = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> values = new Dictionary<string, Dictionary<string, string>>();

            var filter = new AvailableFilter();

            foreach (var captionItem in bound.childs[i].childs[0].fields)
            {
                caption.Add(captionItem.Key, captionItem.Value);
            }

            if (bound.childs[i].childs.Count > 1)
            {
                foreach (var value in bound.childs[i].childs[1].childs)
                {
                    Dictionary<string, string> captionItems = new Dictionary<string, string>();
                    foreach (var captionItem in value.childs[0].fields)
                    {
                        captionItems.Add(captionItem.Key, captionItem.Value);
                    }
                    values.Add(value.name, captionItems);
                }
            }
            filter.caption = caption;
            filter.values = values;
            availableFilters.Add(bound.childs[i].name, filter);
        }
    }

    /// <summary>
    /// Метод заполняет массив treeElements доступными элементами из данного JSON
    /// </summary>
    public void GetTreeElements()
    {
        treeElements.Clear();
        TreeBound bound = null;
        foreach (var item in treeBound.childs)
        {
            if (item.name == "tree_elements")
            {
                bound = item;
            }
        }
        for (int i = 0; i < bound.childs.Count; i++)
        {
            List<string> filter_list = new List<string>();
            Dictionary<string, string> caption = new Dictionary<string, string>();
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>();

            var element = new TreeElement();


            if (bound.childs[i].childs.Count == 1)
            {
                if (bound.childs[i].childs[0].name == "caption")
                {

                    for (int j = 0; j < bound.childs[i].childs[0].fields.Count; j++)
                    {
                        string[] array = bound.childs[i].childs[0].fields.Keys.ToArray<string>();
                        caption.Add(array[j], bound.childs[i].childs[0].fields[array[j]]);
                    }
                }
                else if (bound.childs[i].childs[0].name == "filters")
                {
                    for (int j = 0; j < bound.childs[i].childs[0].childs.Count; j++)
                    {
                        List<string> itemsInFilter = new List<string>();
                        for (int l = 0; l < bound.childs[i].childs[0].childs[j].fields.Count; l++)
                        {
                            string[] arr = bound.childs[i].childs[0].childs[j].fields.Keys.ToArray<string>();
                            itemsInFilter.Add(bound.childs[i].childs[0].childs[j].fields[arr[l]]);
                        }
                        filters.Add(bound.childs[i].childs[0].childs[j].name, itemsInFilter);
                    }
                    caption.Add("ru", bound.childs[i].name);
                    caption.Add("en", bound.childs[i].name);
                }
            }
            else if (bound.childs[i].childs.Count == 2)
            {
                for (int j = 0; j < bound.childs[i].childs[0].fields.Count; j++)
                {
                    filter_list.Add(bound.childs[i].childs[0].fields[j.ToString()]);
                }
                for (int j = 0; j < bound.childs[i].childs[1].fields.Count; j++)
                {
                    string[] array = bound.childs[i].childs[1].fields.Keys.ToArray<string>();
                    caption.Add(array[j], bound.childs[i].childs[1].fields[array[j]]);
                }
            }
            else
            {
                caption.Add("ru", bound.childs[i].name);
                caption.Add("en", bound.childs[i].name);
            }


            element.filter_list = filter_list;
            element.caption = caption;
            element.filters = filters;

            treeElements.Add(bound.childs[i].name, element);
        }
    }
}
