///<summary>
///<para>Copyright (C) 2015 南京理工大学版权所有</para>
/// <para>文 件 名：画图控件 </para>
/// <para>文件功能：根据获得的数据做出相应的方程图片（可选择相应x、y轴单位做出多张图片）以及保存图片 </para>
/// <para>开发部门：能动学院802教研室 </para>
/// <para>创 建 人：曹纪鹏 </para>
/// <para>电子邮件：malory9@outlook.com </para>
/// <para>创建日期：2016.5.29</para>
/// <para>修 改 人：</para>
/// <para>修改日期：</para>
/// <para>备    注：x、y轴已解决，问题有：y轴单位显示是垂直的，这对用户有点不友好</para>
/// </summary>

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CreateDt;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;

namespace DrawMltPicControl
{
    public partial class DrawMltiPicControl : UserControl
    {
        /// <summary>
        ///     勾选的需要作图的标题对的列表，Tuple是内置泛型类型，此处则相当于(x,y),存储的是x，y在_dt中对应title的下标
        /// </summary>
        private static readonly List<Tuple<int, int>> DisplayList = new List<Tuple<int, int>>();


        /// <summary>
        ///     垂直显示图标时的每个控件所占空间大小，手动实现，数值是由12*12的方格划分计算的
        /// </summary>
        private static readonly Dictionary<int, List<Tuple<int, int>>> SplitSeriesDictVerti =
            #region SplitSeriesDictVerti初始化

            new Dictionary<int, List<Tuple<int, int>>>
            {
                {1, new List<Tuple<int, int>> {SeriesFact(12, 12)}},
                {2, new List<Tuple<int, int>> {SeriesFact(6, 12), SeriesFact(6, 12)}},
                {3, new List<Tuple<int, int>> {SeriesFact(6, 6), SeriesFact(6, 6), SeriesFact(6, 6)}},
                {4, new List<Tuple<int, int>> {SeriesFact(6, 6), SeriesFact(6, 6), SeriesFact(6, 6), SeriesFact(6, 6)}},
                {
                    5,
                    new List<Tuple<int, int>>
                    {
                        SeriesFact(6, 4),
                        SeriesFact(6, 4),
                        SeriesFact(6, 4),
                        SeriesFact(6, 4),
                        SeriesFact(6, 4)
                    }
                },
                {
                    6,
                    new List<Tuple<int, int>>
                    {
                        SeriesFact(6, 4),
                        SeriesFact(6, 4),
                        SeriesFact(6, 4),
                        SeriesFact(6, 4),
                        SeriesFact(6, 4),
                        SeriesFact(6, 4)
                    }
                },
                {
                    7,
                      new List<Tuple<int, int>>
                    {
                        SeriesFact(4, 4),
                    SeriesFact(4, 4),
                      SeriesFact(4, 4),
                     SeriesFact(4, 4),
                        SeriesFact(4, 4),
                       SeriesFact(4, 4),SeriesFact(4, 4),
                    }
                },
                {
                    8,
                    new List<Tuple<int, int>>
                    {
                        SeriesFact(4, 4),
                 SeriesFact(4, 4),
                        SeriesFact(4, 4),
                     SeriesFact(4, 4),
                   SeriesFact(4, 4),
                   SeriesFact(4, 4),
                       
                       SeriesFact(4, 4),
                        SeriesFact(4, 4),}
                },
                {
                    9,
                    new List<Tuple<int, int>>
                    {
                        SeriesFact(4, 4),
                 SeriesFact(4, 4),
                        SeriesFact(4, 4),
                     SeriesFact(4, 4),
                   SeriesFact(4, 4),
                   SeriesFact(4, 4),
                       SeriesFact(4, 4),
                       SeriesFact(4, 4),
                        SeriesFact(4, 4),}
                }
            };
            #endregion

        /// <summary>
        ///     水平显示图标时的每个控件所占空间大小，手动实现，同上
        /// </summary>
        private static readonly Dictionary<int, List<Tuple<int, int>>> SplitSeriesDictHor = 
            #region SplitSeriesDictHor初始化

