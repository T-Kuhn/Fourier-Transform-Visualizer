using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourier_Transform_Visualizer
{
    public class FourierTransform
    {
        double[] Re;
        double[] Im;
        double[][] IFT;
        int N;

        public FourierTransform(int nmbr)
        {
            N = nmbr;
            Re = new double[nmbr];
            Im = new double[nmbr];
            //IFT = new double[nmbr][nmbr];
        }
        public void calcFT(Plotter pltr)
        {
            for (int h = 0; h < N; h++)
            {
                Re[h] = 0;
                Im[h] = 0;
            }
            //for-loop for all the k values
            for (int j = 0; j < N; j++)
            {
                //loop through all the n values for every k value to create the e^...'s
                for (int i = 0; i < N; i++)
                {
                    Re[j] += pltr.doublepos[i].Y * Math.Cos(2 * Math.PI / N * i * j);
                    Im[j] -= pltr.doublepos[i].Y * Math.Sin(2 * Math.PI / N * i * j);
                }
            }
        }
        public void calcIFT(Plotter pltr, int nmbr)
        {
            for (int h = 0; h < N; h++)
            {
                pltr.doublepos[h].X = 0;
                pltr.doublepos[h].Y = 0;
            }
            //for-loop for all the k values
            for (int j = 0; j < N; j++)
            {
                pltr.doublepos[j].X = j;
                //loop through all the n values for every k value to create the e^...'s
                for (int i = 0; i < nmbr; i++)
                {
                    pltr.doublepos[j].Y += Re[i] * Math.Cos(2 * Math.PI / N * i * j) - Im[i] * Math.Sin(2 * Math.PI / N * i * j);
                }
                pltr.doublepos[j].Y = pltr.doublepos[j].Y / N;
            }
        }
        public void cutFreq(int nmbr)
        {
            for (int i = nmbr; i < N - nmbr; i++)
            {
                Re[i] = 0;
                Im[i] = 0;
            }
        }
        public void calcPS(Plotter pltr)
        {
            for (int i = 0; i < N; i++)
            {
                pltr.doublepos[i].X = i;
                pltr.doublepos[i].Y = (Re[i] * Re[i] + Im[i] * Im[i]) /100;

            }

        }
    }
}
