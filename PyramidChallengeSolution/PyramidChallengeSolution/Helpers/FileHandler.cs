using PyramidChallengeSolution.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PyramidChallengeSolution.Helpers
{
    public class FileHandler
    {
        public static Pyramid GetPyramidFromFile(string fileName)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Questions\\{fileName}");
            string lineContent;
            int lineNumber = 0;
            List<Level> levels = new List<Level>();
            StreamReader file = new StreamReader(path);

            //compose a pyramid that contains levels and nodes
            while ((lineContent = file.ReadLine()) != null)
            {
                //Console.WriteLine(lineContent);
                var nodes = lineContent.Split(' ').Select(x => new Node { Value = Convert.ToInt64(x) }).ToList();
                levels.Add(new Level { Nodes = nodes, LevelNo = lineNumber} );
                lineNumber++;
            }

            file.Close();

            return new Pyramid { Levels = levels };
        }

        public static void WritePyramid(Pyramid pyramid)
        {
            foreach (var level in pyramid.Levels)
            {
                foreach (var node in level.Nodes)
                {
                    Console.Write(node.Value + " ");
                }
                Console.WriteLine(" ");
            }
        }
    }
}