            new Dictionary<int, List<Tuple<int, int>>>
            {
                {1, new List<Tuple<int, int>> {SeriesFact(12, 12)}},
                {2, new List<Tuple<int, int>> {SeriesFact(12,6), SeriesFact(12, 6)}},
                {3, new List<Tuple<int, int>> {SeriesFact(6, 6), SeriesFact(6, 6), SeriesFact(6, 6)}},
                {4, new List<Tuple<int, int>> {SeriesFact(6, 6), SeriesFact(6, 6), SeriesFact(6, 6), SeriesFact(6, 6)}},
                {
                    5,
                    new List<Tuple<int, int>>
                    {
                        SeriesFact(4,6),
                      SeriesFact(4,6),
                       SeriesFact(4,6),
                      SeriesFact(4,6),
                     SeriesFact(4,6),
                    }},
                {
                    6,
                    new List<Tuple<int, int>>
                    {
                        SeriesFact(4, 6),
                   SeriesFact(4, 6),
                    SeriesFact(4, 6),
                   SeriesFact(4, 6),
                   SeriesFact(4, 6),
                       SeriesFact(4, 6),
                    }
                },
                {
                    7,
                    new List<Tuple<int, int>>
                    {
                        SeriesFact(4, 4),
                    SeriesFact(4, 4),
                      SeriesFact(4, 4),
                     SeriesFact(4, 4),
                        SeriesFact(4, 4),
                       SeriesFact(4, 4),SeriesFact(4, 4),
                    }
                },
                {
                    8,
                    new List<Tuple<int, int>>
                    {
                    SeriesFact(4, 4),
                        SeriesFact(4, 4),
                     SeriesFact(4, 4),
                   SeriesFact(4, 4),
                   SeriesFact(4, 4),
                       SeriesFact(4, 4),
                       SeriesFact(4, 4),
                        SeriesFact(4, 4),}
                },
                {
                    9,
                    new List<Tuple<int, int>>
                    {
                        SeriesFact(4, 4),
                 SeriesFact(4, 4),
                        SeriesFact(4, 4),
                     SeriesFact(4, 4),
                   SeriesFact(4, 4),
                   SeriesFact(4, 4),
                       SeriesFact(4, 4),
                       SeriesFact(4, 4),
                        SeriesFact(4, 4),}
                }
            
            };

        #endregion

        /// <summary>
        ///     图表集合，为了避免多次创建与删除临时图表
        /// </summary>
        private static readonly List<ChartControl> ChartList

            #region ChartList初始化
            = new List<ChartControl>
            {
                new ChartControl(),
                new ChartControl(),
                new ChartControl(),
                new ChartControl(),
                new ChartControl(),
                new ChartControl(),
                new ChartControl(),
                new ChartControl(),
                new ChartControl()
            };

        #endregion

        /// <summary>
        ///     CreateDt类来自我提前编写的类库CreateDataTable ，GetDataTable 返回其形参构造的datatable
        /// </summary>
        private static DataTable _dt;


        public DrawMltiPicControl()
        {
            InitializeComponent();
        }


        /// <summary>
        ///     创建序对的简便方法，仅仅为了省空间和时间
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static Tuple<int, int> SeriesFact(int x, int y)
        {
            return new Tuple<int, int>(x, y);
        }

        /// <summary>
        ///     设置output文件的位置
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadDt(string fileName)
        {
            try
            {
                _dt = CreateDtClass.GetDataTable(fileName); //测试时注意output文件的位置
                LoadTitles();
            }

            catch (InvalidCastException)
            {
                MessageBox.Show(@"计算数据有错误");
            }
        }

        /// <summary>
        ///     将已关闭的dockpannel重新显示
        /// </summary>
        public void ShowDockPannel()
        {
            optionDockPanel.Show();
        }


        /// <summary>
        ///     给左上角的2个列表添加标题
        /// </summary>
        private void LoadTitles()
        {
            for (var i = 0; i < _dt.Columns.Count; i++)
            {
                var item1 = new CheckedListBoxItem(_dt.Columns[i]);
                var item2 = new CheckedListBoxItem(_dt.Columns[i]);
                xAxisCheckedListBoxControl.Items.Add(item1);
                yAxisCheckedListBoxControl.Items.Add(item2);
            }
        }

        /// <summary>
        ///     将选择的x轴、y轴对应曲线在chartControl中作图
        /// </summary>
        /// <param name="chartControl"></param>
        /// <param name="x">对应x轴</param>
        /// <param name="y">对应y轴</param>
        private static void DrawPic(ref ChartControl chartControl, int x, int y)
        {
            var series = new Series
            {
                View = new LineSeriesView()
            };

            //作图的风格，此处选择为线
            for (var i = 0; i < _dt.Rows.Count; ++i)
                series.Points.Add(new SeriesPoint(_dt.Rows[i][x], _dt.Rows[i][y]));
            chartControl.Series.Add(series);


            var xyDiagram = (XYDiagram) chartControl.Diagram; //设置x、y轴单位
            xyDiagram.AxisX.Title.Text = _dt.Columns[x].ColumnName;
            xyDiagram.AxisX.Title.Font = new Font("Times New Roman", 11F);
            xyDiagram.AxisX.Title.Visible = true;
            xyDiagram.AxisX.Title.Alignment = StringAlignment.Center;
            xyDiagram.AxisY.Title.Text = _dt.Columns[y].ColumnName;
            xyDiagram.AxisY.Title.Font = new Font("Times New Roman", 11F);
            xyDiagram.AxisY.Title.Visible = true;
            xyDiagram.AxisY.Title.Alignment = StringAlignment.Center;
        }

