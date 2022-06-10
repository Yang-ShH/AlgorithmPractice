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
            //�������а�ť
            GenerateAllButtons();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shuffle();
        }

        //����˳��
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


        //�������а�ť
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
                    btn.Tag = r * colCount + c;//��ʾ�����ڵ�����λ��
                    //ע���¼�
                    btn.Click += new EventHandler(btn_Click);
                    buttons[r, c] = btn;
                    this.Controls.Add(btn);
                }
        }


        //����������ť
        void Swap(Button btna, Button btnb)
        {
            string t = btna.Text;
            btna.Text = btnb.Text;
            btnb.Text = t;


            bool v = btna.Visible;
            btna.Visible = btnb.Visible;
            btnb.Visible = v;
        }


        //��ť����¼�����
        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;//��ǰ���а�ť
            Button blank = FindHiddenButton();//�հװ�ť
            //�ж���հװ�ť�Ƿ����ڣ�����ǣ�����
            if (IsNeighbor(btn, blank))
            {
                Swap(btn, blank);
                blank.Focus();
            }

            //�ж��Ƿ������
            if (ResultIsOk())
            {
                MessageBox.Show("��ϲ��ɣ�");
            }
        }


        //����Ҫ���صİ�ť
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


        //�ж��Ƿ�����
        bool IsNeighbor(Button btnA, Button btnB)
        {
            int a = (int)btnA.Tag; //Tag�м�¼������λ��
            int b = (int)btnB.Tag;
            int r1 = a / colCount, c1 = a % colCount;
            int r2 = b / colCount, c2 = b % colCount;

            if (r1 == r2 && (c1 == c2 - 1 || c1 == c2 + 1) //��������
                || c1 == c2 && (r1 == r2 - 1 || r1 == r2 + 1))
                return true;
            return false;
        }


        //����Ƿ����
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