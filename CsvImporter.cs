using System;
using System.Collections.Generic;
using System.IO;

namespace BluebeamComSht
{
    /// <summary>
    /// Class <c>CsvImporter</c> imports csv file for reading and differentiates between <see cref="Comment"/> and <see cref="Reply"/> rows for the purpose of reformatting the comment summary sheet.
    /// </summary>
    public static class CsvImporter
    {
        /// <summary>
        /// Imports csv file and returns the result.
        /// </summary>
        /// <param name="file">The file path and name of csv file as initially provided by user.</param>
        /// <returns>A list of comments.</returns>
        /// 
        public static List<Comment> ImportComments(string file)
        {
            string currentFile = file; //In the event of error, new variable will allow for file path to be updated.

            var comments = new List<Comment>();
            var replies = new List<Reply>();
            var indexes = new ColumnIndexes();
            var retry = 0;

            do
            {
                try
                {
                    retry++;
                    //instantiate StreamReader
                    var sr = new StreamReader(currentFile);

                    #region Check current available headers in imported file
                    var columnHeaders = new List<string>();

                    string heading = sr.ReadLine();
                    string[] headings = heading.Split(',');

                    //Convert headings array to a list so that it may be edited in the event that there is no pre-exisiting Response column
                    for (int i = 0; i < headings.Length; i++)
                    {
                        columnHeaders.Add(headings[i]);
                    }

                    //The following variables align with the required out-of-the-box column headings
                    indexes.IdIndex = columnHeaders.IndexOf("ID");
                    indexes.ParentIndex = columnHeaders.IndexOf("Parent");
                    indexes.PageLabelIndex = columnHeaders.IndexOf("Page Label");
                    indexes.StatusIndex = columnHeaders.IndexOf("Status");
                    indexes.DateIndex = columnHeaders.IndexOf("Date");
                    indexes.AuthorIndex = columnHeaders.IndexOf("Author");
                    indexes.CommentsIndex = columnHeaders.IndexOf("Comments");

                    #region Test and add mandatory column names to missingHeaders list
                    //Check that all required headers were exported from Bluebeam
                    var missingHeaders = new List<string>();
                                        
                    if (indexes.IdIndex < 0)
                    {
                        missingHeaders.Add("ID");
                    }
                    if (indexes.ParentIndex < 0)
                    {
                        missingHeaders.Add("Parent");
                    }
                    if (indexes.PageLabelIndex < 0)
                    {
                        missingHeaders.Add("Page Label");
                    }
                    if (indexes.StatusIndex < 0)
                    {
                        missingHeaders.Add("Status");
                    }
                    if (indexes.DateIndex < 0)
                    {
                        missingHeaders.Add("Date");
                    }
                    if (indexes.AuthorIndex < 0)
                    {
                        missingHeaders.Add("Author");
                    }
                    if (indexes.CommentsIndex < 0)
                    {
                        missingHeaders.Add("Comments");
                    }

                    //Display alert if any required columns are missing
                    if (missingHeaders.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nERROR: Mandatory column(s) has/have not been imported from Bluebeam.\nPlease re-import the CSV Summary with " + string.Join(", ", missingHeaders) + " column(s).");
                        Console.ResetColor();

                        Console.WriteLine(Properties.Resources.exit);
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    #endregion

                    //Check for custom "Response" column - may already exist due to user's Bluebeam Profile customisations
                    if (columnHeaders.Contains("Response") != true)
                    {
                        columnHeaders.Add("Response");
                    }
                    
                    indexes.ResponseIndex = columnHeaders.IndexOf("Response");
                    #endregion

                    #region Read Remaining Rows
                    while (sr.EndOfStream == false)
                    {
                        string line = sr.ReadLine();
                        string[] row = line.Split(','); //NOTE: Will cause issues if there are commas in the comment cell

                        string id = row[indexes.IdIndex];
                        string parent = row[indexes.ParentIndex];
                        string pageLabel = row[indexes.PageLabelIndex];
                        string status = row[indexes.StatusIndex];
                        string date = row[indexes.DateIndex];
                        string author = row[indexes.AuthorIndex];
                        string comment = row[indexes.CommentsIndex];
                        string response = "";

                        //Capture response data, if already exists in the imported file. Otherwise, skip step.
                        if (indexes.ResponseIndex < row.Length)
                        {
                            response = row[indexes.ResponseIndex];
                        }

                        //Identify and instantiate rows
                        if (parent == "")
                        {
                            var commentRow = CreateComment(id, pageLabel, status, date, author, comment, response);
                            comments.Add(commentRow);
                        }
                        else
                        {
                            var replyRow = CreateReply(id, parent, pageLabel, status, date, author, comment, response);
                            replies.Add(replyRow);                            
                        }
                    }
                    #endregion
                    
                    sr.Dispose();

                    #region Tidy Replies prior to appending to appropriate Comment
                    //Run through the list of replies to consolidate related replies
                    foreach (Reply r in replies)
                    {
                        int parentReply = replies.FindIndex(x => x._id.Contains(r.ParentID));
                        if (parentReply != -1)
                        {
                            (replies[parentReply]).Response += "\n\n" + r.Response;
                        }
                    }

                    //Run through list of replies again to match up to original comment
                    foreach (Reply rr in replies)
                    {
                        //Find row where ID matches Parent
                        int originalComment = comments.FindIndex(y => y._id.Contains(rr.ParentID));

                        if (originalComment != -1)
                        {
                            //Consolidate relevant information and append to the Response column of the original row
                            if (comments[originalComment].Response == "")
                            {
                                comments[originalComment].Response = rr.Response;
                            }
                            else
                            {
                                comments[originalComment].Response += "\n\n" + rr.Response;
                            }
                        }
                    }
                    #endregion

                    break;
                }
                catch (IOException error) //Use of base class exception type to cover both directory and file not found exceptions
                {
                    if (retry == 0)
                    {
                        throw;
                    }

                    //Advise that files is not found
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nERROR:" + error.Message + "\n");
                    Console.WriteLine(Properties.Resources.fileError);
                    Console.ResetColor();

                    //Allow option to retry import
                    Console.WriteLine(Properties.Resources.retry);

                     string input = Console.ReadLine();
                    if (input != "1")
                    {
                        currentFile = input;
                        retry = 0;
                    }
                }
            } while (true);
            
            return comments;
        }

        /// <summary>
        /// Instantiates review comment object.
        /// </summary>
        /// <param name="id">Distinct row identifier.</param>
        /// <param name="pageLabel">The associated page reference for the comment.</param>
        /// <param name="status">The status history for actions relating to the comment.</param>
        /// <param name="date">The date the comment was last updated.</param>
        /// <param name="author">The author of the comment.</param>
        /// <param name="comment">The contents of the comment.</param>
        /// <param name="response">The subsequent replies to the main comment.</param>
        /// <returns>The comment for the distinct identifier.</returns>
        public static Comment CreateComment(string id, string pageLabel, string status, string date, string author, string comments, string response)
        {
            var comment = new Comment(id, pageLabel, status, date, author, comments, response);
            return comment;
        }

        /// <summary>
        /// Instantiates comment response object.
        /// </summary>
        /// <param name="id">Distinct row identifier.</param>
        /// <param name="parent">Identifier of parent row.</param>
        /// <param name="pageLabel">The associated page reference for the parent row.</param>
        /// <param name="status">The status history for actions relating to the comment response.</param>
        /// <param name="date">The date the response was last updated.</param>
        /// <param name="author">The author of the response.</param>
        /// <param name="comment">The contents of the response.</param>
        /// <param name="response">The contents of manually inputted responses made outside of Bluebeam's Reply function.</param>
        /// <returns>The response for the distinct identifier.</returns>
        public static Reply CreateReply(string id, string parent, string pageLabel, string status, string date, string author, string comments, string response)
        {
            var reply = new Reply(id, parent, pageLabel, status, date, author, comments, response);
            return reply;
        }
    }
}
