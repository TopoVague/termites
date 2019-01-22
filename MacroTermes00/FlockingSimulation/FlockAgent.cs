using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Geometry;


namespace macroTermes00.FlockingSimulation
{
    public class FlockAgent
    {
        public Point3d Position;
        public Vector3d Velocity;

        private Vector3d desiredVelocity;
        public FlockSystem FlockSystem;


 
        


        public FlockAgent(Point3d position, Vector3d velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public void UpdateVelocityAndPosition()
        {
            // we update the velocity in order to gracefully approach the new position, we keep 97% percent of current veloctiy and add 3 percent of the desired velocity
            Velocity = 0.97 * Velocity + 0.03 * desiredVelocity;

            //add an if statement to limit the length of the velocity, can change 
            //the 8.0 is hard coded number to limit the higher and lower value of velocity
            if (Velocity.Length > 8.0) Velocity *= 8.0 / Velocity.Length;
            else if (Velocity.Length < 4.0) Velocity *= 4.0 / Velocity.Length;

            //scale the velocity by the time step before you set the position
            Position += Velocity * FlockSystem.Timestep;

        }

        //containment behavior
        public void ComputeDesiredVelocity(List<FlockAgent> neighbours)
        {

            desiredVelocity = new Vector3d(0.0, 0.0, 0.0);
            double boundingBoxSize = 30.0;


            if (Position.X < 0.0)
            {
                desiredVelocity += new Vector3d(-Position.X, 0.0, 0.0);
            }
            else if (Position.X > boundingBoxSize)
            {
                desiredVelocity = new Vector3d(boundingBoxSize - Position.X, 0.0, 0.0);
            }
            if (Position.Y < 0.0)
            {
                desiredVelocity += new Vector3d(0.0, -Position.Y, 0.0);
            }
            else if (Position.Y > boundingBoxSize)
            {
                desiredVelocity = new Vector3d(0.0, boundingBoxSize - Position.Y, 0.0);
            }
            if (Position.Z < 0.0)
            {
                desiredVelocity += new Vector3d(0.0, 0.0, -Position.Z);
            }
            else if (Position.Z > boundingBoxSize)
            {
                desiredVelocity = new Vector3d(0.0, 0.0, boundingBoxSize - Position.Z);
            }



            if (neighbours.Count != 0)
            {

                //-----------------------------------
                //alignment behavior-----------------
                //-----------------------------------
                Vector3d alignment = Vector3d.Zero;
                foreach (FlockAgent neighbour in neighbours)
                {
                    alignment += neighbour.Velocity;
                }
                alignment /= neighbours.Count;
                desiredVelocity += FlockSystem.AlignmentStrength * alignment;

                //-----------------------------------
                //separation behavior----------------
                //-----------------------------------
                Vector3d separation = Vector3d.Zero;
                foreach (FlockAgent neighbour in neighbours)
                {
                    double distanceToNeighbour = Position.DistanceTo(neighbour.Position);
                    if (distanceToNeighbour < FlockSystem.SeparationDistance)
                    {
                        Vector3d getAway = Position - neighbour.Position;
                        separation += getAway / (getAway.Length * distanceToNeighbour);
                    }

                }
                desiredVelocity += FlockSystem.SeparationStrength * separation;


                //-----------------------------------
                //cohesion behavior------------------
                //----------------------------------- 
                Point3d center = Point3d.Origin;
                foreach (FlockAgent neighbour in neighbours)
                {
                    center += neighbour.Position;
                }
                center /= neighbours.Count; 
                Vector3d cohesion = center - Position;
                desiredVelocity += FlockSystem.CohesionStrength * cohesion;
                //-----------------------------------

            }
            else
            {
                desiredVelocity += Velocity; //maintain the current velocity
            }

            //-----------------------------------
            //avoid obstacles behavior------------------
            //----------------------------------- 
            double scaleConstant = 30.0;

            foreach (Circle repeller in FlockSystem.Repellers)
            {
                double distanceToRepeller = Position.DistanceTo(repeller.Center);
                Vector3d repulsion = Position - repeller.Center;
                repulsion /= (repulsion.Length * distanceToRepeller);
                repulsion *= 30.0 * repeller.Radius;
                desiredVelocity += repulsion;
            }


        }


    }


}
