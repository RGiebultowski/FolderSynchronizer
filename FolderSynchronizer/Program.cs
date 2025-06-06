internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Usage: FolderSynchronizer.exe <sourcePath> <replicaPath> <interval> <logFile>");
        }
    }
}