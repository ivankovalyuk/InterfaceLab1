using System;
using System.IO;
using System.Management; // нужно добавить ссылку
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int j = 10;
            do
            {
                Console.WriteLine("Information about system");
                Console.WriteLine("Hardware (HW) or Software (SW) information would you see");
                string i = Console.ReadLine();
                if (i == "HW" || i == "hw")
                {
                    Console.WriteLine("Information about: Disk (D) | PROCESSOR (P) | RAM (R) |");
                    string hw = Console.ReadLine();
                    if (hw == "D" || i == "d")
                    {
                        if (hw == "exit") continue;
                        foreach (DriveInfo dI in DriveInfo.GetDrives())
                        {
                            Console.Write(
                                  "\t Drive: {0}\n\t" +
                                  " Disk format: {1}\n\t " +
                                  " Disk volume (GB): {2}\n\t Free volume (GB): {3}\n",
                                  dI.Name, dI.DriveFormat, (double)dI.TotalSize / 1024 / 1024 / 1024, (double)dI.AvailableFreeSpace / 1024 / 1024 / 1024);
                            Console.WriteLine();
                        }
                    }
                    else if (hw == "P" || hw == "p")
                    {
                        if (hw == "exit") continue;
                        Console.WriteLine("Processor capacity:  {0}", Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
                        Console.WriteLine("Model processor:  {0}", Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"));
                        Console.WriteLine("Quantity processors:  {0}", Environment.ProcessorCount);
                    }
                    else if (hw == "R" || hw == "r")
                    {
                        if (hw == "exit") continue;
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
                        Console.WriteLine("PhysicalMemory");
                        foreach (ManagementObject queryObj in searcher.Get())
                        {
                            Console.WriteLine(" Bank: {0} ; Capacity: {1} Gb; Speed: {2} ; SerialNumber: {3}; ", queryObj["BankLabel"], Math.Round(System.Convert.ToDouble(queryObj["Capacity"]) / 1024 / 1024, 2),
                                               queryObj["Speed"], queryObj["SerialNumber"]);
                        }
                    }

                }
                else if (i == "SW" || i == "sw")
                {
                    Console.WriteLine("Information about: Operating system (OS) | Name users (NU) | Nama machine (NM) | System directory (SD)");
                    string sw = Console.ReadLine();
                    if (sw == "OS" || sw == "os")
                    {
                        Console.WriteLine("Operating system (version number):  {0}", Environment.OSVersion);
                        Console.WriteLine("more information (M) or exit (E)");
                        string q = Console.ReadLine();
                        if (q == "M")
                            ShowSystemInfoFromWMI();
                        else continue;
                    }
                    if (sw == "NU" || sw == "nu")
                    {
                        Console.WriteLine("Name users: {0}", Environment.UserName);
                    }
                    if (sw == "NM" || sw == "nm")
                    {
                        Console.WriteLine("Name machine: {0}", Environment.MachineName);
                    }
                    if (sw == "SD" || sw == "sd")
                    {
                        Console.WriteLine("System directory: {0}", Environment.SystemDirectory);
                    }
                }
            }
            while (j != 0);
        }
        public static void ShowSystemInfoFromWMI()
        {
            StringBuilder systemInfo = new StringBuilder();
            ManagementClass manageClass = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection manageObjects = manageClass.GetInstances();
            PropertyDataCollection properties = manageClass.Properties;
            foreach (ManagementObject obj in manageObjects)
            {
                foreach (PropertyData property in properties)
                {
                    try
                    {
                        systemInfo.AppendLine(property.Name + ":  " +
                                        obj.Properties[property.Name].Value.ToString());
                    }
                    catch { }
                }
                systemInfo.AppendLine();
            }
            Console.WriteLine(systemInfo);
        }
    }
}