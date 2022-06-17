using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRD
{
    public class CalcPath
    {
        private List<NodeInfo> OptionalNodes = new List<NodeInfo>();
        private List<NodeInfo> WalkedNodes = new List<NodeInfo>();
        public CalcPath(List<int> buttonText, int row, int col)
        {
            Form1.Map.Clear();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Form1.Map.Add(new NodeInfo
                    {
                        Text = buttonText[i * col + j],
                        X = i,
                        Y = j,
                        Fixed = false,
                        Finded = false,
                        ParentNode = null
                    });
                }
            }
        }
        /// <summary>
        /// 搜索邻近节点（顺序：上左下右）
        /// </summary>
        /// <param name="currentNode"></param>
        private List<int> CalcOptionalNodes(NodeInfo currentNode)
        {
            var result = new List<int>();
            var currentX = currentNode.X;
            var currentY = currentNode.Y;
            var upX = currentX - 1;
            if (upX >= 0)
            {
                var nextNode = Form1.Map.First(e => e.X == upX && e.Y == currentY);
                var seccess = AddNextNode(currentNode, nextNode);
                if (seccess)
                {
                    result.Add(nextNode.Text);
                }
            }
            var leftY = currentY - 1;
            if (leftY >= 0)
            {
                var nextNode = Form1.Map.First(e => e.X == currentX && e.Y == leftY);
                var seccess = AddNextNode(currentNode, nextNode);
                if (seccess)
                {
                    result.Add(nextNode.Text);
                }
            }
            var downX = currentX + 1;
            if (downX < Form1.RowCount)
            {
                var nextNode = Form1.Map.First(e => e.X == downX && e.Y == currentY);
                var seccess = AddNextNode(currentNode, nextNode);
                if (seccess)
                {
                    result.Add(nextNode.Text);
                }
            }
            var rightY = currentY + 1;
            if (rightY < Form1.ColCount)
            {
                var nextNode = Form1.Map.First(e => e.X == currentX && e.Y == rightY);
                var seccess = AddNextNode(currentNode, nextNode);
                if (seccess)
                {
                    result.Add(nextNode.Text);
                }
            }
            return result;
        }
        /// <summary>
        /// 添加下一可选节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nextNode"></param>
        private bool AddNextNode(NodeInfo parentNode, NodeInfo nextNode)
        {
            if (!nextNode.Finded && !nextNode.Fixed)
            {
                nextNode.ParentNode = parentNode;
                nextNode.Finded = true;
                OptionalNodes.Add(nextNode);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 计算路径
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<NodePos> CalcRoute(int start, NodePos targetPos)
        {
            var endTarget = Form1.Map.First(e => e.X == targetPos.X && e.Y == targetPos.Y);
            var end = endTarget.Text;
            if (start == end)
            {
                return new List<NodePos>();
            }
            OptionalNodes.Clear();
            WalkedNodes.Clear();
            foreach (var item in Form1.Map)
            {
                item.ParentNode = null;
                item.Finded = false;
            }
            var startNode = Form1.Map.First(e => e.Text == start);
            startNode.Finded = true;
            OptionalNodes.Add(startNode);
            while (!WalkedNodes.Exists(e => e.Text == end))
            {
                while (OptionalNodes.Count > 0)
                {
                    var node = OptionalNodes.First();
                    var nextNum = CalcOptionalNodes(node);
                    
                    OptionalNodes.Remove(node);
                    WalkedNodes.Add(node);
                    if (OptionalNodes.Exists(e => e.Text == end))
                    {
                        WalkedNodes.Add(OptionalNodes.First(e => e.Text == end));
                        break;
                    }
                }
            }
            var endNode = WalkedNodes.First(e => e.Text == end);
            var route = OutputRoute(endNode);
            route.Reverse();
            return route;
        }
        /// <summary>
        /// 输出路径
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<NodePos> OutputRoute(NodeInfo node)
        {
            var result = new List<NodePos>();
            result.Add(new NodePos { X = node.X, Y = node.Y});
            node.Finded = false;
            if (node.ParentNode != null)
            {
                result.AddRange(OutputRoute(node.ParentNode));
                node.ParentNode = null;
            }

            return result;
        }

        public List<int> CalcSpaceRoute(List<NodePos> nodeMoveRoute)
        {
            var spaceStart = Form1.Map.First(e => e.Text == 0);
            var firstNode = Form1.Map.First(e => e.X == nodeMoveRoute[0].X && e.Y == nodeMoveRoute[0].Y);
            var firstText = firstNode.Text;
            firstNode.Fixed = true;
            nodeMoveRoute.Remove(nodeMoveRoute.First());
            var result = new List<int>();
            while (nodeMoveRoute.Count > 0)
            {
                var currentEndNode = nodeMoveRoute.First();
                nodeMoveRoute.Remove(currentEndNode);
                var currentRoute = new List<NodePos>();
                if (currentEndNode.Y == Form1.ColCount - 1
                    && nodeMoveRoute.Count == 0
                    && currentEndNode.X < Form1.RowCount - 2
                    && currentEndNode.X != spaceStart.X)
                {
                    var currentRow = Form1.RowCount - currentEndNode.X;

                    currentRoute.AddRange(CalcRoute(0, new NodePos
                    {
                        X = currentEndNode.X + 1,
                        Y = currentEndNode.X
                    }));
                    if (currentRoute.Any())
                    {
                        foreach (var item in currentRoute)
                        {
                            result.AddRange(MoveSpace(item));
                        }
                        currentRoute.Clear();
                    }

                    currentRoute.AddRange(CalcSpecialRoute1(currentEndNode.X, currentRow, currentRow));
                }
                else if (currentEndNode.X == Form1.RowCount - 1 && currentEndNode.Y < Form1.ColCount - 2
                    && currentEndNode.Y != spaceStart.Y
                    && nodeMoveRoute.Count == 0)
                {
                    var currentCol = Form1.ColCount - currentEndNode.Y;

                    currentRoute.AddRange(CalcRoute(0, new NodePos
                    {
                        X = currentEndNode.Y + 1,
                        Y = Form1.ColCount - 1
                    }));
                    if (currentRoute.Any())
                    {
                        foreach (var item in currentRoute)
                        {
                            result.AddRange(MoveSpace(item));
                        }
                        currentRoute.Clear();
                    }
                    currentRoute.AddRange(CalcSpecialRoute2(currentEndNode.X, currentCol, currentCol));
                }
                else
                {
                    currentRoute = CalcRoute(0, currentEndNode);
                }
                foreach (var item in currentRoute)
                {
                    result.AddRange(MoveSpace(item));
                }

                result.AddRange(MoveSpace(new NodePos { X = firstNode.X, Y = firstNode.Y }));
            }
            return result;
        }


        public List<NodePos> CalcSpecialRoute3(int moveRowNo)
        {
            var result = new List<NodePos>();
            for (int i = 0; i < Form1.ColCount - 1; i++)
            {
                result.Add(new NodePos
                {
                    X = moveRowNo,
                    Y = i
                });
            }
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = Form1.ColCount - 1
            });
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = Form1.ColCount - 2
            });
            for (int i = Form1.ColCount - 2; i >= 0; i--)
            {
                result.Add(new NodePos
                {
                    X = moveRowNo,
                    Y = i
                });
            }
            for (int i = 0; i < Form1.ColCount - 1; i++)
            {
                result.Add(new NodePos
                {
                    X = moveRowNo + 1,
                    Y = i
                });
            }
            for (int i = Form1.ColCount - 1; i >= Form1.ColCount - 2; i++)
            {
                result.Add(new NodePos
                {
                    X = moveRowNo,
                    Y = i
                });
            }
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = Form1.ColCount - 2
            });

            return result;
        }

        public List<NodePos> CalcSpecialRoute2(int moveRowNo)
        {
            var result = new List<NodePos>();
            for (int i = 0; i < Form1.ColCount - 1; i++)
            {
                result.Add(new NodePos
                {
                    X = moveRowNo,
                    Y = i
                });
            }
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = Form1.ColCount - 1 - 1
            });
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = Form1.ColCount - 1
            });
            for (int i = Form1.ColCount - 1; i >= 0; i--)
            {
                result.Add(new NodePos
                {
                    X = moveRowNo,
                    Y = i
                });
            }
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = 0
            });
            return result;
        }

        public List<NodePos> CalcSpecialRoute1(int moveRowNo)
        {
            var result = new List<NodePos>();
            for (int i = 0; i < Form1.ColCount; i++)
            {
                result.Add(new NodePos
                {
                    X = moveRowNo,
                    Y = i
                });
            }
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = Form1.ColCount - 1
            });
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = Form1.ColCount - 2
            });
            for (int i = Form1.ColCount - 2; i >= 0; i--)
            {
                result.Add(new NodePos
                {
                    X = moveRowNo,
                    Y = i
                });
            }
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = 0
            });
            return result;
        }

        public List<NodePos> CalcSpecialRoute1(int moveRowNo, int row = 0, int col = 0)
        {
            var result = new List<NodePos>();
            for (int i = Form1.ColCount - col; i < Form1.ColCount; i++)
            {
                result.Add(new NodePos
                {
                    X = moveRowNo,
                    Y = i
                });
            }
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = Form1.ColCount - 1
            });
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = Form1.ColCount - 2
            });
            for (int i = Form1.ColCount - 2; i >= Form1.ColCount - col; i--)
            {
                result.Add(new NodePos
                {
                    X = moveRowNo,
                    Y = i
                });
            }
            result.Add(new NodePos
            {
                X = moveRowNo + 1,
                Y = Form1.ColCount - col
            });
            return result;
        }

        public List<NodePos> CalcSpecialRoute2(int moveRowNo, int row = 0, int col = 0)
        {
            var result = new List<NodePos>();
            for (int i = Form1.ColCount - 1; i >= Form1.ColCount - col; i--)
            {
                result.Add(new NodePos
                {
                    X = Form1.RowCount - row,
                    Y = i
                });
            }

            for (int i = Form1.RowCount - row + 1; i <= Form1.RowCount - 1; i++)
            {
                result.Add(new NodePos
                {
                    X = i,
                    Y = Form1.ColCount - col
                });
            }
            result.Add(new NodePos
            {
                X = Form1.RowCount - 1,
                Y = Form1.ColCount - col + 1
            });
            result.Add(new NodePos
            {
                X = Form1.RowCount - 2,
                Y = Form1.ColCount - col + 1
            });
            for (int i = Form1.RowCount - 2; i >= Form1.RowCount - row; i--)
            {
                result.Add(new NodePos
                {
                    X = i,
                    Y = Form1.ColCount - col
                });
            }

            for (int i = Form1.ColCount - col + 1; i < Form1.ColCount; i++)
            {
                result.Add(new NodePos
                {
                    X = Form1.RowCount - row,
                    Y = i
                });
            }
            result.Add(new NodePos
            {
                X = Form1.RowCount - row + 1,
                Y = Form1.ColCount - 1
            });
            return result;
        }

        public List<int> MoveSpace(NodePos nextNode)
        {
            var result = new List<int>();
            var nextNodeInfo = Form1.Map.First(e => e.X == nextNode.X && e.Y == nextNode.Y);
            var spaceNodeInfo = Form1.Map.First(e => e.Text == 0);
            //判断与空白按钮是否相邻，如果是，交换
            var nextButton = Form1._buttons[nextNodeInfo.X, nextNodeInfo.Y];
            var spaceButton = Form1._buttons[spaceNodeInfo.X, spaceNodeInfo.Y];
            if (Form1.IsNeighbor(nextButton, spaceButton))
            {
                if (spaceNodeInfo.X < nextNodeInfo.X)
                {
                    if (!string.IsNullOrEmpty(Form1.OutPutJson.stepList) && Form1.OutPutJson.stepList[^2] == 'd')
                    {
                        var endStr = Form1.OutPutJson.stepList[^1];
                        var endNum = endStr - '0';
                        Form1.OutPutJson.stepList = Form1.OutPutJson.stepList.Remove(Form1.OutPutJson.stepList.Length - 1, 1) + $"{endNum + 1}";
                    }
                    else
                    {
                        Form1.OutPutJson.stepList += "d1";
                    }
                }
                else if (spaceNodeInfo.X > nextNodeInfo.X)
                {
                    if (!string.IsNullOrEmpty(Form1.OutPutJson.stepList) && Form1.OutPutJson.stepList[^2] == 'u')
                    {
                        var endStr = Form1.OutPutJson.stepList[^1];
                        var endNum = endStr - '0';
                        Form1.OutPutJson.stepList = Form1.OutPutJson.stepList.Remove(Form1.OutPutJson.stepList.Length - 1, 1) + $"{endNum + 1}";
                    }
                    else
                    {
                        Form1.OutPutJson.stepList += "u1";
                    }
                }
                else if (spaceNodeInfo.Y > nextNodeInfo.Y)
                {
                    if (!string.IsNullOrEmpty(Form1.OutPutJson.stepList) && Form1.OutPutJson.stepList[^2] == 'l')
                    {
                        var endStr = Form1.OutPutJson.stepList[^1];
                        var endNum = endStr - '0';
                        Form1.OutPutJson.stepList = Form1.OutPutJson.stepList.Remove(Form1.OutPutJson.stepList.Length - 1, 1) + $"{endNum + 1}";
                    }
                    else
                    {
                        Form1.OutPutJson.stepList += "l1";
                    }
                }
                else if (spaceNodeInfo.Y < nextNodeInfo.Y)
                {
                    if (!string.IsNullOrEmpty(Form1.OutPutJson.stepList) && Form1.OutPutJson.stepList[^2] == 'r')
                    {
                        var endStr = Form1.OutPutJson.stepList[^1];
                        var endNum = endStr - '0';
                        Form1.OutPutJson.stepList = Form1.OutPutJson.stepList.Remove(Form1.OutPutJson.stepList.Length - 1, 1) + $"{endNum + 1}";
                    }
                    else
                    {
                        Form1.OutPutJson.stepList += "r1";
                    }
                }
                (spaceNodeInfo.X, nextNodeInfo.X) = (nextNodeInfo.X, spaceNodeInfo.X);
                (spaceNodeInfo.Y, nextNodeInfo.Y) = (nextNodeInfo.Y, spaceNodeInfo.Y);
                result.Add(nextNodeInfo.Text);
            }
            return result;
        }
    }
    public class NodePos
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class NodeInfo
    {
        public int Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        /// <summary>
        /// 固定的，不可移动
        /// </summary>
        public bool Fixed { get; set; }
        /// <summary>
        /// 查询过的
        /// </summary>
        public bool Finded { get; set; }
        public NodeInfo? ParentNode { get; set; }
    }

}
