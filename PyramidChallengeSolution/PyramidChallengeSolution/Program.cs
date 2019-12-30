using PyramidChallengeSolution.Helpers;
using PyramidChallengeSolution.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PyramidChallengeSolution
{
    class Program
    {
        private static string questionFileName = "Question.txt";
        static void Main(string[] args)
        {
            var pyramid = FileHandler.GetPyramidFromFile(questionFileName);
            FileHandler.WritePyramid(pyramid);
            var questionHandler =  new QuestionHandler(pyramid);
            questionHandler.BuildDirectedGraph();
            var path = questionHandler.GetMaxValuePath();
            ShowResult(path);

            Console.ReadLine();
        }

        private static void ShowResult(List<Node> nodes)
        {
            Console.WriteLine("=============Result===========");
            Console.WriteLine($"Max sum: {nodes.Select(node =>node.Value).ToList().Sum()}" );
            Console.WriteLine($"Path: {string.Join(",", nodes.Select(node => node.Value).ToList()) }" );
        }
    }
}
