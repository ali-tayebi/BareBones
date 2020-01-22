using System;

namespace BareBones.StartupTasks
{
    [Serializable]
    public class StartupTaskFatalException : Exception
    {
        public StartupTaskFatalException(Type startupTask, Exception innerException)
            : base($"Fatal error in running startup task ({startupTask.FullName})", innerException)
        {
            StartupTask = startupTask;
        }
        public Type StartupTask { get; }
    }
}
