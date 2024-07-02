using Unity.Collections;
using UnityEngine;

public interface IDataEntry
{
    void ParseCsv(string csvLine);
}

public struct ProductData : IDataEntry
{
    public FixedString512Bytes partNumber;
    public FixedString512Bytes productType;
    public FixedString512Bytes category;
    public FixedString512Bytes brand;
    public FixedString512Bytes family;
    public FixedString512Bytes line;
    public FixedString512Bytes productSegment;
    public FixedString512Bytes status;
    public FixedString512Bytes valuel;
    public FixedString512Bytes valueCurrency;
    public FixedString512Bytes defaultQuantityUnits;
    public FixedString512Bytes name;
    public FixedString512Bytes description;
    public FixedString512Bytes plannerCode;
    public FixedString512Bytes sourceLink;

    public void ParseCsv(string csvLine)
    {
        var parts = csvLine.Split(',');
        if (parts.Length != 15)
        {
            return;
        }
        
        partNumber = parts[0];
        productType = parts[1];
        category = parts[2];
        brand = parts[3];
        family = parts[4];
        line = parts[5];
        productSegment = parts[6];
        status = parts[7];
        valuel = parts[8];
        valueCurrency = parts[9];
        defaultQuantityUnits = parts[10];
        name = parts[11];
        description = parts[12];
        plannerCode = parts[13];
        sourceLink = parts[14];
    }
}

public struct CatalogData : IDataEntry
{
    public FixedString512Bytes levelType;
    public FixedString512Bytes code;
    public FixedString512Bytes catalogType;
    public FixedString512Bytes name;
    public FixedString512Bytes description;
    public FixedString512Bytes sourceLink;
    
    public void ParseCsv(string csvLine)
    {
        var parts = csvLine.Split(',');
        if (parts.Length != 6)
        {
            return;
        }
        
        levelType = parts[0];
        code = parts[1];
        catalogType = parts[2];
        name = parts[3];
        description = parts[4];
        sourceLink = parts[5];
    }
}