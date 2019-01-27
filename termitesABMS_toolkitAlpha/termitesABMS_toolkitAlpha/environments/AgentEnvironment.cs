using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace termitesABMS_toolkitAlpha.environments
{
    public class AgentEnvironment
    {
        public Plane BasePlane = Plane.WorldXY;
        public double EnvWidth = 30.0;
        public double EnvHeight = 30.0;
        public double EnvDepth = 30.0;

        public AgentEnvironment(Plane basePlane,double width, double height)
        {

            BasePlane = basePlane;
            EnvWidth = width;
            EnvHeight = height;
            

        }

        public AgentEnvironment(Plane basePlane, double width, double height, double depth)
        {
            BasePlane = basePlane;
            EnvWidth = width;
            EnvHeight = height;
            EnvDepth = depth;

        }

        public double computeBoundingVolume()
        {
            return  EnvWidth * EnvWidth * EnvDepth;
        }

        public List<LineCurve> Display2DGenericEnvironment()
        {
            Point3d A = BasePlane.Origin + BasePlane.XAxis * EnvWidth * 0.5 + BasePlane.YAxis * EnvHeight * 0.5;
            Point3d B = BasePlane.Origin - BasePlane.XAxis * EnvWidth * 0.5 + BasePlane.YAxis * EnvHeight * 0.5;
            Point3d C = BasePlane.Origin - BasePlane.XAxis * EnvWidth * 0.5 - BasePlane.YAxis * EnvHeight * 0.5;
            Point3d D = BasePlane.Origin + BasePlane.XAxis * EnvWidth * 0.5 - BasePlane.YAxis * EnvHeight * 0.5;
           
            List<LineCurve> displayLines = new List<LineCurve>();
            displayLines.Add(new LineCurve(A, B));
            displayLines.Add(new LineCurve(B, C));
            displayLines.Add(new LineCurve(C, D));
            displayLines.Add(new LineCurve(D, A));

            return displayLines;
        }
        public List<LineCurve> Display3DGenericEnvironment()
        {
            Point3d A = BasePlane.Origin + BasePlane.XAxis * EnvWidth * 0.5 + BasePlane.YAxis * EnvHeight * 0.5;
            Point3d B = BasePlane.Origin - BasePlane.XAxis * EnvWidth * 0.5 + BasePlane.YAxis * EnvHeight * 0.5;
            Point3d C = BasePlane.Origin - BasePlane.XAxis * EnvWidth * 0.5 - BasePlane.YAxis * EnvHeight * 0.5;
            Point3d D = BasePlane.Origin + BasePlane.XAxis * EnvWidth * 0.5 - BasePlane.YAxis * EnvHeight * 0.5;
            Point3d E = BasePlane.Origin + BasePlane.XAxis * EnvWidth * 0.5 + BasePlane.YAxis * EnvHeight * 0.5 + BasePlane.ZAxis * EnvDepth;
            Point3d F = BasePlane.Origin - BasePlane.XAxis * EnvWidth * 0.5 + BasePlane.YAxis * EnvHeight * 0.5 + BasePlane.ZAxis * EnvDepth;
            Point3d G = BasePlane.Origin - BasePlane.XAxis * EnvWidth * 0.5 - BasePlane.YAxis * EnvHeight * 0.5 + BasePlane.ZAxis * EnvDepth;
            Point3d H = BasePlane.Origin + BasePlane.XAxis * EnvWidth * 0.5 - BasePlane.YAxis * EnvHeight * 0.5 + BasePlane.ZAxis * EnvDepth;
            List<LineCurve> displayLines = new List<LineCurve>();

            displayLines.Add(new LineCurve(A, B));
            displayLines.Add(new LineCurve(B, C));
            displayLines.Add(new LineCurve(C, D));
            displayLines.Add(new LineCurve(D, A));
            displayLines.Add(new LineCurve(A, E));
            displayLines.Add(new LineCurve(B, F));
            displayLines.Add(new LineCurve(C, G));
            displayLines.Add(new LineCurve(D, H));

            return displayLines;
        }

    }
}
