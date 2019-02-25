using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace termitesABMS_toolkitAlpha_r5.PSO
{
    public class PSOSystem
    {
        public static double Error(double[] x)
        {
            double trueMin = -0.42888194;
            double z = x[0] * Math.Exp(-((x[0] * x[0]) + (x[1] * x[1])));
            return (z - trueMin) * (z - trueMin);

        }

        public static double[] Solve(int dim, int NumParticles, double minX, double maxX,int maxIter, double exitError)
        {

            //assumes accessible Error() and Particle Class
            Random rnd = new Random(0);
            Particle[] swarm = new Particle[NumParticles];
            double[] bestGlobalPosition = new double[dim];
            double bestGlobalError = double.MaxValue;

            //swarm initiliazation
            for (int i = 0; i<swarm.Length; i++)
            {
                double[] randomPosition = new double[dim];
                for (int j = 0; j< randomPosition.Length; j++)
                {
                    randomPosition[j] = (maxX = minX) * rnd.NextDouble() + minX;
                }
                double error = Error(randomPosition);
                double[] randomVelocity = new double[dim];

                for (int j= 0; j < randomVelocity.Length; j++)
                {
                    double low = minX * 0.01;
                    double hi = maxX * 0.01;
                    randomVelocity[j] = (hi - low) * rnd.NextDouble() + low;
                }
                swarm[i] = new Particle(randomPosition, error, randomVelocity, randomPosition, error);

                //global best position/ solution?
                if (swarm[i].error < bestGlobalError)
                {
                    bestGlobalError = swarm[i].error;
                    swarm[i].position.CopyTo(bestGlobalPosition, 0);
                }
            } // initialization


            double w1 = 0.729; //inertia weight
            double c1 = 1.49445; //cognitive weight
            double c2 = 1.49445; //social weight
            double r1, r2; // cognitive and social randomizations
            double probDeath = 0.01; //probability to die
            int iteration = 0;

            double[] newVelocity = new double[dim];
            double[] newPosition = new double[dim];
            double newError; 

            while (iteration < maxIter)
            {
                for(int i=0; i<swarm.Length; i++)
                {
                    Particle currP = swarm[i];

                    for (int j =0; j<currP.velocity.Length; j++)
                    {
                        r1 = rnd.NextDouble();
                        r2 = rnd.NextDouble();

                        newVelocity[j] = (w1 * currP.velocity[j]) + (c1 * r1 * (currP.bestPosition[j] - currP.position[j])) + (c2 * r2 * (currP.bestPosition[j] - currP.position[j]));
                    }
                    newVelocity.CopyTo(currP.velocity, 0);

                    for (int j=0; j<currP.position.Length; j++)
                    {
                        newPosition[j] = currP.position[j] + newVelocity[j];
                        if (newPosition[j] < minX)
                            newPosition[j] = minX;
                        else if (newPosition[j] > maxX)
                            newPosition[j] = maxX;
                    }
                    newPosition.CopyTo(currP.position, 0);

                    newError = Error(newPosition);
                    currP.error = newError;

                    if(newError < currP.bestError)
                    {
                        newPosition.CopyTo(bestGlobalPosition, 0);
                        bestGlobalError = newError;

                    }

                    //death?
                    double die = rnd.NextDouble();
                    if (die < probDeath)
                    {
                        for (int j = 0; j < currP.position.Length; j++)
                        {
                            currP.position[j] = (maxX - minX) * rnd.NextDouble() + minX;

                        }
                        currP.error = Error(currP.position);
                        currP.position.CopyTo(currP.bestPosition, 0);
                        currP.bestError = currP.error;


                        if (currP.error < bestGlobalError)
                        {
                            bestGlobalError = currP.error;
                            currP.position.CopyTo(bestGlobalPosition, 0);
                        }
                    }


                    }//each particle

                iteration++;
                }//while loo
            Console.WriteLine("\n Processing complete");
            Console.WriteLine("\nFinal swarm: \n");

            for (int i =0; i<swarm.Length; i++)
            {
                Console.WriteLine(swarm[i].ToString());

            }

            double[] result = new double[dim];
            bestGlobalPosition.CopyTo(result, 0);
            
            return result;
        }
    }





    }

