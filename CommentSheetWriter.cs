using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BluebeamComSht
{
    /// <summary>
    /// Class <c>CommentSheetWriter</c> writes new comment summary.
    /// </summary>
    public static class CommentSheetWriter
    {
        /// <summary>
        /// Writes list to new file.
        /// </summary>
        /// <param name="export">The new csv file name.</param>
        /// <param name="comments">The list of comment objects.</param>
        public static void Write(string export, List<Comment> comments)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(">>> Comment sheet to be written into: " + export);
            Console.ResetColor();

            System.Diagnostics.Contracts.Contract.Requires(comments != null);
            StreamWriter writer = new StreamWriter(export);
            CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(comments);
            writer.Dispose();
        }
    }
}
