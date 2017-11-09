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

        public List<T> GetDataSet<T>(string storedProcedureName, List<object[]> mPara)
        {

            try
            {
                using (sqlConnection = new SqlConnection(ConnnectionString))
                {
                    sqlConnection.Open();
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
                        return BindList<T> (dt);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        public static List<T> BindList<T>(DataTable dt)
        {
            // Example 1:
            // Get private fields + non properties
            //var fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            // Example 2: Your case
            // Get all public fields
            var fields = typeof(T).GetProperties();

            List<T> lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                // Create the object of T
                var ob = Activator.CreateInstance<T>();

                foreach (var fieldInfo in fields)
                {
                   // foreach (DataColumn dc in dt.Columns)
                    {
                        // Matching the columns with fields
                       // if (fieldInfo.Name == dc.ColumnName)
                        {
                            Type type = fieldInfo.PropertyType;

                            // Get the value from the datatable cell
                            object value = (dr[fieldInfo.Name] == DBNull.Value ? null : dr[fieldInfo.Name]);

                            // Set the value into the object
                            fieldInfo.SetValue(ob, value);
                           // break;
                        }
                    }
                }

                lst.Add(ob);
            }

            return lst;
        }

        //static object GetValue(object ob, Type targetType)
        //{
        //    if (targetType == null)
        //    {
        //        return null;
        //    }
        //    else if (targetType == typeof(String))
        //    {
        //        return ob + "";
        //    }
        //    else if (targetType == typeof(int))
        //    {
        //        int i = 0;
        //        int.TryParse(ob + "", out i);
        //        return i;
        //    }
        //    else if (targetType == typeof(short))
        //    {
        //        short i = 0;
        //        short.TryParse(ob + "", out i);
        //        return i;
        //    }
        //    else if (targetType == typeof(long))
        //    {
        //        long i = 0;
        //        long.TryParse(ob + "", out i);
        //        return i;
        //    }
        //    else if (targetType == typeof(ushort))
        //    {
        //        ushort i = 0;
        //        ushort.TryParse(ob + "", out i);
        //        return i;
        //    }
        //    else if (targetType == typeof(uint))
        //    {
        //        uint i = 0;
        //        uint.TryParse(ob + "", out i);
        //        return i;
        //    }
        //    else if (targetType == typeof(ulong))
        //    {
        //        ulong i = 0;
        //        ulong.TryParse(ob + "", out i);
        //        return i;
        //    }
        //    else if (targetType == typeof(double))
        //    {
        //        double i = 0;
        //        double.TryParse(ob + "", out i);
        //        return i;
        //    }
        //    else if (targetType == typeof(DateTime))
        //    {
        //        // do the parsing here...
        //    }
        //    else if (targetType == typeof(bool))
        //    {
        //        // do the parsing here...
        //    }
        //    else if (targetType == typeof(decimal))
        //    {
        //        // do the parsing here...
        //    }
        //    else if (targetType == typeof(float))
        //    {
        //        // do the parsing here...
        //    }
        //    else if (targetType == typeof(byte))
        //    {
        //        // do the parsing here...
        //    }
        //    else if (targetType == typeof(sbyte))
        //    {
        //        // do the parsing here...
        //    }
           
        
        //    return ob;
        //}


    }
}
