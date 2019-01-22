using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;



namespace macroTermes00.FlockingSimulation
{
    public class GHC_flockingSimulation : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_flockingSimulation class.
        /// </summary>
        

        public GHC_flockingSimulation()
          : base("GHC_flockingSimulation", "Flock Sim",
              "A component that runs a flocking simulation",
              "CicadaToolkit", "Flock")
        {

        }

        private FlockSystem flockSystem;
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Reset", "Reset", "Reset", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("Play", "Play", "Play", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("3Dsimulation", "3Dsimulation", "3Dsimulation", GH_ParamAccess.item, true);
            pManager.AddIntegerParameter("Count", "Count", "Count", GH_ParamAccess.item, 10);
            pManager.AddNumberParameter("Timestep", "Timestep", "Timestep", GH_ParamAccess.item, 0.01);
            pManager.AddNumberParameter("NeighbourhoodRadius", "NeighbourhoodRadius", "NeighbourhoodRadius", GH_ParamAccess.item, 10);
            pManager.AddNumberParameter("Alignment", "Alignment", "Alignment", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("Cohesion", "Cohesion", "Cohesion", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("Separation", "Separation", "Separation", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("SeparationDistance", "SeparationDistance", "SeparationDistance", GH_ParamAccess.item, 5);
            pManager.AddCircleParameter("Repellers", "Repellers", "Repellers", GH_ParamAccess.list);
            pManager[10].Optional = true;
            pManager.AddBooleanParameter("UseParallel", "UseParallel", "UseParallel", GH_ParamAccess.item, true);
            pManager.AddBooleanParameter("UseR-Tree", "UseR-Tree", "UseR-Tree", GH_ParamAccess.item, true);
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
            DA.GetData("Reset", ref iReset);
            DA.GetData("Play", ref iPlay);
            DA.GetData("3Dsimulation", ref i3D);
            DA.GetData("Count", ref iCount);
            DA.GetData("Timestep", ref iTimeStep);
            DA.GetData("NeighbourhoodRadius", ref iNeighbourhoodRadius);
            DA.GetData("Alignment", ref iAlignment);
            DA.GetData("Cohesion", ref iCohesion);
            DA.GetData("Separation", ref iSeparation);
            DA.GetData("SeparationDistance", ref iSeparationDistance);
            DA.GetDataList("Repellers", iRepellers);
            DA.GetData("UseParallel", ref iUseParallel);
            DA.GetData("UseR-Tree", ref iUseRTree);


            if (iReset || flockSystem == null)
            {
                flockSystem = new FlockSystem(iCount, i3D);
            } else
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
                return Properties.Resources.cicadaGhxIcon_temp_03;
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