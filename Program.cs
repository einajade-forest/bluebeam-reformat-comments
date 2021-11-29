using System;
using System.Collections.Generic;

namespace BluebeamComSht
{
    class Program
    {
        /// <summary>
        /// Starts the console application and obtains import file details.
        /// </summary>
        static void Main()
        {
            //Application header for console and initial instruction
            Console.WriteLine(Properties.Resources.intro);

            string importFile = Console.ReadLine();
            List<Comment> reformattedCommentSheet = CsvImporter.ImportComments(importFile);
            
            //Request input for file name of the reformatted comment sheet
            Console.WriteLine(Properties.Resources.newFileName);

            //Use input as file name
            CommentSheetWriter.Write($"Export/{Console.ReadLine()}.csv", reformattedCommentSheet);
            Console.WriteLine(Properties.Resources.complete);

            //Warn that file needs to be resaved as Excel spreadsheet to retain sophisticated formatting changes after opening
            Console.WriteLine(Properties.Resources.note);
            Console.WriteLine(Properties.Resources.exit);
            Console.ReadKey();
        }
    }
}
