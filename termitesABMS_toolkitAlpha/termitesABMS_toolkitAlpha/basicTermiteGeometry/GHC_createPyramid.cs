using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace termitesABMS_toolkitAlpha.basicTermiteGeometry
{
    public class GHC_createPyramid : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_createPyramid class.
        /// </summary>
        public GHC_createPyramid()
          : base("GHC_createPyramid", "Pyramid",
              "Create A basic Pyramid",
               "Termites", "5 | Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Base PLane", "Base Plane", "Base PLane", GH_ParamAccess.item, Plane.WorldXY);
            pManager.AddNumberParameter("Height", "Height", "Height", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("Width", "Width", "Width", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("Length", "Length", "Length", GH_ParamAccess.item, 1.0);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Pyramid", "Pyrmd", " Pyramid", GH_ParamAccess.item);


        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            Plane iBasePlane = Plane.Unset;
            double iHeight = double.NaN;
            double iWidth = double.NaN;
            double iLength = double.NaN;

            DA.GetData(0, ref iBasePlane);
            DA.GetData(1, ref iHeight);
            DA.GetData(2, ref iWidth);
            DA.GetData(3, ref iLength);

            Pyramid pyramid = new Pyramid(iBasePlane, iLength, iHeight, iWidth);

            DA.SetData("Pyramid", pyramid);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("7e2dd318-575d-4e70-8fc8-5b4987cca00c"); }
        }
    }
}