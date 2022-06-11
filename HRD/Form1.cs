namespace HRD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static readonly int RowCount = Program.GetConfig<int>("rowCount");
        private static readonly int ColCount = Program.GetConfig<int>("colCount");
        private readonly Button[,] _buttons = new Button[RowCount, ColCount];


        private void Form1Load(object sender, EventArgs e)
        {
            //�������а�ť
            GenerateAllButtons();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            Shuffle();
        }

        //����˳��
        private void Shuffle()
        {
            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColCount; c++)
                {
                    var num = Program.GetConfig<int>($"matrix:{r}:{c}");
                    _buttons[r, c].Text = num.ToString();
                    _buttons[r,c].Visible = num != 0;
                }
        }


        //�������а�ť
        private void GenerateAllButtons()
        {
            var panel1Width = this.panel1.Width;
            int x0 = (65 * ColCount) > (Width - panel1Width) ? 110 : (Width - panel1Width - (65 * ColCount)) / 2 + panel1Width;
            int y0 = (65 * RowCount) > Height ? 60 : (Height - (65 * RowCount)) / 2;
            //int sideLength = Math.Min((Width - (2 * x0)) / ColCount, (Height - (2 * y0)) / RowCount);
            //sideLength = sideLength > 65 ? 65 : sideLength;
            int w = 65;
            int h = 65;
            
            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColCount; c++)
                {
                    var num = Program.GetConfig<int>($"matrix:{r}:{c}");
                    Button btn = new Button();
                    btn.Text = num.ToString();
                    btn.Top = y0 + r * h;
                    btn.Left = x0 + c * w;
                    btn.Width = w;
                    btn.Height = h;
                    btn.Visible = num != 0;
                    btn.Margin = new Padding(0, 0, 0, 0);
                    btn.Tag = r * ColCount + c;//��ʾ�����ڵ�����λ��
                    //ע���¼�
                    btn.Click += ButtonClick;
                    _buttons[r, c] = btn;
                    this.Controls.Add(btn);
                }
        }


        //����������ť
        private static void Swap(Button btnA, Button btnB)
        {
            (btnA.Text, btnB.Text) = (btnB.Text, btnA.Text);
            (btnA.Visible, btnB.Visible) = (btnB.Visible, btnA.Visible);
        }


        //��ť����¼�����
        private void ButtonClick(object sender, EventArgs e)
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
        private Button FindHiddenButton()
        {
            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColCount; c++)
                {
                    if (!_buttons[r, c].Visible)
                    {
                        return _buttons[r, c];
                    }
                }
            return new Button();
        }


        //�ж��Ƿ�����
        private static bool IsNeighbor(Button btnA, Button btnB)
        {
            int a = (int)btnA.Tag; //Tag�м�¼������λ��
            int b = (int)btnB.Tag;
            int r1 = a / ColCount, c1 = a % ColCount;
            int r2 = b / ColCount, c2 = b % ColCount;

            if (r1 == r2 && (c1 == c2 - 1 || c1 == c2 + 1) //��������
                || c1 == c2 && (r1 == r2 - 1 || r1 == r2 + 1))
                return true;
            return false;
        }


        //����Ƿ����
        private bool ResultIsOk()
        {
            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColCount; c++)
                {
                    if (_buttons[r, c].Text != (r * ColCount + c + 1).ToString())
                    {
                        return false;
                    }
                }
            return true;
        }
    }
}