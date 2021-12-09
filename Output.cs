using System;
using System.Drawing;

namespace AutoTest
{
    class Output
    {
        //This is the default location
        private String path = "C:\\Users\\Administrator\\PICS Telecom\\Sandstone Technologies - Documents\\Engineering Documents\\Test Reports\\Platform Test Reports";

        private String directory = "";

        //Set the directory to save test reports in.
        //Accessed Via Menu Bar
        public bool setDirectory(String newdir)
        {
            if (newdir.Length > 30000) { Console.WriteLine("Invalid Directory Name"); return false; }
            if (!System.IO.Directory.Exists(newdir)) { Console.WriteLine("Directory Does Not Exist"); return false; }
            path = newdir;
            return true;
        }

        //Gets the full path that is currently set
        public String getDirectory()
        {
            return path + "\\" + directory;
        }

        //Might need to do more checking here.
        //Create the PO/SO folder for the test.
        //If the directory already exists it will just use that one
        public bool createDirectory(String newdir)
        {
            if (newdir.Length > 255) { Console.WriteLine("Invalid Directory Name"); return false; }
            String fulldir = System.IO.Path.Combine(path, newdir);
            if (System.IO.Directory.Exists(fulldir))
            {
                directory = newdir;
            } else
            {
                System.IO.Directory.CreateDirectory(fulldir);
                directory = newdir;
            }
            return true;
        }

        //Checks if a directory has been set
        public bool checkDirectory()
        {
            if (directory.Equals("")){
                return false;
            }
            return true;
        }

        //Saves an image to the currently set directory as a png.
        //Creates a folder for the part number of the report unless one is already created.
        public void saveReport(Image image, String part, String serial)
        {
            String fulldir = path + "\\" + directory + "\\" + part;
            if (!System.IO.Directory.Exists(fulldir))
            {
                System.IO.Directory.CreateDirectory(fulldir);
            }
            //To change the file type: Change the extension and the ImageFormat
            image.Save(fulldir + "\\" + part + "_" + serial + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
