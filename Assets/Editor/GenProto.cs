using UnityEngine;
using UnityEditor;
using System.Diagnostics;

public class ProtoExporter : Editor
{
    [MenuItem("Tools/Export Protobuf Files to C#")]
    private static void ExportProtobufToCSharp()
    {
        string protocPath = "Assets\\..\\..\\JFrame-Proto\\env\\bin\\protoc.exe"; // protoc编译器的路径
        // string protoDir0 = "Assets\\..\\..\\JFrame-Proto"; // 设定存放 Proto 文件的文件夹路径
        string protoDir1 = "Assets\\..\\..\\JFrame-Proto\\protocol"; // 设定存放 Proto 文件的文件夹路径
        // string protoDir2 = "Assets\\..\\..\\JFrame-Proto\\env\\include\\google\\protobuf"; // 设定存放 Proto 文件的文件夹路径
        // string protoDir3 = "Assets\\..\\..\\JFrame-Proto\\env\\include\\google\\protobuf\\compiler"; // 设定存放 Proto 文件的文件夹路径
        string outputDir = "Assets\\Script\\Proto\\"; // 设定导出 C# 文件的路径

        // 递归获取所有的 proto 文件
        string[] protoFiles1 = System.IO.Directory.GetFiles(protoDir1, "*.proto");

        foreach (string protoFile in protoFiles1)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = true,
                FileName = protocPath,
                Arguments = $"--csharp_out={outputDir} --proto_path={protoDir1} {protoFile}"
            };

            Process process = Process.Start(processInfo);
            process.WaitForExit();
        }
        /*
        // 递归获取所有的 proto 文件
        string[] protoFiles2 = System.IO.Directory.GetFiles(protoDir2, "*.proto");

        foreach (string protoFile in protoFiles2)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = true,
                FileName = protocPath,
                Arguments = $"--csharp_out={outputDir} --proto_path={protoDir2} {protoFile}"
            };

            Process process = Process.Start(processInfo);
            process.WaitForExit();
        }
        
        // 递归获取所有的 proto 文件
        string[] protoFiles3 = System.IO.Directory.GetFiles(protoDir3, "*.proto");

        foreach (string protoFile in protoFiles3)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = true,
                FileName = protocPath,
                Arguments = $"--csharp_out={outputDir} --proto_path={protoDir3} {protoFile}"
            };

            Process process = Process.Start(processInfo);
            process.WaitForExit();
        }

         */
        AssetDatabase.Refresh();
        UnityEngine.Debug.Log("Proto files exported to C# successfully!");
    }
}