using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoWGenerator
{
    class CSV2PNML
    {
        public CSV2PNML(string csvPath, string savePath)
        {
            /*
            string[] splitCSVPath = csvPath.Split('\\');
            string csvName = splitCSVPath[(splitCSVPath.Length) - 1];
            */

            string csvLine;

            string[] splitLine;

            try
            {
                int line = -1;
                StreamReader csvReader = new StreamReader(csvPath);

                csvLine = csvReader.ReadLine();
                while (csvLine != null)
                {

                    if (line > -1)
                    {
                        //splitLine = csvLine.Split(',');

                        Writer writer = new Writer();
                        writer.Initiate(csvLine, savePath);

                    }
                    csvLine = csvReader.ReadLine();
                    line++;
                }
                csvReader.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
