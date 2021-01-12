using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
public class AwaitOperator
{
    public static async Task Main(string[] args)
    {
        string url,fileName;
        if(args.Length ==2)
        {
            (url,fileName) = (args[0],args[1]);
        }
        else
        {
            (url,fileName) = ("https://docs.microsoft.com/en-us/","bin/Debug/net5.0/test");
            Console.WriteLine("no data config");
        }
        Task<int> downloading = DownloadDocsMainPageAsync(url,fileName);
        Console.WriteLine($"{nameof(Main)}: Launched downloading.");

        int bytesLoaded = await downloading;
        Console.WriteLine($"{nameof(Main)}: Downloaded {bytesLoaded} bytes.");
    }

    private static async Task<int> DownloadDocsMainPageAsync(string url,string filepath)
    {
        Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: About to start downloading.");

        var client = new HttpClient();
        byte[] content = await client.GetByteArrayAsync(url);
        File.WriteAllBytes(filepath,content);
        Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: Finished downloading.");
        return content.Length;
    }
}
// Output similar to:
// DownloadDocsMainPageAsync: About to start downloading.
// Main: Launched downloading.
// DownloadDocsMainPageAsync: Finished downloading.
// Main: Downloaded 27700 bytes.