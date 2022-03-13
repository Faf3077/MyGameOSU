using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGameCSGO
{
    public partial class Form1 : Form
    {
        public Bitmap HandlerTexture = Resource1.Handler,
                      TargetTexture = Resource1.Target;
        private Point _targetPosition = new Point(200, 200);
        private Point _direction = Point.Empty;
        //текущее количество очков, которое изначально равно 0
        private int _score = 0;
        public Form1()
        {
            InitializeComponent();
            //добавляю атрибуты для того чтобы избежать мерцания картинок 
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint, true);
            UpdateStyles();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Refresh();//для перерисовки формы, для отображения движения
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            //каждый тик таймера меняет директорию на какое то случайне число
            Random r = new Random();
            timer2.Interval = r.Next(25, 1000);
            _direction.X = r.Next(-1, 2);
            _direction.Y = r.Next(-1, 2);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics a = e.Graphics;
            //Для определения курсора на форме 
            var localPosition = this.PointToClient(Cursor.Position);
            //уеличиваю скорость передвижениния круга по осям 
            _targetPosition.X += _direction.X * 10;
            _targetPosition.Y += _direction.Y * 10;
            if (_targetPosition.X < 0 || _targetPosition.X > 500)
            {
                //если условие круг выходит за рамки формы, то он отскакивает(меняет направление)
                _direction.X *= -1;

            }
            if (_targetPosition.Y < 0 || _targetPosition.Y > 500)
            {
                //если условие круг выходит за рамки формы, то он отскакивает
                _direction.Y *= -1;
            }

            //проежуточная точка 
            Point between = new Point(localPosition.X - _targetPosition.X, localPosition.Y - _targetPosition.Y);
            float distance = (float)Math.Sqrt((between.X * between.X) + (between.Y * between.Y)); //сумма квадратов катетов, для работы системы начисления очков 

            //если попадает в круг, то идет начиление очков 
            if (distance < 10)//точность попадания в круг
            {
                AddScore(10);
            }
            

            var handlerRect = new Rectangle(localPosition.X - 50, localPosition.Y - 50, 100, 100);
            var targetRect = new Rectangle(_targetPosition.X - 50, _targetPosition.Y - 50, 100, 100);

            //для показа кругов на форме    
            a.DrawImage(HandlerTexture, handlerRect);
            a.DrawImage(TargetTexture, targetRect);

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void AddScore(int score)
        {
            _score += score;
            scoreLabel.Text = _score.ToString();

        }
    }
}
