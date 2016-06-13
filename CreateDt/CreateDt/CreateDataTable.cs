///<summary>
///<para>Copyright (C) 2015 南京理工大学版权所有</para>
/// <para>文 件 名：数据表构建 </para>
/// <para>文件功能：根据获得的数据构建相应的dataTable </para>
/// <para>开发部门：能动学院802教研室 </para>
/// <para>创 建 人：曹纪鹏 </para>
/// <para>电子邮件：malory9@outlook.com </para>
/// <para>创建日期：2016.5.5</para>
/// <para>修 改 人：</para>
/// <para>修改日期：</para>
/// <para>备    注：</para>
/// </summary>

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CreateDt
{
    public abstract class CreateDtClass
    {
        /// <summary>
        /// 根据des所指向的文件数据构造DataTable并返回
        /// </summary>
        /// <param name="des"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string des = "output.txt")
        {
            var dataTable = new DataTable();
            foreach (var readLine in ReadFileHelper.ReadLines(des))
            {
                if (readLine.Contains("title"))
                {
                    foreach (var gettitle in Gettitles(readLine))
                        dataTable.Columns.Add(gettitle, typeof(double));
                }
                else if (!readLine.Contains("[begin_FlightDyn]") && !readLine.Contains("[end_FlightDyn]") &&
                         !readLine.Contains("output"))
                    dataTable.Rows.Add(ConvertToObjs(GetData(readLine)));
            }
            return dataTable;
        }


        /// <summary>
        /// 接受相应的DataTable和 x轴、y轴，返回x轴与y轴构成的点的集合
        /// 返回的点的类型是.NET自带类Tuple
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<double?, double?>> GetPoints(DataTable dt, int x, int y)
        {
            for (var i = 0; i < dt.Rows.Count; ++i)
                yield return new Tuple<double?, double?>(dt.Rows[i][x] as double?, dt.Rows[i][y] as double?);
        }

        private static IEnumerable<string> Gettitles(string line)
        {
            return
                line.Substring("title".Length)                                 //LINQ + Lambda表达式
                    .Split('{', '}', ',')
                    .Where(title => !string.IsNullOrWhiteSpace(title)) //Where接受谓词委托筛选数据，与title => !string.IsNullOrWhiteSpace(title) 等价的函数形式是                                                                                                                                        // string UnnamedFunc(string title){if(!string.IsNullOrWhiteSpace(title))    return title;         }
                    .ToList();                                                                                                                                                                                                              
           
        }

       
        /// <summary>
        /// 将line内的数据分解为double数据集合
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static List<double> GetData(string line)
        {
            return
                line.Split('{', '}', ',', ';')
                    .Where(data => !string.IsNullOrWhiteSpace(data))
                    .Select(data =>
                    {
                        double val;
                        if (!double.TryParse(data, out val))
                        {
                            throw new InvalidCastException();
                        }
                         return val;
                    }).ToList();



        }

        
        protected static object[] ConvertToObjs(List<double> data)
        {
            var objArray = new object[data.Count];
            for (var index = 0; index < data.Count; ++index)
                objArray[index] = data[index];
            return objArray;
        }

       
    }
}