using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace macroTermes00
{
    public class MacroTermes00info : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "cicada00";
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
                return new Guid("6d35c681-4479-428f-aa1f-53ac75d7357c");
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
