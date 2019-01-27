using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace termitesABMS_toolkitAlpha.environments
{
    public class GHC_DisplayGenericAgentEnvironment : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_DisplayAgentEnvironment class.
        /// </summary>
        public GHC_DisplayGenericAgentEnvironment()
          : base("GHC_DisplayAgentEnvironment", "Display Environment",
              "Visualize the Agents' Environment",
               "Termites", "2 | Environments")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("AgentEnvironment", "AgentEnvironment", "AgentEnvironment", GH_ParamAccess.item);
            
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("EnvironmentDisplay", "EnvironmentDisplay", "EnvironmentDisplay", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            AgentEnvironment iAgentEnvironment = null;
            
            DA.GetData("AgentEnvironment", ref iAgentEnvironment);
            if (iAgentEnvironment.EnvDepth == 0)
            {
                DA.SetDataList("EnvironmentDisplay", iAgentEnvironment.Display2DGenericEnvironment());
            }
            else
            {
                DA.SetDataList("EnvironmentDisplay", iAgentEnvironment.Display3DGenericEnvironment());
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
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ce88dc41-9968-4fa5-9bc5-014ce5511205"); }
        }
    }
}