        /// <summary>
        ///     统一作图函数，给每个分图表分配空间及空间块的相关设置
        /// </summary>
        /// <param name="count">当前所需作图的序对数量</param>
        private void DrawPicSum(int count)
        {
            showChartTableLayoutPanel.Controls.Clear();
            for (var i = 0; i < count; ++i)
            {
                var chart = ChartList[i];
                chart.Dock = DockStyle.Fill;

                showChartTableLayoutPanel.Controls.Add(chart);


                showChartTableLayoutPanel.SetRowSpan(chart, SplitSeriesDictVerti[count][i].Item1); //设置每个图表分配的空间
                showChartTableLayoutPanel.SetColumnSpan(chart, SplitSeriesDictVerti[count][i].Item2);

                chart.Series.Clear();
                DrawPic(ref chart, DisplayList[i].Item1, DisplayList[i].Item2);
            }
            ;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            //若找到单选的属性则此处if判断可删除
            if (xAxisCheckedListBoxControl.CheckedItemsCount != 1 || yAxisCheckedListBoxControl.CheckedItemsCount != 1)
            {
                MessageBox.Show(@"勾选的数目不正确，请检查");
            }
            else
            {
                var item1 = xAxisCheckedListBoxControl.CheckedItems[0]; //item1和item2分别对应所勾选的标题
                var item2 = yAxisCheckedListBoxControl.CheckedItems[0];
                var item1Index = xAxisCheckedListBoxControl.SelectedIndex;
                var item2Index = yAxisCheckedListBoxControl.SelectedIndex;

                DisplayList.Add(new Tuple<int, int>(item1Index, item2Index));
                for (var i = 0; i < xAxisCheckedListBoxControl.ItemCount; ++i)
                {
                    xAxisCheckedListBoxControl.SetItemCheckState(i, 0); //添加完成后将勾选的勾去掉
                    yAxisCheckedListBoxControl.SetItemCheckState(i, 0);
                }
                var line = string.Format("x轴：{0}--- y轴: {1}", item1, item2);
                selectedPairListBoxControl.Items.Add(line);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (selectedPairListBoxControl.SelectedItem == null) return;
            DisplayList.RemoveAt(selectedPairListBoxControl.SelectedIndex);
            selectedPairListBoxControl.Items.RemoveAt(selectedPairListBoxControl.SelectedIndex);
        }


        private void drawButton_Click(object sender, EventArgs e)
        {
            if (DisplayList.Count > 9) MessageBox.Show(@"当前所选序对数过多");
            else
            {
                DrawPicSum(DisplayList.Count);
            }
        }

        private void toVertiButton_Click(object sender, EventArgs e)
        {
            var curCount = showChartTableLayoutPanel.Controls.Count;
            for (var i = 0; i < curCount; ++i)
            {
                showChartTableLayoutPanel.SetRowSpan(showChartTableLayoutPanel.Controls[i],
                    SplitSeriesDictVerti[curCount][i].Item1);
                showChartTableLayoutPanel.SetColumnSpan(showChartTableLayoutPanel.Controls[i],
                    SplitSeriesDictVerti[curCount][i].Item2);
            }
        }

        private void toHorButton_Click(object sender, EventArgs e)
        {
            var curCount = showChartTableLayoutPanel.Controls.Count;
            for (var i = 0; i < curCount; ++i)
            {
                showChartTableLayoutPanel.SetRowSpan(showChartTableLayoutPanel.Controls[i],
                    SplitSeriesDictHor[curCount][i].Item1);
                showChartTableLayoutPanel.SetColumnSpan(showChartTableLayoutPanel.Controls[i],
                    SplitSeriesDictHor[curCount][i].Item2);
            }
        }

        /// <summary>
        ///     导出图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportButton_Click(object sender, EventArgs e)
        {
            var w = showChartTableLayoutPanel.Width;
            var h = showChartTableLayoutPanel.Height;

            var map = new Bitmap(w, h);
            showChartTableLayoutPanel.DrawToBitmap(map,
                new Rectangle(0, 0, showChartTableLayoutPanel.Width, showChartTableLayoutPanel.Height));

            var save = new SaveFileDialog
            {
                FileName = "outputPic",
                Title = @"保存图片",
                Filter =
                    @"jpg文件(*.jpg)|*.jpg|jpeg文件(*.jpeg)|*.jpeg|bmp文件(*.bmp)|*.bmp|gif文件(*.gif)|*.gif|ico文件(*.ico)|*.ico|png文件(*.png)|*.png"
            };

            if (save.ShowDialog() != DialogResult.OK) return;
            var dest = save.FileName;
            map.Save(dest);
        }
    }
}