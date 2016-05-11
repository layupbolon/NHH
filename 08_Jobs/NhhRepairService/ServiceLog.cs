using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhRepairService
{
        /// <summary>
        /// 服务日志
        /// </summary>
        public class ServiceLog
        {
            private static readonly object obj = new object();

            /// <summary>
            /// 记录日志
            /// </summary>
            /// <param name="taskName"></param>
            /// <param name="message"></param>
            /// <param name="type"></param>
            public static void Log(string taskName, string message, string type)
            {
                lock (obj)
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    path = Path.Combine(path, taskName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    path = Path.Combine(path, DateTime.Now.ToString("yyyy-MM"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string filePath = Path.Combine(path, string.Format("{0}.txt", DateTime.Now.ToString("yyyyMMdd")));
                    if (!File.Exists(filePath))
                    {
                        using (var stream = File.Create(filePath))
                        {
                            stream.Close();
                        }
                    }

                    using (StreamWriter writer = new StreamWriter(filePath, true, System.Text.Encoding.UTF8))
                    {
                        writer.WriteLine(string.Format("{0} [{1}]{2}", DateTime.Now.ToString("HH:mm:ss"), type, message));
                        writer.WriteLine("-----------------------------------------------------------------");
                        writer.Close();
                    }
                }
            }
        }
    }
