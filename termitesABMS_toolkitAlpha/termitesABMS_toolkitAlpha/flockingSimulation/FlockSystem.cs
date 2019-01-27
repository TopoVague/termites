using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using termitesABMS_toolkitAlpha.environments;

namespace termitesABMS_toolkitAlpha.flockingSimulation
{
    public class FlockSystem
    {
        public List<FlockAgent> Agents;
        public AgentEnvironment defaultEnv;
        public double Timestep;
        public double NeighbourhoodRadius;
        public double FieldOfView;
        public double AlignmentStrength;
        public double CohesionStrength;
        public double SeparationStrength;
        public double SeparationDistance;
        public double MinSpeed;
        public double MaxSpeed;
        public List<Circle> Repellers;
        public bool UseParallel;
        public bool UseRTree;

        public double AgentEnvX;
        public double AgentEnvY;
        public double AgentEnvZ;
        public double defaultEnvSize = 10.0;
        //Constructor
        public FlockSystem(int agentCount, bool i3D, AgentEnvironment iAgentEnvironment)
        {
            Agents = new List<FlockAgent>();
            
            //TO DO check here if you have an environment or not
            if (iAgentEnvironment == null)
            {
                if (i3D)
                {
                    defaultEnv = new AgentEnvironment(Plane.WorldXY, defaultEnvSize, defaultEnvSize, defaultEnvSize);
                    AgentEnvX = defaultEnv.EnvWidth;
                    AgentEnvY = defaultEnv.EnvHeight;
                    AgentEnvZ = defaultEnv.EnvDepth;
                    for (int i = 0; i < agentCount; i++)
                    {

                        FlockAgent agent = new FlockAgent(
                            Util.GetRandomPoint(0.0, AgentEnvX, 0.0, AgentEnvY, 0.0, AgentEnvZ),
                            Util.GetRandomUnitVector() * 4.0,
                            defaultEnv);
                        agent.FlockSystem = this;
                        Agents.Add(agent);
                    }

                }
                else
                {
                    defaultEnv = new AgentEnvironment(Plane.WorldXY, defaultEnvSize, defaultEnvSize, 0);
                    AgentEnvX = defaultEnv.EnvWidth;
                    AgentEnvY = defaultEnv.EnvHeight;
                    AgentEnvZ = defaultEnv.EnvDepth;
                    for (int i = 0; i < agentCount; i++)
                    {

                        FlockAgent agent = new FlockAgent(
                            Util.GetRandomPoint(0.0, AgentEnvX, 0.0, AgentEnvY, 0.0, AgentEnvZ),
                            Util.GetRandomUnitVector() * 4.0,
                            defaultEnv);
                        agent.FlockSystem = this;
                        Agents.Add(agent);
                    }

                }


            }
            else
            {

                defaultEnv = iAgentEnvironment;
                AgentEnvX = defaultEnv.EnvWidth;
                AgentEnvY = defaultEnv.EnvHeight;
                AgentEnvZ = defaultEnv.EnvDepth; 
                for (int i = 0; i < agentCount; i++)
                {
                    FlockAgent agent = new FlockAgent(
                        Util.GetRandomPoint(0.0, AgentEnvX, 0.0, AgentEnvY, 0.0, AgentEnvZ),
                        Util.GetRandomUnitVectorXY() * 4.0,
                        defaultEnv);

                    agent.FlockSystem = this;
                    Agents.Add(agent);
                }

            }


        }


        //method for finding neighbouring agents
        private List<FlockAgent> FindNeighbours(FlockAgent agent)
        {
            List<FlockAgent> neighbours = new List<FlockAgent>();
            foreach (FlockAgent neighbour in Agents)
            {
                if (neighbour != agent && neighbour.Position.DistanceTo(agent.Position) < NeighbourhoodRadius)
                {
                    neighbours.Add(neighbour);
                }
            }
            return neighbours;
        }

        //make the compute desired velocity into a function so that we can run it in parallel
        private void ComputeAgentDesiredVelocty(FlockAgent agent)
        {

        }


        //-----------------------------------
        //update Agent Method------------------
        //----------------------------------- 
        public void Update()
        {
            if (!UseParallel)
            {
                foreach (FlockAgent agent in Agents)
                {
                    List<FlockAgent> neighbours = FindNeighbours(agent);
                    agent.ComputeDesiredVelocity(neighbours);

                }
            }
            else
            {
                // we have to import the library to be able to run the code in parallel- using System.Threading.tasks
                // then we call the ForEach method which takes two arguments an object and a method that we use a special 
                //syntax, symbol (FlockAgent agent =>{here come the code});
                //declared above and 
                // returns only one number which is the velocity!!!!
                Parallel.ForEach(Agents, (FlockAgent agent) =>
                {
                    List<FlockAgent> neighbours = FindNeighbours(agent);
                    agent.ComputeDesiredVelocity(neighbours);
                });
            }


            //we compute the desired velocity first before we update the velocity
            foreach (FlockAgent agent in Agents)
            {
                agent.UpdateVelocityAndPosition();
            }

        }/// simple update ends here




        public void updateUsingRTree()
        {
            RTree rTree = new RTree();
            for (int i = 0; i < Agents.Count; i++)
                rTree.Insert(Agents[i].Position, i);
            foreach (FlockAgent agent in Agents)
            {
                List<FlockAgent> neighbours = new List<FlockAgent>();

                //here is an anonynmous function that implements an rTree
                EventHandler<RTreeEventArgs> rTreeCallback =
                (object sender, RTreeEventArgs args) =>
                {
                    if (Agents[args.Id] != agent)
                        neighbours.Add(Agents[args.Id]);
                };

                rTree.Search(new Sphere(agent.Position, NeighbourhoodRadius), rTreeCallback);
                agent.ComputeDesiredVelocity(neighbours);
            }
            foreach (FlockAgent agent in Agents) agent.UpdateVelocityAndPosition();

        }
    }
}

