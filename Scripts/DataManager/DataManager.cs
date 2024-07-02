using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sinbad;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct DataJobHandle<T> where T : struct, IDataEntry
{
    public JobHandle JobHandle;
    public NativeArray<byte> FileData;
    public NativeArray<T> Results;
    public NativeArray<int> Count;
}

public class DataManager : TSingleton<DataManager>
{
    public Dictionary<Type, List<IDataEntry>> DataMap = new Dictionary<Type, List<IDataEntry>>();

    public IEnumerator CoLoadAndProcessCsv<T>(string filePath, int maxEntries) where T : struct, IDataEntry
    {
        var fileData = CsvUtil.LoadCsvFileData(GetPath(filePath));
        var results = new NativeArray<T>(maxEntries, Allocator.TempJob);
        var count = new NativeArray<int>(1, Allocator.TempJob);

        var job = new CsvParseJob<T>
        {
            FileData = fileData,
            Results = results,
            Count = count
        };

        var jobHandle = job.Schedule();

        // Job이 완료될 때까지 기다립니다.
        yield return new WaitUntil(() => jobHandle.IsCompleted);

        jobHandle.Complete();

        // 결과 처리
        ProcessResults(results, count, typeof(T));

        // 자원 해제
        fileData.Dispose();
        results.Dispose();
        count.Dispose();
    }

    private void ProcessResults<T>(NativeArray<T> results, NativeArray<int> count, Type type) where T : struct, IDataEntry
    {
        var resultCount = count[0];
        var resultArray = new T[resultCount];
        results.Slice(0, resultCount).CopyTo(resultArray);
        DataMap[type] = new List<IDataEntry>();
        for (var i = 0; i < resultCount; i++)
        {
            DataMap[type].Add(resultArray[i]);
        }
    }

    private string GetPath(string filePath)
    {
        return $"{GetLocalPath()}{filePath}";
    }

    private string GetLocalPath()
    {
        var path = string.Empty;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                path = Application.persistentDataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                Debug.Log($"Android Path 1 : {path}");
                path = $"{path}/RawResources/";
                var directoryPath = Path.GetDirectoryName(path);
                path = directoryPath;
                Debug.Log($"Android Path 2 : {path}");
                path = Path.Combine(path, "Resources/");
                Debug.Log($"Android Path 3 : {path}");
                return path;
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
                path = Application.dataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, "Assets", "Resources/");
            case RuntimePlatform.WindowsEditor:
                path = Application.dataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, "Assets", "Resources/");
            default:
                path = Application.dataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, "Resources/");
        }
    }
}