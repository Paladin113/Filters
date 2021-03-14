using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс представления элементов дерева
/// </summary>
public class TreeElement
{
    public List<string> filter_list = null;
    public Dictionary<string, string> caption = null;
    public Dictionary<string, List<string>> filters = null;
}
