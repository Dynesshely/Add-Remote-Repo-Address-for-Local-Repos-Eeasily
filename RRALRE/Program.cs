using System.Diagnostics;

namespace ARRALRE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string root_path = Path.GetFullPath(args[0]);
            Dictionary<string, string> targets = new();
            for (int i = 1; i < args.Length; ++i)
                targets.Add($"{root_path}\\{args[i]}", args[i]);
            foreach (string item in targets.Keys)
            {
                DirectoryInfo dir = new(item);
                foreach (DirectoryInfo subdir in dir.GetDirectories())
                {
                    using Process git = new();
                    git.StartInfo.WorkingDirectory = subdir.FullName;
                    git.StartInfo.FileName = "git.exe";
                    git.StartInfo.Arguments = $"remote add home-server git@home-server:{targets[item]}/{subdir.Name}.git";
                    git.StartInfo.UseShellExecute = false;
                    git.StartInfo.CreateNoWindow = false;
                    git.Start();
                    git.WaitForExit();
                }
            }
        }
    }
}