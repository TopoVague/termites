using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace termitesABMS_toolkitAlpha
{
    public class termitesABMS_toolkitAlphaInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "termitesABMStoolkitAlpha";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("e4f05cbb-0288-4a20-9ac4-89a7e5e1e9c9");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
