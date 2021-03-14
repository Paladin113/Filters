using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFilter : MonoBehaviour
{
    [SerializeField] private Main main = null;
    [SerializeField] public Toggle toggle = null;
    public string key;
    public string value;

    public void Click()
    {
        if (main != null)
        {
            if (main.activeFilters.ContainsKey(key) && toggle.isOn && !main.activeFilters[key].Contains(value))
            {
                main.activeFilters[key].Add(value);
            }
            else if (!main.activeFilters.ContainsKey(key) && toggle.isOn)
            {
                main.activeFilters.Add(key, new List<string>() { value });
            }
            if (!toggle.isOn)
            {
                if (main.activeFilters.ContainsKey(key))
                {
                    main.activeFilters[key].Remove(value);
                    if (main.activeFilters[key].Count == 0)
                    {
                        main.activeFilters.Remove(key);
                    }
                }
                foreach (Transform child in main.deepList[main.deepList.Count - 1].transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            main.filterManager.DestroyNotFit(main.deepList.Count);
            main.filterManager.RefreshAvailableFilters();
        }
    }

    private void Start()
    {
        main = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
    }
}
