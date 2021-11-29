namespace BluebeamComSht
{
    /// <inheritdoc/>
    public class Reply : Row
    {
        /// <summary> Gets or sets the id of the parent row.</summary>
        public string ParentID { get; protected set; }

        /// <summary>
        /// Initialises a new instance of<see cref="Reply"/> class.
        /// </summary>
        /// <param name="id">The identifier for the response.</param>
        /// <param name="parent">The id of the parent row.</param>
        /// <param name="pageLabel">The associated page reference for the parent comment.</param>
        /// <param name="status">The status history for actions relating to the response.</param>
        /// <param name="date">The date the response was last updated.</param>
        /// <param name="author">The author of the response.</param>
        /// <param name="comment">The contents of the response.</param>
        /// <param name="response">The contents of manually inputted responses made outside of Bluebeam's Reply function.</param>
        public Reply(string id, string parent, string pageLabel, string status, string date, string author, string comment, string response)
        {
            this._id = id;
            this.ParentID = parent;
            this.PageLabel = pageLabel;
            this.Status = status; //Should be empty for replies if review process was followed correctly
            this.Date = date;
            this.Author = author;
            this.Comment = comment;
            this.Response = response;
            
            if (response == "")
            {
                this.Response = ConsolidateResponse();
            }
            else
            {
                this.Response += "\n\n" + ConsolidateResponse();
            }
            
        }

        /// <summary>
        /// Consoliates the key values from the reply row in a format suitable to be append to the parent row's Response column.
        /// </summary>
        /// <returns>Concatenated string.</returns>
        public string ConsolidateResponse()
        {
            string concatResponse = Date + " - " + Author + "\n";
            concatResponse += Comment;

            return concatResponse;
        }
    }
}
