namespace BluebeamComSht
{
    /// <inheritdoc/>
    public class Comment : Row
    {      
        /// <summary>
        /// Initialises a new instance of<see cref="Comment"/> class.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <param name="pageLabel">The associated page reference for the comment.</param>
        /// <param name="status">The status history for actions relating to the comment.</param>
        /// <param name="date">The date the main comment was last updated.</param>
        /// <param name="author">The author of the comment.</param>
        /// <param name="comment">The contents of the comment.</param>
        /// <param name="response">The subsequent replies to the main comment.</param>
        public Comment(string id, string pageLabel, string status, string date, string author, string comment, string response)
        {
            this._id = id;
            this.PageLabel = pageLabel;
            this.Status = status;
            this.Date = date;
            this.Author = author;
            this.Comment = comment;
            this.Response = response;
        }
    }
}
