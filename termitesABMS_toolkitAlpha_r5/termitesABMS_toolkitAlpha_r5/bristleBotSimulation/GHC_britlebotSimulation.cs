using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using termitesABMS_toolkitAlpha_r5.agentEnvironments;
using termitesABMS_toolkitAlpha_r5.Agents;

namespace termitesABMS_toolkitAlpha_r5.bristleBotSimulation
{
    public class GHC_britlebotSimulation : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_britlebotSimulation class.
        /// </summary>
        public GHC_britlebotSimulation()
          : base("GHC_bristlebotSimulation", "bristlebot Sim",
              "A component that runs simulation for bristlebots",
              "Termites", "4 | Simulations")
        {

        }

        private BristlebotSystem bristlebotSystem;
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Reset", "Reset", "Reset", GH_ParamAccess.item, false); //p0
            pManager.AddBooleanParameter("Play", "Play", "Play", GH_ParamAccess.item, false);//p1
            pManager.AddGenericParameter("Environment", "Environment", "Environment", GH_ParamAccess.item); //p2
            pManager.AddGenericParameter("BristlebotAgent", "BristlebotAgent", "BristlebotAgent", GH_ParamAccess.item); //p3
            pManager.AddIntegerParameter("Count", "Count", "Count", GH_ParamAccess.item, 10);//p4
            pManager.AddGenericParameter("SimulationSettings", "SimulationSettings", "SimulationSettings", GH_ParamAccess.item); //p5
            pManager.AddCircleParameter("Repellers", "Repellers", "Repellers", GH_ParamAccess.list); //p6
            pManager[7].Optional = true;
            pManager.AddBooleanParameter("UseCoresInParallel", "UseCoresInParallel", "UseCoresInParallel", GH_ParamAccess.item, true); //p7
            pManager.AddBooleanParameter("UseR-TreeSearch", "UseR-TreeSearch", "UseR-TreeSearch", GH_ParamAccess.item, true); //p8
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Info", "Info", "Information", GH_ParamAccess.item);
            pManager.AddPointParameter("Positions", "Positions", "The Agent Positions", GH_ParamAccess.list);
            pManager.AddVectorParameter("Velocities", "Velocties", "The Agent Velocities", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            // code for reading input parameters
            //declare input parameters
            bool iReset = false;
            bool iPlay = false;
            Point3d initPos = new Point3d(0,0,0);
            Vector3d initVel = new Vector3d(0.5, 0.5, 1);

            AgentEnvironment iAgentEnvironment = new AgentEnvironment(Plane.WorldXY, 50.0, 50.0, 0);
            BristlebotAgent iBristlebot = new BristlebotAgent(initPos, initVel);
            int iCount = 10;
            double iTimeStep = 0.01;

            double iNeighbourhoodRadius = 1;
            double iSeparationDistance = 1.0;
            List<Circle> iRepellers = new List<Circle>();

            bool iUseParallel = false;
            bool iUseRTree = false;

            //read in input parameters
            DA.GetData("Reset", ref iReset); //0
            DA.GetData("Play", ref iPlay);//1
            DA.GetData("Environment", ref iAgentEnvironment);//2
            DA.GetData("BristlebotAgent", ref iAgentEnvironment);//2
            DA.GetData("Count", ref iCount);//4
            DA.GetData("Timestep", ref iTimeStep);//5
            DA.GetData("NeighbourhoodRadius", ref iNeighbourhoodRadius);//6

            DA.GetData("SeparationDistance", ref iSeparationDistance);//10
            DA.GetDataList("Repellers", iRepellers);//11
            DA.GetData("UseCoresInParallel", ref iUseParallel);//12
            DA.GetData("UseR-TreeSearch", ref iUseRTree);//13


            if (iReset || bristlebotSystem == null)
            {
                bristlebotSystem = new BristlebotSystem(iCount, iAgentEnvironment);
            }
            else
            {
    
                bristlebotSystem.NeighbourhoodRadius = iNeighbourhoodRadius;

                bristlebotSystem.Repellers = iRepellers;
                bristlebotSystem.UseParallel = iUseParallel;

                if (iUseRTree)
                {
                    bristlebotSystem.updateUsingRTree();
                }
                else
                {
                    bristlebotSystem.Update();
                }
                if (iPlay) ExpireSolution(true);

            }

            List<GH_Point> positions = new List<GH_Point>();
            List<GH_Vector> velocities = new List<GH_Vector>();

            foreach (BristlebotAgent agent in bristlebotSystem.Agents)
            {
                positions.Add(new GH_Point(agent.Position));
                velocities.Add(new GH_Vector(agent.Velocity));
            }

            DA.SetDataList("Positions", positions);
            DA.SetDataList("Velocities", velocities);

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
                return Properties.Resources.termitesGhxIcon_temp_15;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("11bea06a-3b26-4f2e-8bbe-72498edb5344"); }
        }
    }
}