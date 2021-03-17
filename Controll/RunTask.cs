using System.Diagnostics;
using System.IO;

namespace Controll
{
    public class RunTask
    {
        private readonly FileInfo _path;
        private Process Process { get; set; }

        public RunTask(FileInfo fileInfo)
        {
            _path = fileInfo;
        }

        public void OpenFile()
        {
            Process = Process.Start(_path.FullName);
        }

        public void CloseFile()
        {
            Process.Kill();
        }
    }
}