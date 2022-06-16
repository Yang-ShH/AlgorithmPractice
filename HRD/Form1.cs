using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HRD
{
    public partial class Form1 : Form
    {
        public static MapJson OutPutJson = Program.GetConfig();
        public static readonly int RowCount = OutPutJson.rowCount;
        public static readonly int ColCount = OutPutJson.colCount;
        //public static readonly int RowCount = Program.GetConfig<int>("rowCount");
        //public static readonly int ColCount = Program.GetConfig<int>("colCount");
        public static Button[,] _buttons = new Button[RowCount, ColCount];
        public static List<NodeInfo> Map = new();
        
        public Form1()
        {
            InitializeComponent();
            
            //this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pMouseWheel);
        }

        //private void pMouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    //MessageBox.Show("�����¼��ѱ���׽");
        //    Size t = new Size(0, 0); //t������Ϊ���ֹ����ı仯ֵ���Խ�Ͽؼ��ĳߴ�(+t)��ʵ�ֹ�����š�
        //    t.Width += e.Delta;
        //    t.Height += e.Delta;
        //    foreach (var button in _buttons)
        //    {
        //        button.Size = t;
        //    }

        //    Refresh();
        //}


        private void Form1Load(object sender, EventArgs e)
        {
            //�������а�ť
            GenerateAllButtons();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            Shuffle();
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
        }

        //����˳��
        private void Shuffle()
        {
            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColCount; c++)
                {
                    //var num = Program.GetConfig<int>($"matrix:{r}:{c}");
                    var num = OutPutJson.matrix[r,c];

                    _buttons[r, c].Text = num.ToString();
                    _buttons[r, c].Visible = num != 0;
                }
        }


        //�������а�ť
        private void GenerateAllButtons()
        {
            var panel1Width = this.panel1.Width;
            var btnWidth = 30;
            //int x0 = (btnWidth * ColCount) > (Width - panel1Width) ? 110 : (Width - panel1Width - (btnWidth * ColCount)) / 2 + panel1Width;
            //int y0 = (btnWidth * RowCount) > Height ? 20 : (Height - (btnWidth * RowCount)) / 2;
            int x0 = 110;
            int y0 = 10;
            //int sideLength = Math.Min((Width - (2 * x0)) / ColCount, (Height - (2 * y0)) / RowCount);
            //sideLength = sideLength > 65 ? 65 : sideLength;
            int w = 50;
            int h = btnWidth;

            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColCount; c++)
                {
                    var num = OutPutJson.matrix[r, c];
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
            btnA.Refresh();
            btnB.Refresh();
        }

        private static void Swap(int aX, int aY, int bX, int By)
        {
            var btnA = _buttons[aX, aY];
            var btnB = _buttons[bX, By];
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
        public static bool IsNeighbor(Button btnA, Button btnB)
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
                    var text = _buttons[r, c].Text;
                    if (text != (r * ColCount + c + 1).ToString())
                    {
                        if (text == 0.ToString() && r * ColCount + c + 1 != r * c)
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            return true;
        }

        private void Button2Click(object sender, EventArgs e)
        {
            Button blank = FindHiddenButton();
            if ((int)blank.Tag != RowCount * ColCount - 1)
            {
                if (MessageBox.Show("�ո��ڳ�ʼλ�ã������ú����ԣ�", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Shuffle();
                }
                //MessageBox.Show("�ո��ڳ�ʼλ�ã������ú����ԣ�", "��ʾ");
            }
            else
            {
                var listNum = new List<int>();
                foreach (var button in _buttons)
                {
                    listNum.Add(int.Parse(button.Text));
                }
                listNum.Remove(0);
                var inverse = CalcIvnCount(listNum);
                //MessageBox.Show($"��������{inverse}");
                if (inverse % 2 == 0)
                {
                    MessageBox.Show("�û��ݵ��н⣡");
                    OutPutJson.recoverable = true;
                }
                else
                {
                    MessageBox.Show("�û��ݵ��޽⣡");
                    OutPutJson.recoverable = false;
                }
            }
        }

        private int CalcIvnCount(List<int> inputList)
        {
            var result = 0;
            for (int i = 0; i < inputList.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (inputList[j] > inputList[i])
                    {
                        result += 1;
                    }
                }
            }

            return result;
        }

        //private void Button3Click(object sender, EventArgs e)
        //{
        //    textBox1.Text = string.Empty;
        //    var listNum = new List<int>();
        //    foreach (var button in _buttons)
        //    {
        //        listNum.Add(int.Parse(button.Text));
        //    }
        //    var map = new CalcPath(listNum, RowCount, ColCount);
        //    var tarNum = listNum.Where(e => e != 0).OrderBy(e => e).ToList();
        //    foreach (var item in tarNum)
        //    {
        //        var targetPos = new NodePos
        //        {
        //            X = item % ColCount == 0 ? item / ColCount - 1 : item / ColCount,
        //            Y = (item % ColCount == 0 ? ColCount : item % ColCount) - 1
        //        };
        //        var itemNode = Map.First(e => e.Text == item);
        //        if (itemNode.X == targetPos.X && itemNode.Y == targetPos.Y)
        //        {
        //            itemNode.Fixed = true;
        //            continue;
        //        }
        //        var route = map.CalcRoute(item, targetPos);
        //        char[] routeStr = route.Select(e => (char)((e.X * ColCount) + e.Y)).ToArray();
        //        var result = map.CalcSpaceRoute(route);
        //        textBox1.Text += result;
        //    }
        //}

        private async void Button3Click(object sender, EventArgs e)
        {
            OutPutJson.stepList = string.Empty;
            var sw = Stopwatch.StartNew();

            var routeString = Task.Run(() =>
            {
                var result = string.Empty;
                var listNum = new List<int>();
                foreach (var button in _buttons)
                {
                    listNum.Add(int.Parse(button.Text));
                }
                var map = new CalcPath(listNum, RowCount, ColCount);
                var tarNum = listNum.Where(e => e != 0).OrderBy(e => e).ToList();
                var currentRow = RowCount;
                var currentCol = ColCount;
                while (currentRow > 2)
                {
                    var currentRowNodes = new List<int>();
                    for (int i = ColCount - currentCol; i < ColCount; i++)
                    {
                        currentRowNodes.Add((RowCount - currentRow) * ColCount + i + 1);
                    }

                    foreach (var item in currentRowNodes)
                    {
                        result += CalcOneNodeRoute(map, item);
                        tarNum.Remove(item);
                    }

                    var currentColNodes = new List<int>();

                    for (int i = 1; i < currentRowNodes.Count; i++)
                    {
                        currentColNodes.Add(currentRowNodes.First() + ColCount * i);
                    }
                    foreach (var item in currentColNodes)
                    {
                        result += CalcOneNodeRoute(map, item);
                        tarNum.Remove(item);
                    }
                    currentRow--;
                    currentCol--;
                }
                foreach (var item in tarNum)
                {
                    result += CalcOneNodeRoute(map, item);
                }
                return result;
            });

            textBox1.Text = await routeString;
            sw.Stop();
            textBox2.Text = $"�����ʱ��{sw.ElapsedMilliseconds} ����\r\n";
            var route = textBox1.Text.Split(",").Where(e => !string.IsNullOrEmpty(e)).ToArray();
            textBox2.Text += $"���� {route.Length} ��";
            OutPutJson.stepCount = route.Length;
            OutPutJson.duration = (int)(sw.ElapsedMilliseconds / 1000);
            OutPutJsonResult();
        }

        private string CalcOneNodeRoute(CalcPath map, int node)
        {
            var targetPos = new NodePos
            {
                X = node % ColCount == 0 ? node / ColCount - 1 : node / ColCount,
                Y = (node % ColCount == 0 ? ColCount : node % ColCount) - 1
            };
            var itemNode = Map.First(e => e.Text == node);
            if (itemNode.X == targetPos.X && itemNode.Y == targetPos.Y)
            {
                itemNode.Fixed = true;
                return string.Empty;
            }
            var route = map.CalcRoute(node, targetPos);
            char[] routeStr = route.Select(e => (char)((e.X * ColCount) + e.Y)).ToArray();
            var result = map.CalcSpaceRoute(route);
            //textBox1.Text += result;
            return result;
        }

        private void OutPutJsonResult()
        {
            string path = "OutPut.json";
            if (!File.Exists(path))
            {
                var file = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                file.Close();
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(OutPutJson));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var listNum = new List<int>();
            foreach (var button in _buttons)
            {
                listNum.Add(int.Parse(button.Text));
            }
            var map = new CalcPath(listNum, RowCount, ColCount);
            var route = textBox1.Text.Split(",").Where(e => !string.IsNullOrEmpty(e)).ToArray();
            foreach (var item in route)
            {
                var num = Convert.ToInt32(item);
                var spaceNode = Map.First(e => e.Text == 0);
                var spaceBtn = _buttons[spaceNode.X, spaceNode.Y];
                var currentNode = Map.First(e => e.Text == num);
                var currentBtn = _buttons[currentNode.X, currentNode.Y];
                Swap(spaceBtn, currentBtn);
                (spaceNode.X, currentNode.X) = (currentNode.X, spaceNode.X);
                (spaceNode.Y, currentNode.Y) = (currentNode.Y, spaceNode.Y);
                currentBtn.Focus();
                Thread.Sleep(7);
            }

            MessageBox.Show("��ɣ�");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //var mapJson = JsonConvert.DeserializeObject<MapJson>(textBox2.Text);
            //using (System.IO.StreamWriter file = new System.IO.StreamWriter("appsettings.json", false))
            //{
            //    file.Write(mapJson);//ֱ��׷���ļ�ĩβ��������
            //}

            //RowCount = Program.GetConfig<int>("rowCount");
            //ColCount = Program.GetConfig<int>("colCount");
            //_buttons = new Button[RowCount, ColCount];

            //GenerateAllButtons();
            //this.Refresh();
        }

    }

    public class MapJson
    {
        public int rowCount { get; set; }
        public int colCount { get; set; }
        public int[,] matrix { get; set; }
        /// <summary>
        /// ��ԭ����
        /// </summary>
        public int stepCount { get; set; }
        /// <summary>
        /// �ָ�����
        /// </summary>
        public string stepList { get; set; }
        /// <summary>
        /// �����ʱ
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// �Ƿ�ɻ�ԭ
        /// </summary>
        public bool recoverable { get; set; }
    }
}