using System.Collections.Generic;
using LitJson;
using System.Linq;

/// <summary>
/// Класс удобного представления Json
/// </summary>
public class TreeBound
{
    public List<TreeBound> childs = new List<TreeBound>();
    public Dictionary<string, string> fields = new Dictionary<string, string>();
    public string name;

    public TreeBound(string Name, JsonData jd)
    {
        name = Name;
        string[] array = null;

        if (jd.IsString || jd.IsObject)
        {
            array = jd.Keys.ToArray<string>();
        }

        for (int i = 0; i < jd.Count; i++)
        {
            if (jd[i].IsString)
            {
                if (jd.IsArray)
                {
                    fields.Add(i.ToString(), jd[i].ToString());
                }
                else
                {
                    fields.Add(array[i], jd[i].ToString());
                }
            }
            else if (jd[i].IsArray)
            {
                childs.Add(new TreeBound(array[i], jd[i]));
            }
            else
            {
                childs.Add(new TreeBound(array[i], jd[i]));
            }
        }
    }
}
