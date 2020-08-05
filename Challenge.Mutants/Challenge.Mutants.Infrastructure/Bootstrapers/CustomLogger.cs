using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace Challenge.Mutants.Infrastructure.Bootstrapers
{
    public interface ICustomLogger
    {
        bool Debug(string message);
        bool Information(string message);
        bool Warning(string message);
        bool Error(string message, Exception exception);
    }

    public class CustomLogger : ICustomLogger
    {
        private readonly string[] availables;
        private readonly string filePath;
        private readonly string fileName;

        public CustomLogger(IConfiguration configuration)
        {
            availables = configuration.GetSection("Logging:Availables").Get<string[]>();
            filePath = configuration.GetSection("Logging:FilePath").Value;
            fileName = string.Format("log-{0:yyyy-MM-dd}.txt", DateTime.Now);
        }

        public CustomLogger(string[] availables, string filePath)
        {
            this.availables = availables;
            this.filePath = filePath;
            fileName = string.Format("log-{0:yyyy-MM-dd}.txt", DateTime.Now);
        }

        public bool Debug(string message)
        {
            var level = availables.FirstOrDefault(x => x.Equals("Debug"));
            if (level == null)
            {
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);

            var path = $"{filePath}{fileName}";

            using (StreamWriter file = new StreamWriter(path, true))
            {
                file.WriteLine($"{level}: {message}");
                file.Close();
            }

            return true;
        }

        public bool Information(string message)
        {
            var level = availables.FirstOrDefault(x => x.Equals("Information"));
            if (level == null)
            {
                return false;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);

            var path = $"{filePath}{fileName}";

            using (StreamWriter file = new StreamWriter(path, true))
            {
                file.WriteLine($"{level}: {message}");
                file.Close();
            }

            return true;
        }

        public bool Warning(string message)
        {
            var level = availables.FirstOrDefault(x => x.Equals("Warning"));
            if (level == null)
            {
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);

            var path = $"{filePath}{fileName}";

            using (StreamWriter file = new StreamWriter(path, true))
            {
                file.WriteLine($"{level}: {message}");
                file.Close();
            }

            return true;
        }

        public bool Error(string message, Exception exception)
        {
            var level = availables.FirstOrDefault(x => x.Equals("Error"));
            if (level == null)
            {
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);

            var path = $"{filePath}{fileName}";

            using (StreamWriter file = new StreamWriter(path, true))
            {
                file.WriteLine($"{level}: {message}");
                file.WriteLine($"{exception.StackTrace}");
                file.Close();
            }

            return true;
        }
    }
}
