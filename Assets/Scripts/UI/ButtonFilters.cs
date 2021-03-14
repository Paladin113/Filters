using UnityEngine;

public class ButtonFilters : MonoBehaviour
{
    [SerializeField] private Main main = null;
    [SerializeField] private GameObject availableFilters = null;

    public void Click()
    {
        if (!availableFilters.activeSelf)
        {
            main.filterManager.RefreshAvailableFilters();
        }
        else
        {
            availableFilters.SetActive(false);
        }
    }   
}
