using FolderSynchronizer;

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

        Logger logger = new Logger(logFile);
        Synchronizer synchronizer = new Synchronizer(sourcePath, replicaPath, logger);

        logger.Log($"[Logger Task] Syncing folders with interval: {intervalSecs}sec");
        while (true) 
        {
            try
            {
                synchronizer.Synchronize();
            }
            catch (Exception ex) 
            {
                logger.Log($"[Logger Error] Could not sync folders: {ex.Message}");
            }

            Thread.Sleep(intervalSecs * 1000);
        }

    }
}