using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using termitesABMS_toolkitAlpha_r5.agentEnvironments;
using termitesABMS_toolkitAlpha_r5.bristleBotSimulation;

namespace termitesABMS_toolkitAlpha_r5.Agents
{
    public class BristlebotAgent
    {
        public Point3d Position;
        public Vector3d Velocity;
        public AgentEnvironment GenericAgentEnvironment;

        private Vector3d desiredVelocity;
        public  BristlebotSystem;
        public double MaxVelocity = 8.0;
        public double MinVelocity = 4.0;
        public double boundingBoxSizeX;
        public double boundingBoxSizeY;
        public double boundingBoxSizeZ;

        internal BristlebotSystem BristlebotSystem { get; set; }

        public BristlebotAgent(Point3d position, Vector3d velocity)
        {

            Position = position;
            Velocity = velocity;

        }

        public BristlebotAgent(Point3d position, Vector3d velocity, AgentEnvironment agentEnvironment)
        {
            GenericAgentEnvironment = agentEnvironment;
            Position = position;
            Velocity = velocity;
            boundingBoxSizeX = agentEnvironment.EnvWidth;
            boundingBoxSizeY = agentEnvironment.EnvHeight;
            boundingBoxSizeZ = agentEnvironment.EnvDepth;


        }

        public void UpdateVelocityAndPosition()
        {
            // we update the velocity in order to gracefully approach the new position, we keep 97% percent of current veloctiy and add 3 percent of the desired velocity
            Velocity = 0.97 * Velocity + 0.03 * desiredVelocity;

            //add an if statement to limit the length of the velocity, can change 
            //the 8.0 is hard coded number to limit the higher and lower value of velocity
            if (Velocity.Length > MaxVelocity) Velocity *= MaxVelocity / Velocity.Length;
            else if (Velocity.Length < MinVelocity) Velocity *= MinVelocity / Velocity.Length;

            //scale the velocity by the time step before you set the position
            //Position += Velocity * BristlebotSystem.Timestep;

        }

        //containment behavior
        public void ComputeDesiredVelocity(List<BristlebotAgent> neighbours)
        {
            boundingBoxSizeX = GenericAgentEnvironment.EnvWidth;
            boundingBoxSizeY = GenericAgentEnvironment.EnvHeight;
            boundingBoxSizeZ = GenericAgentEnvironment.EnvDepth;

            desiredVelocity = new Vector3d(0.0, 0.0, 0.0);

            if (Position.X < 0.0)
            {
                desiredVelocity += new Vector3d(-Position.X, 0.0, 0.0);
            }
            else if (Position.X > boundingBoxSizeX)
            {
                desiredVelocity = new Vector3d(boundingBoxSizeY - Position.X, 0.0, 0.0);
            }
            if (Position.Y < 0.0)
            {
                desiredVelocity += new Vector3d(0.0, -Position.Y, 0.0);
            }
            else if (Position.Y > boundingBoxSizeY)
            {
                desiredVelocity = new Vector3d(0.0, boundingBoxSizeY - Position.Y, 0.0);
            }
            if (Position.Z < 0.0)
            {
                desiredVelocity += new Vector3d(0.0, 0.0, -Position.Z);
            }
            else if (Position.Z > boundingBoxSizeZ)
            {
                desiredVelocity = new Vector3d(0.0, 0.0, boundingBoxSizeZ - Position.Z);
            }



            //-----------------------------------
            //avoid obstacles behavior------------------
            //----------------------------------- 
            double scaleConstant = 30.0;

            foreach (Circle repeller in BristlebotSystem.Repellers)
            {
                double distanceToRepeller = Position.DistanceTo(repeller.Center);
                Vector3d repulsion = Position - repeller.Center;
                repulsion /= (repulsion.Length * distanceToRepeller);
                repulsion *= scaleConstant * repeller.Radius;
                desiredVelocity += repulsion;
            }
        }
    }
}