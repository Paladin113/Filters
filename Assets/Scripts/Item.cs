using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Класс элемента в панели
/// </summary>
public class Item : MonoBehaviour
{
    [SerializeField] private Item itemPrefab = null;
    [SerializeField] private ButtonAdressBar adressBarComponent = null;
    [SerializeField] public RectTransform rectTransform = null;
    [SerializeField] public Text textInButton = null;
    [SerializeField] public Button button = null;
    [SerializeField] public GameObject deep = null;
    [SerializeField] public Main main = null;
    [NonSerialized] public TreeBound treeBound;
    [NonSerialized] public bool isOpen;
    [NonSerialized] public Item oldItem = null;
    [NonSerialized] private Item item = null;
    [NonSerialized] public TreeElement treeElement;
    [NonSerialized] public AvailableFilter filter;
    public int deepNumber;

    public void Click()
    {
        if (!isOpen && treeElement == null)
        {
            //Реализация item в JSON Parser
            if (main.deepList.Count > deepNumber)
            {
                for (int i = deepNumber; i < main.deepList.Count; i++)
                {
                    Destroy(main.deepList[i].gameObject);
                }
                main.deepList.RemoveRange(deepNumber, main.deepList.Count - deepNumber);
            }

            ButtonAdressBar barComponent = Instantiate(adressBarComponent);
            barComponent.deep = deepNumber;
            barComponent.name = $"{treeBound.name}";
            barComponent.transform.SetParent(main.adressbarContent.transform, false);
            main.adressbarContent.GetComponent<RectTransform>().sizeDelta = new Vector3(-650 + deepNumber * 100, 25);
            barComponent.GetComponent<RectTransform>().anchoredPosition = new Vector3(main.adressBarComponentsList[deepNumber - 1].GetComponent<RectTransform>().anchoredPosition.x + 100, 0, 0);
            barComponent.gameObject.transform.GetChild(0).GetComponent<Text>().text = treeBound.name;
            main.adressBarComponentsList.Add(barComponent);

            GameObject deepd = Instantiate(deep);
            deepd.name = $"deep{deepNumber}";
            deepd.transform.SetParent(main.content.transform, false);
            main.deepList.Add(deepd);
            main.deepList[deepNumber - 1].SetActive(false);

            Item[] items = FindObjectsOfType<Item>();
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].deepNumber == deepNumber)
                {
                    items[i].isOpen = false;
                }
            }
            if (treeBound.fields.Count != 0)
            {
                main.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 15 + 35 * treeBound.fields.Count);
                barComponent.sizeContent = 15 + 35 * treeBound.fields.Count;
                for (int i = 0; i < treeBound.fields.Count; i++)
                {
                    oldItem.isOpen = true;
                    item = Instantiate(itemPrefab);
                    item.rectTransform.anchoredPosition = new Vector3(140, -20 + i * -35, 0);
                    item.transform.SetParent(deepd.transform, false);
                    item.oldItem = item;
                    item.deepNumber++;
                    item.isOpen = false;
                    string[] array = treeBound.fields.Keys.ToArray<string>();
                    item.name = treeBound.fields[array[i]];
                    item.textInButton.text = $"{array[i]} : {treeBound.fields[array[i]]}";

                    item.button.interactable = false;

                }
            }
            else if (treeBound.childs.Count != 0)
            {
                main.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 15 + 35 * treeBound.childs.Count);
                barComponent.sizeContent = 15 + 35 * treeBound.childs.Count;

                for (int i = 0; i < treeBound.childs.Count; i++)
                {
                    oldItem.isOpen = true;
                    item = Instantiate(itemPrefab);
                    item.rectTransform.anchoredPosition = new Vector3(140, -20 + i * -35, 0);
                    item.treeBound = treeBound.childs[i];
                    item.transform.SetParent(deepd.transform, false);
                    item.oldItem = item;
                    item.deepNumber++;
                    item.isOpen = false;
                    item.name = treeBound.childs[i].name;

                    if (item.treeBound.childs.Count != 0)
                    {
                        item.textInButton.text = treeBound.childs[i].name + " {" + treeBound.childs[i].childs.Count + "}";
                    }
                    else
                    {
                        item.textInButton.text = item.treeBound.name + " {" + item.treeBound.fields.Count + "}";
                    }


                    if (treeBound.childs[i].childs.Count == 0 && treeBound.childs[i].fields.Count == 0)
                    {
                        item.button.interactable = false;
                    }

                    if (treeBound.childs.Count == 1)
                    {
                        item.Click();
                    }
                }
            }
        }
        else
        {
            //Реализация item в задании
            OnMouseExit();
            if (main.deepList.Count > deepNumber)
            {
                for (int i = deepNumber; i < main.deepList.Count; i++)
                {
                    Destroy(main.deepList[i].gameObject);
                }
                main.deepList.RemoveRange(deepNumber, main.deepList.Count - deepNumber);
            }
            ButtonAdressBar barComponent = Instantiate(adressBarComponent);
            barComponent.deep = deepNumber;

            string nameElement;
            if (treeElement.caption.Count != 0)
            {
                nameElement = $"{treeElement.caption[main.language]}";
            }
            else
            {
                nameElement = $"{treeBound.name}";
            }


            barComponent.name = $"{nameElement}";

            barComponent.transform.SetParent(main.adressbarContent.transform, false);
            main.adressbarContent.GetComponent<RectTransform>().sizeDelta = new Vector3(-650 + deepNumber * 100, 25);
            barComponent.GetComponent<RectTransform>().anchoredPosition = new Vector3(main.adressBarComponentsList[deepNumber - 1].GetComponent<RectTransform>().anchoredPosition.x + 100, 0, 0);
            barComponent.gameObject.transform.GetChild(0).GetComponent<Text>().text = nameElement;
            main.adressBarComponentsList.Add(barComponent);

            GameObject deepd = Instantiate(deep);
            deepd.name = $"deep{deepNumber}";
            deepd.transform.SetParent(main.content.transform, false);
            main.deepList.Add(deepd);
            main.deepList[deepNumber - 1].SetActive(false);

            Item[] items = FindObjectsOfType<Item>();
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].deepNumber == deepNumber)
                {
                    items[i].isOpen = false;
                }
            }
            if (treeBound.childs.Count != 0)
            {
                main.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 15 + 35 * treeBound.childs.Count);
                barComponent.sizeContent = 15 + 35 * treeBound.childs.Count;

                for (int i = 0; i < treeBound.childs.Count; i++)
                {
                    oldItem.isOpen = true;
                    item = Instantiate(itemPrefab);
                    item.rectTransform.anchoredPosition = new Vector3(140, -20 + i * -35, 0);
                    item.treeBound = treeBound.childs[i];
                    item.transform.SetParent(deepd.transform, false);

                    item.treeElement = main.treeElements[treeBound.childs[i].name];

                    item.oldItem = item;
                    item.deepNumber++;
                    item.isOpen = false;

                    string itemNameElement;
                    if (main.treeElements[treeBound.childs[i].name].caption.Count != 0)
                    {
                        itemNameElement = $"{main.treeElements[treeBound.childs[i].name].caption[main.language]}";
                    }
                    else
                    {
                        itemNameElement = $"{treeBound.name}";
                    }
                    item.name = $"{itemNameElement}";

                    item.textInButton.text = itemNameElement;

                    if (treeBound.childs[i].childs.Count == 0 && treeBound.childs[i].fields.Count == 0)
                    {
                        item.GetComponent<Button>().interactable = false;
                    }

                    if (treeBound.childs.Count == 1)
                    {
                        item.Click();
                    }
                }
            }
            main.filterManager.DestroyNotFit(main.deepList.Count);
            if (main.filterManager.availableFilters.activeSelf)
            {
                main.filterManager.RefreshAvailableFilters();
            }
        }
    }
    private void Start()
    {
        main = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
    }

    private void OnMouseExit()
    {
        main.itemPanelInfo.gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if(treeElement != null && main.info)
        {
            main.itemPanelInfo.gameObject.SetActive(true);
            main.filterManager.GetFilters(new Item[1] { this });
            main.itemPanelInfo.SetInfo(main.treeElements[treeBound.name].caption[main.language], main.filterManager.filtersLists);
        }              
    }
}
