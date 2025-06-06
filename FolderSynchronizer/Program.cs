internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Usage: FolderSynchronizer.exe <sourcePath> <replicaPath> <intervalForSync> <logFile>");
            return;
        }

        string sourcePath = args[0];
        string replicaPath = args[1];
        int intervalSecs = int.Parse(args[2]);
        string logFile = args[3];
    }
}