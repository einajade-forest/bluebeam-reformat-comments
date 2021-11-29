namespace BluebeamComSht
{
    /// <summary>
    /// Storage of column index information.
    /// </summary>
    public class ColumnIndexes
    {
        //Initial values of indexes, to be updated when stream is read
        public int IdIndex = -1;
        public int ParentIndex = -1;
        public int PageLabelIndex = -1;
        public int StatusIndex = -1;
        public int DateIndex = -1;
        public int AuthorIndex = -1;
        public int CommentsIndex = -1;
        public int ResponseIndex = -1;

        /// <summary>
        /// Initialises a new instance of<see cref="ColumnIndexes"/> class.
        /// </summary>
        public ColumnIndexes() { }

    }
}
