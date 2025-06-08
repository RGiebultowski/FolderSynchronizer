using FolderSynchronizer;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Usage: FolderSynchronizer.exe <sourcePath> <replicaPath> <intervalForSync> <logFileName>");
            return;
        }

        string sourcePath = args[0];
        string replicaPath = args[1];
        int intervalSecs = int.Parse(args[2]);
        string logFile = args[3];

        Logger logger = new Logger(logFile);
        IFileComparer fileComparer = new FileComparer(logger);
        Synchronizer synchronizer = new Synchronizer(sourcePath, replicaPath, logger, fileComparer);

        logger.Log($"[Task] Syncing folders with interval: {intervalSecs}sec");
        while (true) 
        {
            try
            {
                logger.Log("[Task] Syncing...");
                synchronizer.Synchronize();
            }
            catch (Exception ex) 
            {
                logger.Log($"[Error] Could not sync folders: {ex.Message}");
            }

            Thread.Sleep(intervalSecs * 1000);
        }

    }
}