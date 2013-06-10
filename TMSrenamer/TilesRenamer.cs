namespace MapTilesRenamer
{
    /// <summary>
    /// Abstract class for supporting different conventions.
    /// </summary>
    public abstract class TilesRenamer
    {
        protected string sourceExtension;
        protected string destExtension;

        protected TilesRenamer(string sourceExt, string destExt)
        {
            this.sourceExtension = sourceExt;
            this.destExtension = destExt;
        }

        public abstract void RenameAll();
    }
}
