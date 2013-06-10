using System;
using System.IO;

namespace MapTilesRenamer
{
    /// <summary>
    /// Provides method for easy switching between TMS/Slippy map file naming conventions
    /// </summary>
    public class SlippyTMSRenamer : TilesRenamer
    {
        private string path;
        
        public SlippyTMSRenamer(string sourceExt, string destExt, string path) : base(sourceExt, destExt) 
        {
            this.path = path ?? Environment.CurrentDirectory;
        }
        
        /// <summary>
        /// Renames y file coordinate by calculate:
        /// yNew = 2^zoom - yOld - 1
        /// using \zoom\x\y.png catalog structure
        /// </summary>   
        public override void RenameAll()
        {
            
            DirectoryInfo[] zoomDirs = new DirectoryInfo(path).GetDirectories();
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
