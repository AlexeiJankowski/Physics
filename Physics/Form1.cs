using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Physics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //Uwaga: strasznie banalne komentarze!
        //Uwaga: dalej znajduje się kod, który można by było napisać lepiej ale poświęcę ten czas naucę programowania w technologii, która mnie naprawde interesuje. Z góry przepraszam...
        private Random rand = new Random();
        private double[,] RandomWalk()
        {
            int n; //ilość ruchów
            double fi, x = 0, y = 0;
            double[,] values = null;
            
            Int32.TryParse(textBox1.Text, out n);
            {
                values = new double[2, n];
                values[0, 0] = x;
                values[1, 0] = y;

                for (int i = 1; i < n; i++)
                {
                    //doprecyzowanie Random do potrzeb eksperymentu
                    fi = (double)rand.Next() / Int32.MaxValue * 2 * Math.PI;  
                    x = x + Math.Cos(fi);
                    y = y + Math.Sin(fi);
                    values[0, i] = x;
                    values[1, i] = y;
                }
            }
            return values;                       
        }

        //Wciskamy przycisk
        private void button1_Click(object sender, EventArgs e)
        {       
            //Tworzymy nowy wykres
            var series = new LiveCharts.Wpf.LineSeries
            {
                //Nie najlepszy wybór dla podobnego wykresu ale i tak nie wiemy gdzie się znajdowała cząsteczka w przerwie pomiędzy wykonaniem ruchów 1, 2, 3, ... n, to zostawiłem livecharts, bo nie chiałem dodawać JavaScript
                Values = new LiveCharts.ChartValues<LiveCharts.Defaults.ObservablePoint>(),
                PointGeometry = LiveCharts.Wpf.DefaultGeometries.Circle
            };
            
            int n; //ilość ruchów
            string textBoxParse = textBox1.Text; 
            //sprawdzamy wprowadzone dane
            if (Int32.TryParse(textBoxParse, out n) && n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    //dodajemy ruchy do wykresu
                    series.Values.Add(new LiveCharts.Defaults.ObservablePoint(RandomWalk()[0, i], RandomWalk()[1, i]));
                }
            }
            else
            {
                MessageBox.Show("Wprowadziłeś błędne dane");
            }

            double s = Math.Sqrt(Math.Pow((RandomWalk()[0, n - 1]), 2) + Math.Pow((RandomWalk()[1, n - 1]), 2.0));
            label2.Text = $"Cząsteczka przemieściła się na odległość {s.ToString()}";

            cartesianChart1.Series.Clear();
            cartesianChart1.Series.Add(series);
        }
    }
}
