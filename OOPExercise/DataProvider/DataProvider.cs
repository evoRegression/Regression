using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProvider
{
    public class DataProvider : IDataProvider
    {
        private int[] data;
        public double Mean()
        {
            // Nagyon jó korai visszatérés és null érték lekezelés
            if (this.data == null || this.data.Length == 0)
                throw new Exception("Data array is empty"); // Ne dobjunk általános Exception-t helyette ArgumentNullException

            return (double)this.data.Sum() / this.data.Length;
        }

        public double Median()
        {
            if (this.data == null || this.data.Length == 0)
                throw new Exception("Data array is empty");
            int[] sortedData = (int[])this.data.Clone(); // Remek megoldás, amit hiányoltam Balázs megoldásába azt itt benne van. Érdemes mindig megtartani az eredeti adatot. 
            int midIndex = sortedData.Length / 2 + 1;
            if( sortedData.Length % 2 == 0)
            {
                return (sortedData[midIndex] + sortedData[midIndex - 1]) / 2.0;
            }
            else
            {
                return sortedData[midIndex];
            }
        }

        public int[] Read(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            List<int> dataList = new List<int>();
            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    // Továbbra is nagyon jó, hogy lekezeled az exception-t. Az int.Parse lehet érdemes lehet TryParse metódust használni.
                    // A TryParse visszatér egy bool-al (true,false) és aszerint lehet tovább dolgozni. Az teljesítmény kritikus rendszereknél az Exception dobás erőforrásigényes.
                    dataList.Add(int.Parse(lines[i])); 
                }
                catch (FormatException)
                {
                    throw new Exception($"Incorrect Data in \"{ filePath}\" file at line: {i+1} : {lines[i]}");
                }
            }
            this.data = dataList.ToArray();
            return data;
        }

        public double StandardDeviation()
        {
            return Math.Sqrt(this.Variance());
        }

        public double Variance()
        {
            // Üss bátran enter-t nem baj ha több a kódsor. Lényeg, hogy átlátható legyen. A logikailag összetartozó blokkok közé nyugodtan beékkelhetsz egy enter-t.
            if (this.data == null || this.data.Length == 0)
                throw new Exception("Data array is empty");
            double mean = this.Mean();
            double sum = 0;
            // this.data nem muszály kiírni a this-t. Az i változó helyett beszédesebb nevet érdemes választani pl. singleDatapoint, aDatapoint.
            foreach (int i in this.data)
            {
                sum += Math.Pow(i - mean, 2.0);
            }
            return sum / this.data.Length;
        }
    }
}
