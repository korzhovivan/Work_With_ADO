using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WindowsFormsApp1
{
    public class EasySQL
    {

        private SqlCommand SQL_command = null;
        private SqlConnection SQL_conection = null;
        private SqlDataReader SQL_reader = null;
        DataTable SQL_table = null;
        public EasySQL()
        {

        }
        public void Connect()
        {
            SQL_conection = new SqlConnection();
            SQL_conection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        }
        public void Command_NonQuery(string commandText)
        {
            SQL_conection.Open();
            SQL_command = new SqlCommand();
            SQL_command.CommandText = commandText;
            SQL_command.Connection = SQL_conection;
            SQL_command.ExecuteNonQuery();
            SQL_conection.Close();
        }
        public DataTable GetTable(string TableName)
        {
            SQL_command = new SqlCommand();
            SQL_command.CommandText = "select * from " + TableName;
            SQL_command.Connection = SQL_conection;
            SQL_conection.Open();

            SQL_table = new DataTable();

            SQL_reader = SQL_command.ExecuteReader();

            int line = 0;
            do
            {
                while (SQL_reader.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < SQL_reader.FieldCount; i++)
                        {
                            SQL_table.Columns.Add(SQL_reader.GetName(i));
                        }
                        line++;
                    }
                    DataRow row = SQL_table.NewRow();
                    for (int i = 0; i < SQL_reader.FieldCount; i++)
                    {
                        row[i] = SQL_reader[i];
                    }
                    SQL_table.Rows.Add(row);
                }

            } while (SQL_reader.NextResult());

            
            SQL_conection.Close();
            SQL_reader.Close();

            return SQL_table;
        }

    }
}
