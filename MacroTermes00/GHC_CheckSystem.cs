using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace macroTermes00
{
    public class GHC_CheckSystem : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_CheckSystem class.
        /// </summary>
        public GHC_CheckSystem()
          : base("helloCicada", "hello",
              "This is the component that checks if necessary files exist  on your system",
              "CicadaToolkit", "Essentials")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("filePath", "sourceFiles", "File path to folder that contains necessary source files", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("output Message", "Output", "This is a message regarding if all the necessary files exist", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string filePath = "";
            string outputMessage = "sucess!!!!";
            
            bool success1= DA.GetData(0, ref filePath);

            if (success1)
            {
                DA.SetData(0, outputMessage);
            }else
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Provide a valid filepth, Mr Designer !"); 
            }
            

        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                //return Bitmap  ("C:\\Users\\Evangelos\\Documents\\GitHub\\CicadaToolkit\\Cicada_alpha\\cicada00\\cicada00\\icon\\cicadaGhxIcon_temp-02.png");
                return Properties.Resources.ca1;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("16945706-e68e-469f-bfe5-40d6edb0a4dc"); }
        }
    }
}