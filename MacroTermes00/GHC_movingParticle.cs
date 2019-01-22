﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace macroTermes00
{
    public class GHC_movingParticle : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_movingParticle class.
        /// </summary>
        /// 

        Point3d currentPosition;
        public GHC_movingParticle()
          : base("GHC_movingParticle", "MvPrtcl",
              "Create a moving particle in a direction defined by the user",
              "CicadaToolkit", "Agents")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Reset", "Reset", "Reset", GH_ParamAccess.item);
            pManager.AddVectorParameter("velocity", "velocity", "velocity", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Play", "Play", "Play", GH_ParamAccess.item);
             

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Particle", "Particle", "Particle", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool iReset = true;
            DA.GetData(0, ref iReset);

            Vector3d iVelocity = new Vector3d();
            DA.GetData(1, ref iVelocity); 

            if (iReset)
            {
                currentPosition = new Point3d(0.0, 0.0, 0.0);
                return;
            }

            currentPosition += iVelocity;
            DA.SetData(0, currentPosition);

            //add this code to not need to a timer 
            bool iPlay = false;
            DA.GetData(2, ref iPlay);
            if (iPlay)
            {
                ExpireSolution(true);
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
            get { return new Guid("68d01d92-c4e4-43de-aafc-9371ce43f307"); }
        }
    }
}