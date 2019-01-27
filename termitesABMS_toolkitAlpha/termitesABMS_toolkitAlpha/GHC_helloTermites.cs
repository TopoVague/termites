using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace termitesABMS_toolkitAlpha
{
    public class GHC_helloTermites : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public GHC_helloTermites()
          : base("hello Termites", "hello",
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
            
            pManager.AddGenericParameter("newProjectFilePath", "newProjectFilePath", "This the file Path in your PC where the output data of your project will be stored", GH_ParamAccess.item );
            

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("NewProjectFilePath", "NewProjectFilePath", "This is where all your data will be stored", GH_ParamAccess.item);
            pManager.AddTextParameter("info", "info", "Wellcome to the World of Termites", GH_ParamAccess.item);
            pManager.AddTextParameter("DefaultProjectFilePath", "DefaultProjectFilePath", "This is where all your data will be stored if you do not provide a filepath", GH_ParamAccess.item);
            
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            
            //call environment to get a local folder
            string defaultFilePath = Environment.GetEnvironmentVariable("ALLUSERSPROFILE");
            // write a messsage 
            string fistMsg = "Hello to the Termite World. Add a panel to see the file path of the default folder where your data will stored";
            string iFilePath = "empty";

            string defaultProjectFolderName = "TermitesProjectFolder";

            bool successfulInput = DA.GetData(0,ref iFilePath);
            if (successfulInput ==true)
            {
                DA.SetData(0, iFilePath+ defaultProjectFolderName);
                DA.SetData(1, fistMsg);
                DA.SetData(2, defaultFilePath+ defaultProjectFolderName);
                Util.CreateFileOrFolder(iFilePath + defaultProjectFolderName);
                
            }
            else
            {
                DA.SetData(0, defaultFilePath+ "\\" + defaultProjectFolderName);
                DA.SetData(1, fistMsg);
                DA.SetData(2, defaultFilePath);
                Util.CreateFileOrFolder(defaultFilePath + "\\"+ defaultProjectFolderName);
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
                return Properties.Resources.termitesGhxIcon_temp_01;
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
            get { return new Guid("e3364d12-4db3-4b12-bb14-e4a4bd321e81"); }
        }
    }
}