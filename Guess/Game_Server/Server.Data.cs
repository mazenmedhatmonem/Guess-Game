using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Game_Server
{
	partial class Server
	{
		string CategoryString;
        string GuessName;
		DataTable CategoryTable;
		string ConnectionString = @"server=.;database=cSharp;Integrated Security=true";
		private void Category()
		{
			string sql = @"select * from Categories ";
			SqlConnection connection = new SqlConnection(ConnectionString);
			SqlDataAdapter da = new SqlDataAdapter(sql, connection);
			CategoryTable = new DataTable();
			connection.Open();
			da.Fill(CategoryTable);

			connection.Close();

			List<string> categ_list = new List<string>();
			foreach (DataRow s in CategoryTable.Rows)
			{
				categ_list.Add(s[1].ToString());
			}
			CategoryString = categ_list.Serialize();
		}

        private string SelectWord(string Level, string category)
        {
            int Type_ID;
            int Level_ID = 1;
            var cat= CategoryTable.AsEnumerable();
            Type_ID = (from c in cat
                       where c[1].ToString() == category
                       select (int)c[0]).First();

            switch (Level)
            {
                case "Easy":
                    Level_ID = 1;
                    break;
                case "Medium":
                    Level_ID = 2;
                    break;
                case "Hard":
                    Level_ID = 3;
                    break;

            }
            string sql = @"select top 1 name from guesses where Dif_level= " + Level_ID + " and Categ_type = " + Type_ID + " order by NEWID() ";

            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            connection.Open();
            da.Fill(ds, "guesses");
            GuessName = ds.Tables["guesses"].Rows[0][0].ToString();
            connection.Close();
            return GuessName;
        }

    }
}
