using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect_4
{
    public partial class Form1 : Form
    {
        //Main Rectangle Of Connect 4
        Color RectColor;
        Pen RectPen;
        Brush RectBrush;
        Rectangle Frame;
        //Circle OF Connect 4
        Color CircleColor;
        Pen CirclePen;
        Circle [,] CircleArray;

        //Circle[,] CircleArray2; 

        Brush CircleBrush;
        //Flag
        bool flag;
        //Circle Frames
        int Rectlength;
        int Rectheight;
        //Brush For Filling Circle
        Brush FillBrush;
        int selectedrow;
        int selectedcol;
        // x and y
        int y;
        int x;

        //
        int FrameWidth;
        int FrameHeight;
        int rowNum;
        int colNum;
        int[,] size;



        public class Circle
        {
               public int Framelength;
               public int Frameheight;
               public int x;
               public int y;
               public bool Fill=false; //
               public Color color; // yellow or red also could be enum
          

            public  Circle(int w, int h , int xCo, int yCo)
            {
                Framelength= w;
                Frameheight= h;
                x= xCo;
                y= yCo;
                
                
            }

           
        }

        public Form1()
        {
            InitializeComponent();
            
            //Initalizing Frame
            RectColor = Color.Black;
            RectPen= new Pen(Color.Black,3);
            RectBrush= new SolidBrush(Color.Blue);
            //Cricle Frame
            CircleColor= Color.Black;
            CirclePen= new Pen(Color.Black,3);
            CircleBrush= new SolidBrush(Color.White);
            // Flag For Knwing Which player is now playing
            flag = true;
            
          
           


        }

        protected override void OnPaint(PaintEventArgs e)
        {

            // Getting Size From Dialogue 
            SizeDialog frm = new SizeDialog();
            DialogResult dialogResult = frm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                size = frm.BoardSize;
                rowNum = size.GetLength(0);
                colNum = size.GetLength(1);
                CircleArray = new Circle[rowNum, colNum];
                FrameHeight = rowNum * 115;// for col
                FrameWidth = colNum * 110;//for row
            }

            DrawRectangle();
            DrawCircles();
            
        }

        //Draw the frame of the game thats colored blue
        public void DrawRectangle() // FRAME MARSOOM SAH
        {
            Graphics g= this.CreateGraphics();
            Frame = new Rectangle(0, 0, FrameWidth, FrameHeight);
            g.DrawRectangle(RectPen, Frame);
            g.FillRectangle(RectBrush, Frame);
            
        }

        public void DrawCircles()// CIRCLES ARE DRAWN CORRECTLY
        {
            Graphics g= this.CreateGraphics();
            Rectlength= 110/*FrameWidth/colNum*/;
            Rectheight= 115/*FrameHeight/rowNum*/;
            x= 0;
            y= 0;          
            int row =0;
            int col = 0;
            for (int i = 0; i < colNum;i++)//Access By Row
            {
                row++;

                for (int n=0;n<rowNum;n++) //Fill B
                {
                    col++;
                    g.DrawEllipse(CirclePen, x, y, Rectlength, Rectheight);//Drawing Circles
                    g.FillEllipse(CircleBrush,x,y, Rectlength, Rectheight);//Coloring it white
                    CircleArray[n,i] = new Circle(Rectlength, Rectheight, x, y); // Adding Eaxh circle to the Circle array
                    y += Rectheight;
                }

                y = 0;
                x += Rectlength;
            }
            
        }

    

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.X>0 && e.X<FrameWidth && e.Y>0 && e.Y<FrameHeight) //within Bounds of Game
            {
                int column = (e.X * colNum) / FrameWidth; // know Which Column Player Chose
                for(int n=(rowNum-1);n>=0;n--)// selecting row
                {
                   if( CircleArray[n,column].Fill==false)
                    {
                        selectedrow= n;
                        selectedcol= column;
                       // CircleArray[n,column].Fill=true;
                        break;
                    }
                }             
            }
            else
            {
                MessageBox.Show(" Click Was Out Of Bounds");// Click Was Outside Of The Game
            }

            if(flag==true)//Player Red
            {
                bool winLose=false;
                Graphics g=this.CreateGraphics();
                FillBrush=new SolidBrush(Color.Red);
                if (CircleArray[selectedrow,selectedcol].Fill==false)// In Case That the Selected Slot Was not Filled
                {
                    g.FillEllipse(FillBrush, CircleArray[selectedrow, selectedcol].x, CircleArray[selectedrow, selectedcol].y, CircleArray[selectedrow, selectedcol].Framelength, CircleArray[selectedrow, selectedcol].Frameheight);
                    CircleArray[selectedrow, selectedcol].Fill = true;
                    CircleArray[selectedrow, selectedcol].color = Color.Red; // To Add Color To The object
                    flag = false;// Player Red Played, Yellows Turn
                    winLose = CheckWin(Color.Red);
                    if (winLose)
                    {
                        MessageBox.Show("Congrats! Player RED Won The Game");
                        Application.Exit();
                    }
                    if (CheckGameOver())
                    {
                        MessageBox.Show("Game Is Over, No One Won");
                        Application.Exit();
                    }
                }
              
               

            }
            else//Player Yellow
            {
                bool winLose = false;
                Graphics g = this.CreateGraphics();
                FillBrush = new SolidBrush(Color.Yellow);
                if (CircleArray[selectedrow,selectedcol].Fill== false)// In Case That The Selected Slot Was Not Filled 
                {
                    g.FillEllipse(FillBrush, CircleArray[selectedrow, selectedcol].x, CircleArray[selectedrow, selectedcol].y, CircleArray[selectedrow, selectedcol].Framelength, CircleArray[selectedrow, selectedcol].Frameheight);
                    CircleArray[selectedrow, selectedcol].Fill = true; // To Say That This Slot Is Filled And Can No Longer be Used
                    CircleArray[selectedrow, selectedcol].color = Color.Yellow;
                    flag=true; // Player Yellow Already Played , Flag true Means Player Reds turn
                    winLose = CheckWin(Color.Yellow); // Check If Yellow Won
                    if (winLose)
                    {
                        MessageBox.Show("Congrats! Player YELLOW Won The Game");
                        Application.Exit();
                    }
                    if (CheckGameOver())// Checking If The Game Was Over And All Slots Are Filled
                    {
                        MessageBox.Show("Game Is Over, No One Won");
                        Application.Exit();
                    }
                }
               
            }

        }// End Of Onclick Fn

        public bool CheckWin(Color player)
        {
            bool WinOrNot = false;
            //Checking For Horizontal Wins

            for (int i = 0; i < rowNum; i++)// Full Rows length
            {
                for (int j = 0; j < (colNum-3); j++)//Columns Length -3
                {
                    if (CircleArray[i, j].color == player &&
                        CircleArray[i, j + 1].color == player &&
                        CircleArray[i, j + 2].color == player &&
                        CircleArray[i, j + 3].color == player)
                    {
                        //Console.WriteLine("Win Detected at Row{0}, Column{1}", i, j);
                        WinOrNot = true;
                    }
                }
            }

            ////Check For Vertical Wins 
            for (int i = 0; i < (rowNum-3); i++)//Row Length - 3
            {
                for (int j = 0; j < colNum; j++)//Full Column Length
                {
                    if (CircleArray[i, j].color == player &&
                        CircleArray[i + 1, j].color == player &&
                        CircleArray[i + 2, j].color == player &&
                        CircleArray[i + 3, j].color == player)
                    {
                        WinOrNot = true;
                    }
                }
            }
            //Check For Downward Diagonal "\"
            for (int i = 0; i < (rowNum-3); i++)
            {
                for (int j = 0; j < (colNum-3); j++)
                {
                    if (CircleArray[i, j].color == player &&
                        CircleArray[i + 1, j + 1].color == player &&
                        CircleArray[i + 2, j + 2].color == player &&
                        CircleArray[i + 3, j + 3].color == player)
                    {
                        WinOrNot = true;
                    }
                }
            }
            //Check For Upward Diagonal "/"
            for (int i = 0; i < (rowNum-3); i++) // Rows Length -3
            {
                for (int j = (colNum-3); j > 2; j--) // Decrementing Full Comlumns Lengths Stopping And Including 3
                {
                    if (CircleArray[i, j].color == player &&
                       CircleArray[i + 1, j - 1].color == player &&
                       CircleArray[i + 2, j - 2].color == player &&
                       CircleArray[i + 3, j - 3].color == player)
                    {
                        WinOrNot = true;
                    }
                }
            }
            return WinOrNot;
        }// End Of Check Win FUnction

        //Check If No Wins And Game Is Over
        public bool CheckGameOver ()
        {
            bool GameOver=false;
            int count = 0;
            for (int i=0;i<rowNum;i++) // Looping In Array To Check If All Circles Are Filled
            {
                for( int j=0; j<colNum;j++)
                {
                    if(CircleArray[i,j].Fill==true)
                    {
                        count++;
                    }    
                }
            }
            if( count==42)//Meaning All Circles Are Filled And no Wins Means Game Is Over
            {
                 GameOver=true;
            }
            return GameOver;
        }
    }
}
