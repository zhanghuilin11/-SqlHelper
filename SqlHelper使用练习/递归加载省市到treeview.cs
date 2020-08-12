using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SqlHelper使用练习
{
    public partial class 递归加载省市到treeview : Form
    {
        public 递归加载省市到treeview()
        {
            InitializeComponent();
        }

        private void 递归加载省市到treeview_Load(object sender, EventArgs e)
        {

        }
        private List<City> LoadData(int belongid,TreeNodeCollection treeNode)
        {// TreeNodeCollection 类用于存储和管理 TreeView 控件中的 TreeNode 对象的集合
            List<City> list = new List<City>();
            string sql = "select * from china_city where belong = @belongId";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@belongId", SqlDbType.Int) { Value = belongid} };
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, parameter);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    City city = new City();
                    city.Id = reader.GetInt32(0);
                    city.City_Name = reader.GetString(1);
                    city.Belong = reader.GetInt32(2);
                    list.Add(city);
                }
            }
            
            
            for (int i = 0; i < list.Count; i++)
            {
                TreeNode node = treeNode.Add(list[i].City_Name);
                
                node.Tag = list[i].Id;
                
                //递归开始
                LoadData(int.Parse(node.Tag.ToString()), node.Nodes);
            }

            return list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            //清空treeview
            LoadData(0,treeView1.Nodes);
            //加载根节点
        }
    }
}
