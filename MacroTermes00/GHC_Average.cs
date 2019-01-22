using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using macroTermes00.Properties;

namespace macroTermes00
{
    public class GHC_Average : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_Average class.
        /// </summary>
        public GHC_Average()
          : base("Average of 2 numbers", "Avg",
              "A component that computes the average",
              "CicadaToolkit", "Essentials")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("First Number", "First", "The First Number", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("Second Number", "Second", "The Second Number", GH_ParamAccess.item, 0.0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Average Value", "Average", "The Average value", GH_ParamAccess.item); 
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            //declare variable to compute average
            double a = double.NaN;
            double b = double.NaN;



            //reference the input data
            //DA.GetData(0, ref a);
            //DA.GetData(0, ref b);
            //add a boolean to make sure we are passing the Type of values we want
            bool success1 = DA.GetData(0, ref a);
            bool success2 = DA.GetData(1, ref b);

   
            // if the values are of the correct type then compute the average
            if (success1 && success1)
            {
               double average = (a + b) / 2;
               DA.SetData(0, average);

            }
            else if( success1==true && success2 == false)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, " One of the inputs is wrong, you idiot!!!");
            }
            else if (success1 == false && success2 == true)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "  One of the inputs is wrong, you idiot!!!");
            }
            else
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, " Check the inputs, you idiot!!!");
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
                return Properties.Resources.cicadaGhxIcon_temp_03;
                // return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("8e317160-2629-4e26-b494-9651a36753f2"); }
        }
    }
}