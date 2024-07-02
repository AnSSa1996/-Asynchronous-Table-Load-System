using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using System.IO;
using Sinbad;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
public struct CsvParseJob<T> : IJob where T : struct, IDataEntry
{
    public NativeArray<byte> FileData;
    public NativeArray<T> Results;  // 고정 크기의 결과 배열
    public NativeArray<int> Count;  // 결과의 실제 개수를 저장하는 배열

    public void Execute()
    {
        var count = 0;
        var buffer = new List<byte>();
        for (var i = 0; i < FileData.Length; i++)
        {
            if (FileData[i] == '\n' || i == FileData.Length - 1)
            {
                if (buffer.Count > 0)
                {
                    var line = System.Text.Encoding.UTF8.GetString(buffer.ToArray());
                    T entry = new T();
                    entry.ParseCsv(line);
                    if (count < Results.Length)
                    {
                        Results[count] = entry;
                        count++;
                    }
                    buffer.Clear();
                }
            }
            else
            {
                buffer.Add(FileData[i]);
            }
        }
        Count[0] = count;
    }
}

