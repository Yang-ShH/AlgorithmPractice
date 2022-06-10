namespace HRD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int rowCount = Program.GetConfig<int>("rowCount");
        static int colCount = Program.GetConfig<int>("colCount");

        Button[,] buttons = new Button[rowCount, colCount];


        private void Form1_Load(object sender, EventArgs e)
        {
            //产生所有按钮
            GenerateAllButtons();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shuffle();
        }

        //打乱顺序
        void Shuffle()
        {
            for (int r = 0; r < rowCount; r++)
                for (int c = 0; c < colCount; c++)
                {
                    var num = Program.GetConfig<int>($"matrix:{r}:{c}");
                    buttons[r, c].Text = num.ToString();
                    buttons[r,c].Visible = num != 0;
                }
        }


        //生成所有按钮
        void GenerateAllButtons()
        {
            int x0 = (65 * colCount) > Width ? 60 : (Width - (65 * colCount)) / 2;
            int y0 = (65 * rowCount) > Height ? 60 : (Height - 60 - (65 * rowCount)) / 2;
            int sideLength = Math.Min((Width - (2 * x0)) / colCount, (Height - (2 * y0)) / rowCount);
            sideLength = sideLength > 65 ? 65 : sideLength;
            int w = sideLength;
            int h = sideLength;
            
            for (int r = 0; r < rowCount; r++)
                for (int c = 0; c < colCount; c++)
                {
                    var num = Program.GetConfig<int>($"matrix:{r}:{c}");
                    Button btn = new Button();
                    btn.Text = num.ToString();
                    btn.Top = y0 + r * h;
                    btn.Left = x0 + c * w;
                    btn.Width = w;
                    btn.Height = h;
                    btn.Visible = num != 0;
                    btn.Tag = r * colCount + c;//表示它所在的行列位置
                    //注册事件
                    btn.Click += new EventHandler(btn_Click);
                    buttons[r, c] = btn;
                    this.Controls.Add(btn);
                }
        }


        //交换两个按钮
        void Swap(Button btna, Button btnb)
        {
            string t = btna.Text;
            btna.Text = btnb.Text;
            btnb.Text = t;


            bool v = btna.Visible;
            btna.Visible = btnb.Visible;
            btnb.Visible = v;
        }


        //按钮点击事件处理
        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;//当前点中按钮
            Button blank = FindHiddenButton();//空白按钮
            //判断与空白按钮是否相邻，如果是，交换
            if (IsNeighbor(btn, blank))
            {
                Swap(btn, blank);
                blank.Focus();
            }

            //判断是否完成了
            if (ResultIsOk())
            {
                MessageBox.Show("恭喜完成！");
            }
        }


        //查找要隐藏的按钮
        Button FindHiddenButton()
        {
            for (int r = 0; r < rowCount; r++)
                for (int c = 0; c < colCount; c++)
                {
                    if (!buttons[r, c].Visible)
                    {
                        return buttons[r, c];
                    }
                }
            return new Button();
        }


        //判断是否相邻
        bool IsNeighbor(Button btnA, Button btnB)
        {
            int a = (int)btnA.Tag; //Tag中记录是行列位置
            int b = (int)btnB.Tag;
            int r1 = a / colCount, c1 = a % colCount;
            int r2 = b / colCount, c2 = b % colCount;

            if (r1 == r2 && (c1 == c2 - 1 || c1 == c2 + 1) //左右相邻
                || c1 == c2 && (r1 == r2 - 1 || r1 == r2 + 1))
                return true;
            return false;
        }


        //检查是否完成
        bool ResultIsOk()
        {
            for (int r = 0; r < rowCount; r++)
                for (int c = 0; c < colCount; c++)
                {
                    if (buttons[r, c].Text != (r * colCount + c + 1).ToString())
                    {
                        return false;
                    }
                }
            return true;
        }
    }
}