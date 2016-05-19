using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fourier_Transform_Visualizer
{


    public partial class FTV : Form
    {
        public FourierTransform FT;
        public Plotter plotter1;
        public Plotter plotter2;
        public Plotter plotter3;
        public Random random;
        public Thread thread1;

        public FTV()
        {
            InitializeComponent();
            random = new Random();
            FT = new FourierTransform(512);
            plotter1 = new Plotter(512);
            plotter2 = new Plotter(512);
            plotter3 = new Plotter(512);

            thread1 = new Thread(new ThreadStart(Thread1));
            // start thread
            thread1.Start();
        }

        private void FTV_Load(object sender, EventArgs e)
        {

        }

        private void FTV_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread1.Abort();
        }

        public void Thread1()
        {
            while (true)
            {
                if (plotter1.drawingReq == true)
                {
                    plotter1.drawingFlag = true;
                    Bitmap flag = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    Graphics flagGraphics = Graphics.FromImage(flag);

                    for (int i = 0; i < plotter1.N - 1; i++)
                    {
                        flagGraphics.DrawLine(new Pen(Brushes.Green, 1), new Point(plotter1.pos[i].X + 5, pictureBox1.Height - 50 - plotter1.pos[i].Y),
                            new Point(plotter1.pos[i + 1].X + 5, pictureBox1.Height - 50 - plotter1.pos[i + 1].Y));

                        if (i % 7 == 0)
                        {
                            pictureBox1.Image = flag;
                            Thread.Sleep(10);
                        }
                    }
                    pictureBox1.Image = flag;
                    plotter1.drawingFlag = false;
                    plotter1.drawingReq = false;
                }
                if (plotter2.drawingReq == true)
                {
                    plotter2.drawingFlag = true;
                    Bitmap flag = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                    Graphics flagGraphics = Graphics.FromImage(flag);

                    for (int i = 0; i < plotter2.N - 1; i++)
                    {
                        flagGraphics.DrawLine(new Pen(Brushes.White, 1), new Point(plotter2.pos[i].X + 5, pictureBox2.Height - plotter2.pos[i].Y),
                            new Point(plotter2.pos[i + 1].X + 5, pictureBox2.Height - plotter2.pos[i + 1].Y));


                        if (i % 7 == 0)
                        {
                            pictureBox2.Image = flag;
                            Thread.Sleep(10);
                        }
                    }
                    pictureBox2.Image = flag;
                    plotter2.drawingFlag = false;
                    plotter2.drawingReq = false;
                }
                if (plotter3.drawingReq == true)
                {
                    if (checkBox1.Checked)
                    {
                        plotter3.drawingFlag = true;
                        Bitmap flag = new Bitmap(pictureBox3.Width, pictureBox3.Height);
                        Graphics flagGraphics = Graphics.FromImage(flag);
                        for (int ii = 0; ii <= plotter3.N; ii++)
                        {
                            FT.calcIFT(plotter3, ii);
                            plotter3.calculateIntpos(1);
                            flag = new Bitmap(pictureBox3.Width, pictureBox3.Height);
                            flagGraphics = Graphics.FromImage(flag);
                            for (int i = 0; i < plotter3.N - 1; i++)
                            {
                                flagGraphics.DrawLine(new Pen(Brushes.Yellow, 1), new Point(plotter3.pos[i].X + 5, pictureBox3.Height - 50 - plotter3.pos[i].Y),
                                    new Point(plotter3.pos[i + 1].X + 5, pictureBox3.Height - 50 - plotter3.pos[i + 1].Y));
                            }
                            pictureBox3.Image = flag;
                        }
                        pictureBox3.Image = flag;
                    }
                    else
                    {
                        plotter3.drawingFlag = true;
                        Bitmap flag = new Bitmap(pictureBox3.Width, pictureBox3.Height);
                        Graphics flagGraphics = Graphics.FromImage(flag);

                        FT.calcIFT(plotter3, plotter3.N);
                        plotter3.calculateIntpos(1);
                        for (int i = 0; i < plotter3.N - 1; i++)
                        {
                            flagGraphics.DrawLine(new Pen(Brushes.Yellow, 1), new Point(plotter3.pos[i].X + 5, pictureBox3.Height - 50 - plotter3.pos[i].Y),
                                new Point(plotter3.pos[i + 1].X + 5, pictureBox3.Height - 50 - plotter3.pos[i + 1].Y));
                            if (i % 7 == 0)
                            {
                                pictureBox3.Image = flag;
                                Thread.Sleep(10);
                            }
                        }

                        pictureBox3.Image = flag;
                    }

                    plotter3.drawingFlag = false;
                    plotter3.drawingReq = false;
                }
                Thread.Sleep(100);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {
                case 0: plotter1.ranPosVal(random.Next(30)); break;
                case 1: plotter1.natPosVal(random.Next(30), random.Next(30)); break;
                case 2: plotter1.RectPosVal(random.Next(50)); break;
                case 3: plotter1.TothsawPosVal(random.Next(50)); break;
                case 4: plotter1.setPosValToDeltaFunction(); break;
                case 5: plotter1.natPosValPlusNoise(random.Next(30), random.Next(30), random.Next(60)); break;
                case 6: plotter1.xTothePowerof2(random.Next(200)); break;
                case 7: plotter1.RectPosVal2(random.Next(60), random.Next(60),random.Next(60)); break;
            }

            if (!plotter1.drawingFlag)
            {
                plotter1.calculateIntpos(1);
                plotter1.drawingReq = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (!plotter1.drawingFlag && !plotter2.drawingFlag)
            {
                FT.calcFT(plotter1);
                FT.calcPS(plotter2);

                plotter2.calculateIntpos(10);

                plotter2.drawingReq = true;
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!plotter3.drawingFlag)
            {
                plotter3.drawingReq = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FT.cutFreq(Convert.ToInt32(textBox1.Text));
            if (!plotter2.drawingFlag)
            {
                FT.calcPS(plotter2);
                plotter2.calculateIntpos(10);
                plotter2.drawingReq = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
