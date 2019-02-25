using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Geometry;

namespace termitesABMS_toolkitAlpha_r5
{
    public static class Util
    {
        static Random random = new Random();

        public static Point3d GetRandomPoint(double minX, double maxX, double minY, double maxY, double minZ, double maxZ)
        {
            double x = minX + (maxX - minX) * random.NextDouble();
            double y = minY + (maxY - minY) * random.NextDouble();
            double z = minZ + (maxZ - minZ) * random.NextDouble();

            return new Point3d(x, y, z);
        }

        public static Vector3d GetRandomUnitVectorXY()
        {

            double angle = 2.0 * Math.PI * random.NextDouble();

            double x = Math.Cos(angle);
            double y = Math.Sin(angle);

            return new Vector3d(x, y, 0.0);

        }

        public static Vector3d GetRandomUnitVector()
        {

            double phi = 2.0 * Math.PI * random.NextDouble();
            double theta = Math.Acos(2.0 * random.NextDouble() - 1.0);

            double x = Math.Sin(theta) * Math.Cos(phi);
            double y = Math.Sin(theta) * Math.Sin(phi);
            double z = Math.Cos(theta);

            return new Vector3d(x, y, z);

        }
        //TODO add a method to create folder structure
        public static void CreateFolderStructure(string FolderPath, bool CreateFolderStructure)
        {
            string folderPath = FolderPath;
            bool createFolderStructure = CreateFolderStructure;

            List<string> subFolderName =  new List<string>() { "00_resources", "01_runnable", "02_jarExportedGeometry", "03_analysisData", "04_archivedOutput", "05_GeometryVisualization", "06_Plots", "07_IVE" };
            double numberOfFolder = subFolderName.Count();
            string pathString ;

            for (int i = 0; i < numberOfFolder; i++)
            {              
                pathString = System.IO.Path.Combine(folderPath, subFolderName[i]);
                System.IO.Directory.CreateDirectory(pathString);
            }
            // To create a string that specifies the path to a subfolder under your 

        }

        public static void CreateFileOrFolder(string FolderName)
        {
            string folderName = FolderName;

            // To create a string that specifies the path to a subfolder under your 
            // top-level folder, add a name for the subfolder to folderName.
            string subFolderName = "04_TermiteArchive";
            string pathString = System.IO.Path.Combine(folderName, subFolderName);

            // You can write out the path name directly instead of using the Combine
            // method. Combine just makes the process easier.
            //string pathString2 = @"c:\Top-Level Folder\SubFolder2";

            // You can extend the depth of your path if you want to.
            //pathString = System.IO.Path.Combine(pathString, "SubSubFolder");

            // Create the subfolder. You can verify in File Explorer that you have this
            // structure in the C: drive.
            //    Local Disk (C:)
            //        Top-Level Folder
            //            SubFolder
            System.IO.Directory.CreateDirectory(pathString);
            // Create a file name for the file you want to create with a particular name. 
            //string fileName = "MyNewFile.txt";
            string fileName = "TermitesSystemCheckTestFile.txt";

            // Use Combine again to add the file name to the path.
            pathString = System.IO.Path.Combine(pathString, fileName);

            // Verify the path that you have constructed.
            Console.WriteLine("Path to my file: {0}\n", pathString);

            // Check that the file doesn't already exist. If it doesn't exist, 
            // create the file and write A SIMPLE MESSAGE TO IO
            // DANGER: System.IO.File.Create will overwrite the file if it already exists.
            // This could happen even with random file names, although it is unlikely.
            if (!System.IO.File.Exists(pathString))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                {
                    //write here the DATA in the file
                    string firstMsg = "this is the first message from Temite Systme";
                    foreach (byte a in firstMsg)
                    {
                        fs.WriteByte(a);
                    }
                }
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }

            // Read and display the data from your file.
            try
            {
                byte[] readBuffer = System.IO.File.ReadAllBytes(pathString);
                foreach (byte b in readBuffer)
                {
                    Console.Write(b + " ");
                }
                Console.WriteLine();
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }


        }

    }
}