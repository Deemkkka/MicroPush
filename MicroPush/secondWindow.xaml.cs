using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;
 

namespace MicroPush
{
    /// <summary>
    /// Логика взаимодействия для secondWindow.xaml
    /// </summary>
    public partial class secondWindow : Window
    {
     

        public secondWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += dispatcherTimer_Tick;
        }

        const int row = 77;  // присваиваем значение переменной =  ( количество строк)
        const int column = 77; //присваиваем значение переменной = (количество столбцов)
        Rectangle[,] field = new Rectangle[column, row];
        DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render);


        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            MainWindow sW = new MainWindow();
            sW.Show();
            this.Close();
        }

       

      

        private void Click_MouseDown(object sender, MouseButtonEventArgs e) //  при нажатии на клетку будет окрашиваться в другой цвет
        {
            ((Rectangle)sender).Fill = (((Rectangle)sender).Fill == Brushes.White) ? Brushes.DarkBlue : Brushes.White;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e) //Действие происходит при клике по кнопке Refresh
        {
            secondWindow sW = new secondWindow();
            sW.Show();
            this.Close();
        }
        
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            GoGen();
        }
       
        private void GoGen()
        {
            if((bool)rbtnOpenSpace.IsChecked)
            {
                NewGenOpen();
            }
            else if((bool)rbtnClosedSpace.IsChecked)
            {
                NewGenClosed();
            }
        }

        bool CheckingSpace = true;

        private void HelpWindow()
        {
            
            if (run == false && ((bool)rbtnOpenSpace.IsChecked == false && (bool)rbtnClosedSpace.IsChecked == false))
            {
                MessageBox.Show("Выберите пространство");
            }
        }
        
        bool run = false;
        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            
            if (run)
            {
                
                timer.Stop();
                run = false;
                btnActive.Content = "Activate";
            }
            else
            {
                if (CheckingSpace == true)
                {
                   
                    HelpWindow();
                    
                }
                if((bool)rbtnFieldClear.IsChecked == false && (bool)rbtnFieldFull.IsChecked == false)
                {
                    rbtnFieldClear.IsChecked = true;
                }
               
                timer.Start();
                run = true;
                btnActive.Content = "Pause";
            }
        }
       

        private void RandomGame()
        {
            Random random = new Random();
            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    Rectangle rectangle = new Rectangle(); // прямоугольник(клетка будет в форме прямоугольника)
                    rectangle.Width = Picture1.ActualWidth / row - 2.0; // ширина клетки
                    rectangle.Height = Picture1.ActualHeight / column - 2.0; // высота клетки
                    rectangle.Fill = (random.Next(0, 2) == 1) ? Brushes.White : Brushes.DarkBlue;   //наполняем фон фигуры 
                    Picture1.Children.Add(rectangle); //добавить
                    Canvas.SetLeft(rectangle, y * Picture1.ActualWidth / row);
                    Canvas.SetTop(rectangle, x * Picture1.ActualHeight / column);
                    rectangle.MouseDown += Click_MouseDown;
                    field[x, y] = rectangle;
                }
            }
        }

        private void GameNoRandom()
        {
            
            for (int x = 0; x < column; x++) //делаем с помощью цикла
            {
                for (int y = 0; y < row; y++)
                {
                    Rectangle rectangle = new Rectangle(); // прямоугольник(клетка будет в форме прямоугольника)
                    rectangle.Width = Picture1.ActualWidth / row - 2.0; // ширина клетки
                    rectangle.Height = Picture1.ActualHeight / column - 2.0; // высота клетки
                    rectangle.Fill = Brushes.White;   //наполняем фон фигуры 
                    Picture1.Children.Add(rectangle); //добавить
                    Canvas.SetLeft(rectangle, y * Picture1.ActualWidth / row);
                    Canvas.SetTop(rectangle, x * Picture1.ActualHeight / column);
                    rectangle.MouseDown += Click_MouseDown;
                    field[x, y] = rectangle;
                }
            }
        }

        private void NewGenOpen()
        {
            int[,] numberNeighbors = new int[column, row];
            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    int neighbors = 0;
                    int xOver = x - 1;
                    int xUnder = x + 1;
                    int yLeft = y - 1;
                    int yRight = y + 1;
                    if (xOver < 0)
                    {
                        xOver = column - 1;
                    }
                    if (xUnder == column)
                    {
                        xUnder = 0;
                    }
                    if (yLeft < 0)
                    {
                        yLeft = row - 1;
                    }
                    if (yRight == column)
                    {
                        yRight = 0;
                    }



                    if (field[xOver, yLeft].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[xOver, y].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[xOver, yRight].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[x, yLeft].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[x, yRight].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[xUnder, yLeft].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[xUnder, y].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[xUnder, yRight].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }

                    numberNeighbors[x, y] = neighbors;
                }
            }

            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    if (numberNeighbors[x, y] < 2 || numberNeighbors[x, y] > 3)
                    {
                        field[x, y].Fill = Brushes.White;
                    }
                    else if (numberNeighbors[x, y] == 3)
                    {
                        field[x, y].Fill = Brushes.DarkBlue;
                    }

                }
            }
        }
        private void NewGenClosed()
        {
            int[,] numberNeighbors = new int[column, row];
            for (int x = 1; x < column; x++)
            {
                for (int y = 1; y < row; y++)
                {
                    int neighbors = 0;
                    int xOver = x - 1;
                    int xUnder = x + 1;
                    int yLeft = y - 1;
                    int yRight = y + 1;
                    if (xOver < 0)
                    {
                        xOver = column - 1;
                    }
                    if (xUnder == column)
                    {
                        xUnder = 0;
                    }
                    if (yLeft < 0)
                    {
                        yLeft = row - 1;
                    }
                    if (yRight == column)
                    {
                        yRight = 0;
                    }



                    if (field[xOver, yLeft].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[xOver, y].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[xOver, yRight].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[x, yLeft].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[x, yRight].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[xUnder, yLeft].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[xUnder, y].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }
                    if (field[xUnder, yRight].Fill == Brushes.DarkBlue)
                    {
                        neighbors++;
                    }

                    numberNeighbors[x, y] = neighbors;
                }
            }

            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    if (numberNeighbors[x, y] < 2 || numberNeighbors[x, y] > 3)
                    {
                        field[x, y].Fill = Brushes.White;
                    }
                    else if (numberNeighbors[x, y] == 3)
                    {
                        field[x, y].Fill = Brushes.DarkBlue;
                    }

                }
            }
        }

        private void rbtnFieldClear_Checked(object sender, RoutedEventArgs e)
        {
            Picture1.Children.Clear();
            for (int x = 0; x < column; x++) //делаем с помощью цикла
            {
                for (int y = 0; y < row; y++)
                {
                    Rectangle rectangle = new Rectangle(); // прямоугольник(клетка будет в форме прямоугольника)
                    rectangle.Width = Picture1.ActualWidth / row - 2.0; // ширина клетки
                    rectangle.Height = Picture1.ActualHeight / column - 2.0; // высота клетки
                    rectangle.Fill = Brushes.White;   //наполняем фон фигуры 
                    Picture1.Children.Add(rectangle); //добавить
                    Canvas.SetLeft(rectangle, x * Picture1.ActualWidth / row);
                    Canvas.SetTop(rectangle, y * Picture1.ActualHeight / column);
                    rectangle.MouseDown += Click_MouseDown;
                    field[x, y] = rectangle;
                }
            }
        }

        private void rbtnFieldFull_Checked(object sender, RoutedEventArgs e)
        {
            Picture1.Children.Clear();
            Random random = new Random();
            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    Rectangle rectangle = new Rectangle(); // прямоугольник(клетка будет в форме прямоугольника)
                    rectangle.Width = Picture1.ActualWidth / row - 2.0; // ширина клетки
                    rectangle.Height = Picture1.ActualHeight / column - 2.0; // высота клетки
                    rectangle.Fill = (random.Next(0, 2) == 1) ? Brushes.White : Brushes.DarkBlue;   //наполняем фон фигуры 
                    Picture1.Children.Add(rectangle); //добавить
                    Canvas.SetLeft(rectangle, x * Picture1.ActualWidth / row);
                    Canvas.SetTop(rectangle, y * Picture1.ActualHeight / column);
                    rectangle.MouseDown += Click_MouseDown;
                    field[x, y] = rectangle;
                }
            }
        }
    }
}
