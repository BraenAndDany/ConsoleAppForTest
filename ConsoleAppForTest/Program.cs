using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreHardwareMonitor.Hardware;

namespace test
{
    class Programm
    {
        static CpuInfo GetCpuInfo()
        {
            Computer computer = new Computer
            {
                IsCpuEnabled = true // Включаем мониторинг CPU
            };
            computer.Open(); // Начинаем мониторинг

            CpuInfo cpuInfo = new CpuInfo();

            foreach (var hardwareItem in computer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.Cpu)
                {
                    hardwareItem.Update(); // Обновляем информацию о CPU
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            cpuInfo.Temperature = sensor.Value.HasValue ? sensor.Value.Value : 0;
                        }
                        else if (sensor.SensorType == SensorType.Load)
                        {
                            cpuInfo.Load = sensor.Value.HasValue ? sensor.Value.Value : 0;
                        }
                    }
                    cpuInfo.Name = hardwareItem.Name;
                }
            }

            computer.Close(); // Заканчиваем мониторинг
            return cpuInfo;
        }

        public class CpuInfo
        {
            public double Temperature { get; set; }
            public double Load { get; set; }
            public string Name { get; set; }
        }

        public void ain()
        {
            string StringCpuInfo = "";
            var cpuInfo = GetCpuInfo();
            Console.WriteLine("Температура процессора: {0} °C", cpuInfo.Temperature);
            Console.WriteLine("Загрузка процессора: {0} %", cpuInfo.Load);
            Console.WriteLine("Название процессора: {0}", cpuInfo.Name);
            StringCpuInfo = "Загрузка процессора: " + cpuInfo.Load + "%              Температура процессора: " + cpuInfo.Temperature;

            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter("C:\\Users\\User\\Desktop\\Новая папка (2)\\Новая папка\\Test.txt", true, Encoding.ASCII);
            //Write a line of text
            sw.WriteLine(cpuInfo.Load);
            //Write a second line of text
            sw.WriteLine(cpuInfo.Temperature);
            //Close the file
            sw.Close();

            int j = 1;

            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Users\\User\\Desktop\\Новая папка (2)\\Новая папка\\Test{0}.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                int i = 1;
                double termal = 0;
                double percent = 0;
                while (line != null)
                {
                    if (i % 2 == 0) {
                    termal += Convert.ToDouble(line);
                    }
                    else
                    {
                    percent += Convert.ToDouble(line);
                    }
                    //Read the next line
                    line = sr.ReadLine();
                    i++;
                }
                Console.WriteLine("Средняя температура: {0} °C", termal / (i / 2));
                Console.WriteLine("Средняя загрузка ЦП: {0} %", percent / (i / 2));
                //close the file
                sr.Close();

                StreamWriter sw2 = new StreamWriter("C:\\Users\\User\\Desktop\\Новая папка (2)\\Test.txt");
                //Write a line of text
                sw2.WriteLine("Средняя температура: {0} °C", termal / (i / 2));
                //Write a second line of text
                sw2.WriteLine("Средняя загрузка ЦП: {0} %", percent / (i / 2));
                //Close the file
                sw2.Close();

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        static void Main(string[] args)
        {
            Programm Info = new Programm();
            Info.ain();
        }
    }
}