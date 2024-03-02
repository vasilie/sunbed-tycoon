using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StocksManager : MonoBehaviour
{
    public GameObject itemLine;
    public Dictionary<string, GameObject> stockObjects = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    public void UpdateUI(List<InventorySlot> itemList) {
        for (int i = 0; i < itemList.Count; i++)
        {
            string id = itemList[i].item.name;
            if (stockObjects.ContainsKey(id)) {
                stockObjects[id].transform.GetChild(1).GetComponent<Text>().text = itemList[i].count.ToString();
            } else {
                GameObject line = Instantiate(itemLine, gameObject.transform);
                line.transform.GetChild(0).GetComponent<Text>().text = itemList[i].item.name;
                line.transform.GetChild(1).GetComponent<Text>().text = itemList[i].count.ToString();
                stockObjects.Add(id, line);
            }
        }
    }
}
