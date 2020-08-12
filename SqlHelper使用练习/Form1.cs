using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SqlHelper使用练习
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Dictionary<string, int> citys = new Dictionary<string, int>();
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbProvince.Items.Clear();
            string sql = "select id,city_name from china_city where belong = @belong";
            SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@belong",SqlDbType.Int){ Value = 0}
            };
            SqlDataReader reader =  SqlHelper.ExecuteReader(sql,sqlParameter);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cmbProvince.Items.Add(reader.GetString(1));
                    //citys.Add(reader.GetString(1),reader.GetInt32(0));
                    citys[reader.GetString(1)] = reader.GetInt32(0);
                }
            }
            
        }

        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCity.Items.Clear();
            string sql = "select id,city_name from china_city where belong = @belong";
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@belong",SqlDbType.Int){ Value = citys[cmbProvince.SelectedItem.ToString()]}
            };
            SqlDataReader reader = SqlHelper.ExecuteReader(sql,parameter);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cmbCity.Items.Add(reader.GetString(1));
                    //citys.Add(reader.GetString(1),reader.GetInt32(0));
                    citys[reader.GetString(1)] = reader.GetInt32(0);
                }
            }
        }
    }
}
