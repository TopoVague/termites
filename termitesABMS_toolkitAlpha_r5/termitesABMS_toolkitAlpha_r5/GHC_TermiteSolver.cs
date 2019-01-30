using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace termitesABMS_toolkitAlpha_r5
{
    public class GHC_TermiteSolver : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_TermiteSolver class.
        /// </summary>
        public GHC_TermiteSolver()
          : base("GHC_TermiteSolver", "TermiteSolver",
              "This is the component that runs the solvers",
              "Termites", "0 | Termite System")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Reset", "Reset", "Reset", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("Play", "Play", "Play", GH_ParamAccess.item, false);
            pManager.AddNumberParameter("Timestep", "Timestep", "Timestep", GH_ParamAccess.item, 0.01);

            pManager.AddBooleanParameter("UseParallel", "UseParallel", "UseParallel", GH_ParamAccess.item, true);
            pManager.AddBooleanParameter("UseR-Tree", "UseR-Tree", "UseR-Tree", GH_ParamAccess.item, true);
            pManager.AddBooleanParameter("UseR-Tree", "UseR-Tree", "UseR-Tree", GH_ParamAccess.item, true);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("TermiteSolverInfo", "TermiteSolverInfo", "Information about the condition of the solver", GH_ParamAccess.item);
            //pManager.AddGenericParameter("MultiAgentSystem", "MultiAgentSystem", "MultiAgentSystem", GH_ParamAccess.item);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            string infoMsg = "This is a text message";
            DA.SetData(0, infoMsg);
            //DA.GetData(1, "This is an agnet system in Progress");
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
                return Properties.Resources.termitesGhxIcon_temp_07;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("8f7f5f54-8962-4cec-a7f3-1d0d099abcd1"); }
        }
    }
}