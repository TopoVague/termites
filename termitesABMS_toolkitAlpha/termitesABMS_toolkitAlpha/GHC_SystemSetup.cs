using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace termitesABMS_toolkitAlpha.Properties
{
    public class GHC_SystemSetup : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_SystemSetup class.
        /// </summary>
        public GHC_SystemSetup()
         : base("SystemSetup", "hello",
              "This is a test Grasshopper Component of Termites",
              "Termites", "0 | Termite System")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {

            //Default FilePath 

            pManager.AddGenericParameter("ProjectFolderFilePath", "ProjectFolderFilePath", "This the file Path in your PC where the output data of your project will be stored", GH_ParamAccess.item);
            pManager.AddNumberParameter("NoOfOutputs", "NoOfOutputs", "NoOfOutputs", GH_ParamAccess.item, 1.0);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("ProjectFolderFilePath", "ProjectFolderFilePath", "We created a folder structure to keep all the data properly ", GH_ParamAccess.item);
            pManager.AddNumberParameter("NoOfOutputs", "NoOfOutputs", "NoOfOutputs", GH_ParamAccess.item);
            pManager.AddTextParameter("info", "info", "Info about your system setup", GH_ParamAccess.item);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            //callenvironment to get a local folder
            string defaultFilePath = Environment.GetEnvironmentVariable("ALLUSERSPROFILE");
            string iFilePath = "empty";
            double iNoOfOutputs = 1.0;
            string fistMsg = "A folder structure has been created in your project folder ";
            //a default name for the project folder
            string defaultProjectFolderName = "TermitesProjectFolder";

            bool successfulInput = DA.GetData("ProjectFolderFilePath", ref iFilePath);
            bool successfulInput2 = DA.GetData("NoOfOutputs", ref iNoOfOutputs);
            
            if (successfulInput == true && successfulInput2 == true)
            {
               
                DA.SetData(0, iFilePath+defaultProjectFolderName);
                DA.SetData("NoOfOutputs", iNoOfOutputs);
                DA.SetData("info", fistMsg);
                Util.CreateFolderStructure(iFilePath + "TermitesProjectFolder", iNoOfOutputs);
                //Util.CreateFileOrFolder(iFilePath + "TermitesProjectFolder");
                
            }
            else if (successfulInput == true && successfulInput2 == false)
            {
                DA.SetData(0, iFilePath+defaultProjectFolderName);
                double defaultOutputNo = 1.0;
                DA.SetData(1, (int) defaultOutputNo);
                DA.SetData(2, fistMsg);
                Util.CreateFolderStructure(iFilePath, 1);
                //Util.CreateFileOrFolder(iFilePath + "TermitesProjectFolder");
            }
            else if (successfulInput == false && successfulInput2 == true)
            {
                DA.SetData(0, defaultFilePath+defaultProjectFolderName);
                DA.SetData(1, iNoOfOutputs);
                DA.SetData(2, fistMsg);

                Util.CreateFolderStructure(defaultFilePath , 1);
               // Util.CreateFileOrFolder(defaultFilePath + "\\TermitesProjectFolder");
            }
            else
            {
                DA.SetData(0, defaultFilePath);
                DA.SetData(1, 1);
                DA.SetData(2, fistMsg);
                // Util.CreateFolderStructure(defaultFilePath , 1);
                Util.CreateFileOrFolder(defaultFilePath + "\\TermitesProjectFolder");
            }
  
          

        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                //return new System.Drawing.Bitmap("C:\\Users\\Evangelos\\Documents\\GitHub\\CicadaToolkit\\Cicada_alpha\\cicada00\\cicada00\\icon\\cicadaGhxIcon_temp-03.png");
                return Properties.Resources.termitesGhxIcon_temp_11;
                //return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e3364d12-4db3-4b12-bb14-e4a4bd321e82"); }
        }
    }
}