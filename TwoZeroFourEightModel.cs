using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twozerofoureight
{
    class TwoZeroFourEightModel : Model
    {
        protected int boardSize; // default is 4
        protected int[,] board;
        protected Random rand;

        public TwoZeroFourEightModel() : this(4)
        {
            // default board size is 4 
        }

        public TwoZeroFourEightModel(int size)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            var range = Enumerable.Range(0, boardSize);
            foreach(int i in range) {
                foreach(int j in range) {
                    board[i,j] = 0;
                }
            }
            rand = new Random();
            board = Random(board);
            NotifyAll();
        }

        public int[,] GetBoard()
        {
            return board;
        }      
        private int[,] Random(int[,] input)
        {                    
            while (game())
            {
                int x = rand.Next(boardSize);
                int y = rand.Next(boardSize);
                if (board[x, y] == 0)
                {
                    board[x, y] = 2;
                    break;
                }
            }
                
            return input;
        }
        public bool game()
        {
            bool check = false ;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == 0) check = true;
                }
            }
            if (check == false) gameover();
            return check;
        }
        public void gameover()
        {
            
                if (endcheck() == 0)
                {
                    System.Windows.Forms.MessageBox.Show("Game Over");
                    
                }
            
        }
        public int endcheck()
        {
            int tcheck = 0;
            for (int x = 0; x < 4; x++)
            {
                for(int y = 0; y < 4; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        if (board[x, y] == board[x, y + 1] || board[x, y] == board[x + 1, y]) tcheck = 1;
                    }
                    else if (x == 3 && y == 0)
                    {
                        if (board[x, y] == board[x, y + 1] || board[x, y] == board[x - 1, y]) tcheck = 1;
                    }
                    else if (x == 0 && y == 3)
                    {
                        if (board[x, y] == board[x, y - 1] || board[x, y] == board[x + 1, y]) tcheck = 1;
                    }
                    else if (x == 3 && y == 3)
                    {
                        if (board[x, y] == board[x, y - 1] || board[x, y] == board[x - 1, y]) tcheck = 1;
                    }
                    else if (x == 0 && (y != 0 && y != 3))
                    {
                        if (board[x, y] == board[x, y - 1] || board[x, y] == board[x, y + 1] || board[x, y] == board[x + 1, y]) tcheck = 1;
                    }
                    else if(x==3 && (y != 0 && y != 3))
                    {
                        if (board[x, y] == board[x, y - 1] || board[x, y] == board[x, y + 1] || board[x, y] == board[x - 1, y]) tcheck = 1;
                    }
                    else if(y==0 && (x != 0 && x != 3))
                    {
                        if (board[x, y] == board[x-1, y] || board[x, y] == board[x+1, y] || board[x, y] == board[x, y+1]) tcheck = 1;
                    }
                    else if(y==3 && (x != 0 && x != 3))
                    {
                        if (board[x, y] == board[x - 1, y] || board[x, y] == board[x + 1, y] || board[x, y] == board[x, y - 1]) tcheck = 1;
                    }
                    else
                    {
                        if (board[x, y] == board[x - 1, y] || board[x, y] == board[x + 1, y] || board[x, y] == board[x, y - 1]|| board[x, y] == board[x, y + 1]) tcheck = 1;
                    }
                    
                }
            }
            return tcheck;
        }

    

        public void PerformDown()
        {
            int[] buffer;
            int pos;
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeY);
            foreach (int i in rangeX)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeX)
                {
                    buffer[k] = 0;
                }
                // shift down
                foreach (int j in rangeY)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeX)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                    }
                }
                // shift down again
                pos = 3;
                foreach (int j in rangeX)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[k, i] = 0;
                }
            }
            board = Random(board);
            NotifyAll();
         
        }

        public void PerformUp()
        {
            int[] buffer;
            int pos;

            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                // shift up
                foreach (int j in range)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                    }
                }
                // shift up again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos++;
                    }
                }
                // copy back
                for (int k = pos; k != boardSize; k++)
                {
                    board[k, i] = 0;
                }
            }
            board = Random(board);
            NotifyAll();
           
        }

        public void PerformRight()
        {
            int[] buffer;
            int pos;

            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeX);
            foreach (int i in rangeY)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeY)
                {
                    buffer[k] = 0;
                }
                // shift right
                foreach (int j in rangeX)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeY)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                    }
                }
                // shift right again
                pos = 3;
                foreach (int j in rangeY)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[i, k] = 0;
                }
            }
            board = Random(board);
            NotifyAll();
           
        }

        public void PerformLeft()
        {
            int[] buffer;
            int pos;
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[boardSize];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                // shift left
                foreach (int j in range)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                    }
                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos++;
                    }
                }
                for (int k = pos; k != boardSize; k++)
                {
                    board[i, k] = 0;
                }
            }
            board = Random(board);
            NotifyAll();
            
        }
    }
}
