using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace codebreakerDay1
{
    public class DB
    {
        const string connString = "server=localhost;database=dayone;uid=USERNAME;password=PASSWORD;";
        public string _sql { get; set; }
        public string _parameters { get; set; }
        public string _values { get; set; }



        public DB(string sql, string parameters = "", string values = "")
        {
            _sql = sql;
            _parameters = parameters;
            _values = values;
        }

        public DataSet GetDataSet()
        {
            SqlConnection sqlCon = new SqlConnection(connString);
            DataSet DS = new DataSet();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            string[] splitParams = _parameters.Split(',');
            string[] splitValues = _values.Split(',');

            sqlCon.Open();

            SqlCommand sqlCMD = new SqlCommand(_sql, sqlCon);
            sqlCMD.CommandText = _sql;
            sqlAdapter.SelectCommand = sqlCMD;

            if (splitParams.Count() != 0)
            {
                for (int i = 0; i < splitParams.Count(); i++)
                {
                    sqlCMD.Parameters.AddWithValue(splitParams[i].ToString(), splitValues[i].ToString());
                }
            }
            sqlAdapter.Fill(DS);
            sqlCon.Close();
            return DS;
        }

    }
}