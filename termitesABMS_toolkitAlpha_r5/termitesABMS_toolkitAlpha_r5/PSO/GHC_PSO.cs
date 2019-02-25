using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;



namespace termitesABMS_toolkitAlpha_r5.PSO
{
    public class GHC_PSO : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_PSO class.
        /// </summary>
        public GHC_PSO()
          : base("Particle Swarm Optimization", "PSO",
              "A component to ",
              "Termites", "4 | Simulations")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Problem Dimension", "Problem Dimension", "Problem Dimension", GH_ParamAccess.item, 2); //p1
            pManager.AddNumberParameter("Number of Particles", "Number of Particles", "Number of Particles", GH_ParamAccess.item, 10); //p2
            pManager.AddNumberParameter("Number of Max Iterations", "Number of Max Iterations", "Number of Max Iterations", GH_ParamAccess.item, 1000); //p3
            pManager.AddNumberParameter("Exit Error", "Exit Error", "Exit Error", GH_ParamAccess.item, 0.0); //p4
            pManager.AddNumberParameter("minimum X", "minimum X", "minimum X", GH_ParamAccess.item, 10.0); //p5 - problem dependdent
            pManager.AddNumberParameter("maximum X", "maximum X", "maximum X", GH_ParamAccess.item, 10.0); //p6 - problem dependdent
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

            //bool iReset = false;
            //bool iPlay = false;

            int iProblemDimensions = 2 ;
            int iPartcileCount = 10;
            int iMaxIterations = 10;
            double iExitError = 1;
            double iMinX = 0.0;
            double iMaxX = 0.0;


            // pManager.AddNumberParameter("Problem Dimension", "Problem Dimension", "Problem Dimension", GH_ParamAccess.item, 2); //p1
            // pManager.AddNumberParameter("Number of Particles", "Number of Particles", "Number of Particles", GH_ParamAccess.item, 10); //p2
            // pManager.AddNumberParameter("Number of Max Iterations", "Number of Max Iterations", "Number of Max Iterations", GH_ParamAccess.item, 1000); //p3
            // pManager.AddNumberParameter("Exit Error", "Exit Error", "Exit Error", GH_ParamAccess.item, 0.0); //p4
            //  pManager.AddNumberParameter("minimum X", "minimum X", "minimum X", GH_ParamAccess.item, 10.0); //p5 - problem dependdent
            // pManager.AddNumberParameter("maximum X", "maximum X", "maximum X", GH_ParamAccess.item, 10.0); //p6 - problem dependdent
            //read input params
            DA.GetData("Problem Dimension", ref iProblemDimensions);
            DA.GetData("Number of Particles", ref iPartcileCount);
            DA.GetData("Number of Max Iterations", ref iMaxIterations);
            DA.GetData("Exit Error", ref iExitError);
            DA.GetData("minimum X", ref iMinX);
            DA.GetData("maximum X", ref iMaxX);
            Console.WriteLine("\n Strating PSO");

            double[] bestPos = PSOSystem.Solve(iProblemDimensions, iPartcileCount, iMinX, iMaxX, iMaxIterations, iExitError);
      
            //TO DO here change
            //double bestError = PSOSystem.Error(bestPosition);

            Console.WriteLine("Best Position found: ");

            Console.WriteLine("Best positionfound:");
            for (int i = 0; i < bestPos.Length; ++i)
            {
                Console.Write("x" + i + " = ");
                Console.WriteLine(bestPos[i].ToString("F6") + " ");
            }
            Console.WriteLine("");
            Console.Write("Final best error = ");
           // Console.WriteLine(bestError.ToString("F5"));

            Console.WriteLine("\nEnd PSO demo\n");
            Console.ReadLine();


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
            get { return new Guid("440df94b-e133-4c8b-9ba9-122aa6540bec"); }
        }
    }
}