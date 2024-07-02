using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DataManagerSample();
        }
    }

    private void DataManagerSample()
    {
        StartCoroutine(DataManager.Instance.CoLoadAndProcessCsv<ProductData>("CSVSample/Product_v6.csv", 100));
        StartCoroutine(DataManager.Instance.CoLoadAndProcessCsv<CatalogData>("CSVSample/Catalog_v2.csv", 100));
    }
}
