﻿

using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDir = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory,"salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDir);

var salesTotal = CalculateSalesTotal(salesFiles);


File.AppendAllText(Path.Combine(salesTotalDir,"totals.txt"), $"{salesTotal}{Environment.NewLine}");



IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();
    
    var foundFiles = Directory.EnumerateFiles(folderName,"*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double SalesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return SalesTotal;
}
record SalesData (double Total);

var salesJson = File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");
var salesData = JsonConvert.DeserializeObject<salesTotalDir>(salesJson);

Console.WriteLine(salesData.Total);

class SalesTotal
{
    public double Total { get; set; }
    
}
