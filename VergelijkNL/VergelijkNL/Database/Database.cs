using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VergelijkNL.Database
{
    public class Database
    {
        private OracleConnection con;

        //methodes
        private void Connect()
        {
            con = new OracleConnection();
            con.ConnectionString = "Data Source=localhost/xe;Persist Security Info=True;User ID=system;Password=xxxxxx";

            con.Open();
        }

        private void Disconnect()
        {
            con.Close();
            con.Dispose();
        }

        protected int doQuery(string query)
        {
            try
            {
                Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                OracleTransaction transact = cmd.Connection.BeginTransaction();
                cmd.Transaction = transact;
                int ret = cmd.ExecuteNonQuery();
                transact.Commit();
                Disconnect();
                return ret;

            }
            catch (Exception e) { Console.WriteLine(e.ToString()); return -1; }
            finally { Disconnect(); }
        }

        protected List<Dictionary<string, object>> getQuery(string query)
        {
            List<Dictionary<string, object>> ret = new List<Dictionary<string, object>>();

            try
            {
                Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;

                OracleDataReader data = cmd.ExecuteReader();


                while (data.Read())
                {
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    for (int c = 0; c < data.FieldCount; c++)
                        d.Add(data.GetName(c).ToLower(), data.GetValue(c));


                    ret.Add(d);
                }

                Disconnect();
                return ret;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return new List<Dictionary<string, object>>();
            }
            finally { Disconnect(); }
        }

        public int getLatestId(String table)
        {
            List<Dictionary<string, object>> data = getQuery("SELECT MAX(Id) + 1 AS ID FROM " + table);

            if (data == null)
                return 0;

            if (data.Count > 0)
                return Convert.ToInt16(data[0]["id"]);
            return -1;
        }

        public string strip(string input)
        {
            if (input == null)
                return null;

            input = input.Replace("'", "");
            input = input.Replace("\\", "");

            return input;
        }
    }
}