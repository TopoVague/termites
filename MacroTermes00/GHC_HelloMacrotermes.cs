using System;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace macroTermes00
{
    public class GHC_HelloMacrotermes : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public GHC_HelloMacrotermes()
          : base("helloCicada", "hello",
              "This is a test Grasshopper Component of Cicada",
              "CicadaToolkit", "Essentials")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Plane", "P", "Base plane for spiral", GH_ParamAccess.item, Plane.WorldXY);
            

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
           
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Plane plane = Plane.WorldXY;
            if (!DA.GetData(0, ref plane)) return;

            //Curve spiral = CreateSpiral(plane, radius0, radius1, turns);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                //return new System.Drawing.Bitmap("C:\\Users\\Evangelos\\Documents\\GitHub\\CicadaToolkit\\Cicada_alpha\\cicada00\\cicada00\\icon\\cicadaGhxIcon_temp-03.png");
                return Properties.Resources.ca1;
                //return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e3364d12-4db3-4b12-bb14-e4a4bd321e81"); }
        }
    }
}
