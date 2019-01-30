using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace termitesABMS_toolkitAlpha_r5
{
    public class GHC_centroid : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_centroid class.
        /// </summary>
        public GHC_centroid()
          : base("GHC_centroid", "Pt  ",
              "Find the Centroid of a set of points and compute the distance from the centroid to the points ",
              "Termites", "5 | Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "Pts", "Input Points", GH_ParamAccess.list);
        }

        /// <summary> 
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Centroid", "Cntrd", "Centroid", GH_ParamAccess.list);
            pManager.AddNumberParameter("Distances", "Dist", "Distance to Centroid", GH_ParamAccess.list);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            //declare a arraylist of points for the input points
            // iPoints i is a conventaion for signifying input
            List<Point3d> iPoints = new List<Point3d>();
            // the first argument can be either a number the index (i.e. 0) or a the anem of the variable
            DA.GetDataList("Points", iPoints);
            //create a new point for the centroid 
            Point3d centroid = new Point3d(0.0, 0.0, 0.0);

            foreach (Point3d point in iPoints)
                centroid += point;

            centroid /= iPoints.Count;
            DA.SetData("Centroid", centroid);

            //decalre a arraylist of number for the distances
            List<double> distances = new List<double>();

            foreach (Point3d point in iPoints)
                distances.Add(centroid.DistanceTo(point));

            DA.SetDataList("Distances", distances);

        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                return Properties.Resources.termitesGhxIcon_temp_08;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e80abf3a-a9dc-46ea-b88a-0a6c2639414d"); }
        }
    }
}