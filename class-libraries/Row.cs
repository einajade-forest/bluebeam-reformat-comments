namespace BluebeamComSht
{
    /// <summary>
    /// Represents a comment or response made within a PDF review session.
    /// </summary>
    public abstract class Row
    {
        //Field
        public string _id;

        //Properties
        //Selected columns, in order of presentation, for the reformatted comment sheet
        
        /// <summary> Gets or sets the page label of the row.</summary>
        public string PageLabel { get; set; }
        /// <summary> Gets or sets the date of when the comment has last been edited in Bluebeam.</summary>
        public string Date { get; set; }
        /// <summary> Gets or sets the author of the comment.</summary>
        public string Author { get; set; }
        /// <summary> Gets or sets the content of the comment made.</summary>
        public string Comment { get; set; }
        /// <summary> Gets or sets the status applied to the markup.</summary>
        public string Status { get; set; }
        /// <summary> Gets or sets the response to the original comment.</summary>
        public string Response { get; set; }
    }
}
