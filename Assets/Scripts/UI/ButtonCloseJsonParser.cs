using System.Collections.Generic;
using UnityEngine;

public class ButtonCloseJsonParser : MonoBehaviour
{
    [SerializeField] private Main main = null;
    [SerializeField] private GameObject jsonParser = null;
    [SerializeField] private GameObject jsonMenu = null;
    [SerializeField] private GameObject contentJsonParser = null;
    [SerializeField] private GameObject contentAddressbar = null;
    [SerializeField] private GameObject availableFilters = null;    

    public void Click()
    {
        jsonMenu.SetActive(true);
        jsonParser.SetActive(false);
        availableFilters.SetActive(false);

        foreach (Transform child in contentJsonParser.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in contentAddressbar.transform)
        {
            Destroy(child.gameObject);
        }
        main.adressBarComponentsList = new List<ButtonAdressBar>();
        main.deepList = new List<GameObject>();
        main.activeFilters.Clear();
    }
}
