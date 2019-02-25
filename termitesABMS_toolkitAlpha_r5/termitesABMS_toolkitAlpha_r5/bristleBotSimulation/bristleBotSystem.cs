using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Threading.Tasks;
using termitesABMS_toolkitAlpha_r5.agentEnvironments;
using termitesABMS_toolkitAlpha_r5.Agents;

namespace termitesABMS_toolkitAlpha_r5.bristleBotSimulation
{
    class BristlebotSystem
    {
        public List<BristlebotAgent> Agents;
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
        public BristlebotSystem(int agentCount, AgentEnvironment iAgentEnvironment)
        {
            Agents = new List<BristlebotAgent>();

            //TO DO check here if you have an environment or not
            if (iAgentEnvironment == null)
            {
               
                
                    defaultEnv = new AgentEnvironment(Plane.WorldXY, defaultEnvSize, defaultEnvSize, defaultEnvSize);
                    AgentEnvX = defaultEnv.EnvWidth;
                    AgentEnvY = defaultEnv.EnvHeight;
                    AgentEnvZ = defaultEnv.EnvDepth;
                    for (int i = 0; i < agentCount; i++)
                    {

                        BristlebotAgent agent = new BristlebotAgent(
                            Util.GetRandomPoint(0.0, AgentEnvX, 0.0, AgentEnvY, 0.0, AgentEnvZ),
                            Util.GetRandomUnitVector() * 4.0,
                            defaultEnv);
                        agent.BristlebotSystem = this;
                        Agents.Add(agent);
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
                    BristlebotAgent agent = new BristlebotAgent(
                        Util.GetRandomPoint(0.0, AgentEnvX, 0.0, AgentEnvY, 0.0, AgentEnvZ),
                        Util.GetRandomUnitVectorXY() * 4.0,
                        defaultEnv);

                    agent.BristlebotSystem = this;
                    Agents.Add(agent);
                }

            }


        }


        //method for finding neighbouring agents
        private List<BristlebotAgent> FindNeighbours(BristlebotAgent agent)
        {
            List<BristlebotAgent> neighbours = new List<BristlebotAgent>();
            foreach (BristlebotAgent neighbour in Agents)
            {
                if (neighbour != agent && neighbour.Position.DistanceTo(agent.Position) < NeighbourhoodRadius)
                {
                    neighbours.Add(neighbour);
                }
            }
            return neighbours;
        }

        //make the compute desired velocity into a function so that we can run it in parallel
        private void ComputeAgentDesiredVelocty(BristlebotAgent agent)
        {

        }


        //-----------------------------------
        //update Agent Method------------------
        //----------------------------------- 
        public void Update()
        {
            if (!UseParallel)
            {
                foreach (BristlebotAgent agent in Agents)
                {
                    List<BristlebotAgent> neighbours = FindNeighbours(agent);
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
                Parallel.ForEach(Agents, (BristlebotAgent agent) =>
                {
                    List<BristlebotAgent> neighbours = FindNeighbours(agent);
                    agent.ComputeDesiredVelocity(neighbours);
                });
            }


            //we compute the desired velocity first before we update the velocity
            foreach (BristlebotAgent agent in Agents)
            {
                agent.UpdateVelocityAndPosition();
            }

        }/// simple update ends here




        public void updateUsingRTree()
        {
            RTree rTree = new RTree();
            for (int i = 0; i < Agents.Count; i++)
                rTree.Insert(Agents[i].Position, i);
            foreach (BristlebotAgent agent in Agents)
            {
                List<BristlebotAgent> neighbours = new List<BristlebotAgent>();

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
            foreach (BristlebotAgent agent in Agents) agent.UpdateVelocityAndPosition();

        }
    }
}
