using System;
using System.Diagnostics;
using System.Drawing;

namespace Fourier_Transform_Visualizer
{
    public class Plotter
    {
        // N describes the number of samples
        public int N;
        // N for plotting purposes
        public int plottN;
        // doublepos[] contains coordinates in the format double of points in the xy-plane
        public DoublePoint[] doublepos;
        // Point[] in int for displaying purpose
        public Point[] pos;
        // Random number generator
        public Random ran;
        // this flag is high while the graph is beeing plotted
        public bool drawingFlag;
        // drawing request flag. If this is high a graph has to be drawn.
        public bool drawingReq;
        public int imgWidth;
        public int imgHeight;
        // the constructor
        public Plotter(int nmbr, int plottNmbr)
        {
            N = nmbr;
            plottN = plottNmbr;
            doublepos = new DoublePoint[nmbr];
            pos = new Point[nmbr];
            ran = new Random();
            drawingFlag = false;
            drawingReq = false;
            for (int i = 0; i < N; i++)
            {
                doublepos[i] = new DoublePoint(0, 0);
                pos[i] = new Point(0, 0);
            }
        }
        public void ranPosVal(int nmbr)
        {
            for (int i = 0; i < N; i++)
            {
                doublepos[i].X = i;
                doublepos[i].Y = ran.Next(nmbr);
            }
        }
        public void ranPosVal_smooth(int nmbr)
        //doesn't work correctly
        {
            for (int i = 0; i < N / 4; i++)
            {
                doublepos[i].X = i * 4;
                doublepos[i].Y = ran.Next(nmbr);
            }
        }
        public void TothsawPosVal(int nmbr)
        {
            int h = 0;
            for (int i = 0; i < N; i++)
            {
                doublepos[i].X = i;
                doublepos[i].Y = h++;
                if (h > nmbr)
                {
                    h = 0;
                }
            }
        }
        public void RectPosVal(int nmbr)
        {
            for (int i = 0; i < N; i++)
            {
                doublepos[i].X = i;

                if (i > 256 - nmbr && i < 256 + nmbr)
                {
                    doublepos[i].Y = nmbr;
                }
                else
                {
                    doublepos[i].Y = 0;
                }
            }
        }
        public void RectPosVal2(int nmbr1, int nmbr2, int nmbr3)
        {
            for (int i = 0; i < N; i++)
            {
                doublepos[i].X = i;

                if (i > 256 - nmbr1 && i < 256 + nmbr1)
                {
                    doublepos[i].Y = nmbr1;
                }
                else
                {
                    doublepos[i].Y = 0;
                }
                if (i > 256 - nmbr2 && i < 256 + nmbr2)
                {
                    doublepos[i].Y += nmbr2;
                }
                else
                {
                    doublepos[i].Y += 0;
                }
                if (i > 256 - nmbr3 && i < 256 + nmbr3)
                {
                    doublepos[i].Y += nmbr3;
                }
                else
                {
                    doublepos[i].Y += 0;
                }
            }
        }
        public void natPosVal(int nmbr1, int nmbr2)
        {
            Debug.WriteLine("Number1: " + nmbr1);
            Debug.WriteLine("Sumber2: " + nmbr2);
            for (int i = 0; i < N; i++)
            {
                doublepos[i].X = i;
                doublepos[i].Y = nmbr1 * Math.Cos(nmbr1 * 2 * Math.PI / N * i) +
                    nmbr2 * Math.Sin(nmbr2 * 2 * Math.PI / N * i);
            }
        }
        public void natPosValPlusNoise(int nmbr1, int nmbr2, int nmbr3)
        {
            Debug.WriteLine("Number1: " + nmbr1);
            Debug.WriteLine("Sumber2: " + nmbr2);
            for (int i = 0; i < N; i++)
            {
                doublepos[i].X = i;
                doublepos[i].Y = nmbr1 * Math.Cos(nmbr1 * 2 * Math.PI / N * i) +
                    nmbr2 * Math.Sin(nmbr2 * 2 * Math.PI / N * i) + ran.Next(nmbr3);
            }
        }
        public void setPosValToDeltaFunction()
        {
            for (int i = 0; i < N; i++)
            {
                doublepos[i].X = i;
                if (i == 0)
                {
                    doublepos[i].Y = 100;
                }
                else
                {
                    doublepos[i].Y = 0;
                }
            }
        }
        public void xTothePowerof2(int nmbr)
        {
            for (int i = 0; i < N; i++)
            {
                doublepos[i].X = i;
                if (i > 150 && i < 150 + nmbr)
                {
                    doublepos[i].Y = -((i - (150 + nmbr / 2)) * (i - (150 + nmbr / 2)) / 20) + (nmbr / 2) * (nmbr / 2) / 20;
                }
                else
                {
                    doublepos[i].Y = 0;
                }
            }
        }
        public void loadDataFromPic()
        {
            Bitmap img = new Bitmap("C://git/Fourier-Transform-Visualizer/64x64pic_1.png");
            DoublePoint[] tmp;
            imgWidth = img.Width;
            imgHeight = img.Height;
            int length = img.Width*img.Height;
            tmp = new DoublePoint[length];
            for (int i = 0; i < length; i++)
            {
                tmp[i] = new DoublePoint(0, 0);
            }

            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    tmp[j + img.Width*i].X = j + img.Width * i;
                    tmp[j + img.Width*i].Y = img.GetPixel(j, i).GetBrightness()*100;
                }
            }

            for (int i = 0; i < N; i++)
            {
                doublepos[i].X = tmp[i].X;
                doublepos[i].Y = tmp[i].Y;
            }
        }
        public void calculateIntpos(int nmbr)
        {
            for (int i = 0; i < N; i++)
            {
                pos[i].X = System.Convert.ToInt32(doublepos[i].X);
                pos[i].Y = System.Convert.ToInt32(doublepos[i].Y / nmbr);
            }
        }

        public void reset()
        {
            for (int i = 0; i < N; i++)
            {
                doublepos[i] = new DoublePoint(0, 0);
                pos[i] = new Point(0, 0);
            }
        }
    }
}
