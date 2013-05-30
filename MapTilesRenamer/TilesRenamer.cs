using System;
using System.IO;

namespace MapTilesRenamer
{
    /// <summary>
    /// Provides method for easy switching between TMS/Slippy map file naming conventions
    /// </summary>
    public class TilesRenamer
    {
        private string sourceExtension;
        private string destExtension;

        public TilesRenamer(string sourceExt, string destExt)
        {
            this.sourceExtension = sourceExt;
            this.destExtension = destExt;
        }

        /// <summary>
        /// Renames y file coordinate by calculate:
        /// yNew = 2^zoom - yOld - 1
        /// using \zoom\x\y.png catalog structure
        /// </summary>
        public void RenameAll()
        {
            DirectoryInfo[] zoomDirs = new DirectoryInfo(Environment.CurrentDirectory).GetDirectories();
            foreach (DirectoryInfo zoomDir in zoomDirs)
            {
                int zoom = int.Parse(zoomDir.Name);

                DirectoryInfo[] xDirs = zoomDir.GetDirectories();
                foreach (DirectoryInfo xDir in xDirs)
                {
                    FileInfo[] yFiles = xDir.GetFiles();
                    RenameYFiles(zoom, yFiles);
                }
            }
        }

        #region Private methods
        private void RenameYFiles(int zoom, FileInfo[] yFiles)
        {
            foreach (FileInfo yFile in yFiles)
            {
                if (yFile.Name.EndsWith(sourceExtension))
                {
                    string newName = ComputeYCoordinate(zoom, yFile) + destExtension;
                    File.Move(Path.Combine(yFile.DirectoryName, yFile.Name), Path.Combine(yFile.DirectoryName, newName));
                }
            }
        }

        private string ComputeYCoordinate(int zoom, FileInfo yFile)
        {
            int oldCoord = int.Parse(yFile.Name.Replace(sourceExtension, null));
            int newCoord = (int)(Math.Pow(2, zoom) - oldCoord - 1);
            return newCoord.ToString();
        }
        #endregion
    }
}
