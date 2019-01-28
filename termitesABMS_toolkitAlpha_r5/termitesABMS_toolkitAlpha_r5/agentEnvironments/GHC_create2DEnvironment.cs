using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace termitesABMS_toolkitAlpha_r5.agentEnvironments
{
    public class GHC_create2DEnvironment : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_environment class.
        /// </summary>
        /// 
        public GHC_create2DEnvironment()
          : base("GHC_Create2dEnvironment", "Create 2d environment",
              "This is a Component to create a generic 2d rectangular environment to run agents in",
              "Termites", "2 | Environments")
        {
        }
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Base PLane", "Base Plane", "Base PLane", GH_ParamAccess.item, Plane.WorldXY);
            pManager.AddNumberParameter("Width", "Width", "Width", GH_ParamAccess.item, 50.0);
            pManager.AddNumberParameter("Height", "Height", "Height", GH_ParamAccess.item, 50.0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("2dEnvironment", "2dEnvironment", " 2dEnvironment", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            Plane iBasePlane = Plane.Unset;
            double iWidth = double.NaN;
            double iHeight = double.NaN;
            double iDepth = 0.0;

            DA.GetData(0, ref iBasePlane);
            DA.GetData(1, ref iHeight);
            DA.GetData(2, ref iWidth);

            AgentEnvironment Env2d = new AgentEnvironment(iBasePlane, iWidth, iHeight, iDepth);


            DA.SetData("2dEnvironment", Env2d);
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
                return Properties.Resources.termitesGhxIcon_temp_12;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("b2fc6141-42c7-4e05-bce3-1570923e946c"); }
        }
    }
}