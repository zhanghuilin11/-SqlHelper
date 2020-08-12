using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace SqlHelper使用练习
{
    public static class SqlHelper
    {
        //readonly 修饰的变量，只能在初始化的时候赋值，以及在构造函数中赋值，其他地方只能读取不能设置
        private static readonly string cnstr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        
        //执行update,delete,insert ExecuteNonQuery()
        public static int ExecuteNonQuery(string sql,params SqlParameter[] pms)
        {
            using (SqlConnection cnn = new SqlConnection(cnstr))
            {
                using (SqlCommand cmd = new SqlCommand(sql,cnn))
                {
                    cnn.Open();
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    return cmd.ExecuteNonQuery();

                }


            }
        }

        //执行查询单个值  ExecuteScalar()
        public static object ExecuteScalar(string sql, params SqlParameter[] pms)
        {
            
            using (SqlConnection cnn = new SqlConnection(cnstr))
            {
                using (SqlCommand cmd = new SqlCommand(sql ,cnn))
                {
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    cnn.Open();
                    return cmd.ExecuteScalar();

                }

            }
        }


        //执行查询返回多行多列  ExecuteReader()

        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] pms)
        {
            
            SqlConnection cnn = new SqlConnection(cnstr);
            //这里不用using是因为reader在使用过程中cnn必须保持连接
            using (SqlCommand cmd = new SqlCommand(sql ,cnn))
            {
                if (pms != null)
                {
                    cmd.Parameters.AddRange(pms);
                }

                try
                {
                    cnn.Open();
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    //System.Data.CommandBehavior.CloseConnection这个枚举表示将来使用完毕sqldatareader后，在关闭reader的同时，在sqldatareader内部
                    //会将关联的connection对象也关闭掉
                }
                catch 
                {
                    //如果sql语句有问题，这里可以将cnn简介关闭和释放，避免一直占用
                    cnn.Close();
                    cnn.Dispose();
                    throw;
                }
            }

        }
    }
}
