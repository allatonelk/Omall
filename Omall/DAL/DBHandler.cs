using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBHandler
    {
        private string ConnnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public SqlConnection sqlConnection;
        

        public int ExecuteStoredProcedure(string storedProcedureName, List<object[]> mPara)
        {

            try
            {
                using (sqlConnection = new SqlConnection(ConnnectionString))
                {


                    SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (mPara != null)
                    {
                        foreach (object[] parameters in mPara)
                        {
                            sqlCommand.Parameters.AddWithValue(parameters[0].ToString(), parameters[1]);
                        }
                    }

                    return sqlCommand.ExecuteNonQuery();

                    
                    

                }
            }
            catch (Exception)
            {

                throw;
            }
                   
               
        }

        public DataTable GetDataSet(string storedProcedureName, List<object[]> mPara)
        {

            try
            {
                using (sqlConnection = new SqlConnection(ConnnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (mPara != null)
                    {
                        foreach (object[] parameters in mPara)
                        {
                            sqlCommand.Parameters.AddWithValue(parameters[0].ToString(), parameters[1]);
                        }  
                    }

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        return dt;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int ExecuteSqlReturnInt(string storedProcedureName, List<object[]> mPara)
        {

            try
            {
                using (sqlConnection = new SqlConnection(ConnnectionString))
                {


                    SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (mPara != null)
                    {
                        foreach (object[] parameters in mPara)
                        {
                            sqlCommand.Parameters.AddWithValue(parameters[0].ToString(), parameters[1]);
                        }
                    }

                    SqlParameter returnParameter = sqlCommand.Parameters.Add("@outvalue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    sqlCommand.ExecuteNonQuery();

                    return int.Parse(returnParameter.Value.ToString());


                }
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
