using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace termitesABMS_toolkitAlpha_r5
{
    public class termitesABMS_toolkitAlpha_r5Info : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "termitesABMStoolkitAlphaR5";
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
                return new Guid("abc5f1da-18dc-492a-9297-425b9ab89d82");
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
