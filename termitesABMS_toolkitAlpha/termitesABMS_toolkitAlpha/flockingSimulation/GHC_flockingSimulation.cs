using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using termitesABMS_toolkitAlpha.environments;

namespace termitesABMS_toolkitAlpha.flockingSimulation
{
    public class GHC_flockingSimulation : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_flockingSimulation class.
        /// </summary>


        public GHC_flockingSimulation()
          : base("GHC_flockingSimulation", "Flock Sim",
              "A component that runs a flocking simulation",
              "Termites", "3 | Simulations")
        {

        }

        private FlockSystem flockSystem;
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Reset", "Reset", "Reset", GH_ParamAccess.item, false); //p0
            pManager.AddBooleanParameter("Play", "Play", "Play", GH_ParamAccess.item, false);//p1
            pManager.AddGenericParameter("Environment", "Environment", "Environment", GH_ParamAccess.item); //p2
            pManager.AddBooleanParameter("isSimulation3D", "isSimulation3D", "isSimulation3D", GH_ParamAccess.item, false); //p3
            pManager.AddIntegerParameter("Count", "Count", "Count", GH_ParamAccess.item, 10);//p4
            pManager.AddNumberParameter("Timestep", "Timestep", "Timestep", GH_ParamAccess.item, 0.01); //p5
            pManager.AddNumberParameter("NeighbourhoodRadius", "NeighbourhoodRadius", "NeighbourhoodRadius", GH_ParamAccess.item, 10); //p6
            pManager.AddNumberParameter("Alignment", "Alignment", "Alignment", GH_ParamAccess.item, 1); //p7
            pManager.AddNumberParameter("Cohesion", "Cohesion", "Cohesion", GH_ParamAccess.item, 1); //p8
            pManager.AddNumberParameter("Separation", "Separation", "Separation", GH_ParamAccess.item, 1); //p9
            pManager.AddNumberParameter("SeparationDistance", "SeparationDistance", "SeparationDistance", GH_ParamAccess.item, 5); //p10
            pManager.AddCircleParameter("Repellers", "Repellers", "Repellers", GH_ParamAccess.list); //p11
            pManager[11].Optional = true;
            pManager.AddBooleanParameter("UseCoresInParallel", "UseCoresInParallel", "UseCoresInParallel", GH_ParamAccess.item, true); //p12
            pManager.AddBooleanParameter("UseR-TreeSearch", "UseR-TreeSearch", "UseR-TreeSearch", GH_ParamAccess.item, true); //p13
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
            
            AgentEnvironment iAgentEnvironment = new AgentEnvironment(Plane.WorldXY, 50.0,50.0,0);
            bool i3D = false;
            int iCount = 10;
            double iTimeStep = 0.01;
            double iNeighbourhoodRadius = 1;
            double iAlignment = 0.0;
            double iCohesion = 0.0;
            double iSeparation = 0.0;
            double iSeparationDistance = 1.0;
            List<Circle> iRepellers = new List<Circle>();
            bool iUseParallel = false;
            bool iUseRTree = false;

            //read in input parameters
            DA.GetData("Reset", ref iReset); //0
            DA.GetData("Play", ref iPlay);//1
            DA.GetData("Environment", ref iAgentEnvironment);//2
            DA.GetData("isSimulation3D", ref i3D);//3
            DA.GetData("Count", ref iCount);//4
            DA.GetData("Timestep", ref iTimeStep);//5
            DA.GetData("NeighbourhoodRadius", ref iNeighbourhoodRadius);//6
            DA.GetData("Alignment", ref iAlignment);//7
            DA.GetData("Cohesion", ref iCohesion);//8
            DA.GetData("Separation", ref iSeparation);//9
            DA.GetData("SeparationDistance", ref iSeparationDistance);//10
            DA.GetDataList("Repellers", iRepellers);//11
            DA.GetData("UseCoresInParallel", ref iUseParallel);//12
            DA.GetData("UseR-TreeSearch", ref iUseRTree);//13


            if (iReset || flockSystem == null)
            {
                flockSystem = new FlockSystem(iCount, i3D, iAgentEnvironment);
            }
            else
            {
                flockSystem.Timestep = iTimeStep;
                flockSystem.NeighbourhoodRadius = iNeighbourhoodRadius;
                flockSystem.AlignmentStrength = iAlignment;
                flockSystem.CohesionStrength = iCohesion;
                flockSystem.SeparationStrength = iSeparation;
                flockSystem.SeparationDistance = iSeparationDistance;
                flockSystem.Repellers = iRepellers;
                flockSystem.UseParallel = iUseParallel;

                if (iUseRTree)
                {
                    flockSystem.updateUsingRTree();
                }
                else
                {
                    flockSystem.Update();
                }
                if (iPlay) ExpireSolution(true);

            }

            List<GH_Point> positions = new List<GH_Point>();
            List<GH_Vector> velocities = new List<GH_Vector>();

            foreach (FlockAgent agent in flockSystem.Agents)
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
                return Properties.Resources.termitesGhxIcon_temp_04;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("11bea06a-3b26-4f2e-8bbe-72498edb5348"); }
        }
    }
}