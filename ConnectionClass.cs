using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace NECT
{
    class ConnectionClass
    {

       
        public static SqlConnection SQLCONNECTION(){



            String str = NECT.Properties.Settings.Default.NECT;

            SqlConnection con = new SqlConnection(str);
            


            return con;
             
           

          

            
        }
        
        public static DataTable Selectcommand(string query) {

            SqlConnection con = SQLCONNECTION();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query,con);
                        try
            {
                con.Open();
                da.SelectCommand.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex) { //System.Windows.Forms.MessageBox.Show(ex.Message); 
                return null; 
            }
            finally { con.Close(); }
        
        
        }

        public static Boolean InsertCommand(string query)
        {

            SqlConnection con = SQLCONNECTION();
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand(query, con);
           try
            {
                con.Open();
                da.InsertCommand.ExecuteNonQuery();
                return true;
               
            }
            catch (Exception ex) { //System.Windows.Forms.MessageBox.Show(ex.Message); 
                return false; }

            finally { con.Close(); }


        }


        public static Boolean DeleteCommand(string query)
        {

            SqlConnection con = SQLCONNECTION();
            SqlDataAdapter da = new SqlDataAdapter();
            da.DeleteCommand = new SqlCommand(query, con);
            try
            {
                con.Open();
                da.DeleteCommand.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex) { //System.Windows.Forms.MessageBox.Show(ex.Message); 
                return false; }

            finally { con.Close(); }


        }
        public static int UpdateCommand(string query)
        {

            SqlConnection con = SQLCONNECTION();
            SqlDataAdapter da = new SqlDataAdapter();
            da.UpdateCommand = new SqlCommand(query, con);
            try
            {
                con.Open();
           int a=  da.UpdateCommand.ExecuteNonQuery();
              //  return true;
                return a;

            }
           catch (Exception ex) { 
            //   System.Windows.Forms.MessageBox.Show(ex.Message); 
               return 0; }

            finally { con.Close(); }


        }


        public static bool InsertImage(string query, byte[] imageData)
        {
            using (SqlConnection con = SQLCONNECTION())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@ImageData", SqlDbType.VarBinary).Value = imageData;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return false;
                }
            }
        }

        public static DataTable proc_Select(string name)
        {

            SqlConnection con = SQLCONNECTION();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(name,con);

            cmd.CommandType = CommandType.StoredProcedure;
           
            cmd.Connection = con;
           
  
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); return null; }
            finally { con.Close(); }


        }

        public static DataTable proc_Select(string name, Dictionary<string, string> param)
        {

            SqlConnection con = SQLCONNECTION();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(name,con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            foreach (KeyValuePair<string, string>  pair in param )
            {
                cmd.Parameters.Add(new SqlParameter("@"+pair.Key, pair.Value));
            }


            try
            {
                con.Open();
                
                DataTable dt = new DataTable();
               dt.Load(cmd.ExecuteReader());
               
                

                return dt;
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); return null; }
            finally { con.Close(); }


        }
        public static string proc_Insert(string name, Dictionary<string, string> param)
        {
            string ResponseMessage = string.Empty;
            SqlConnection con = SQLCONNECTION();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(name, con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            foreach (KeyValuePair<string, string> pair in param)
            {
                cmd.Parameters.Add(new SqlParameter("@" + pair.Key, pair.Value));
            }

            cmd.Parameters.Add(new SqlParameter("@Response",SqlDbType.VarChar,200));
            cmd.Parameters["@Response"].Direction = ParameterDirection.Output;

            try
            {
               
                
               
                //   try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ResponseMessage = cmd.Parameters["@Response"].Value.ToString();
                    return ResponseMessage;

                }
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); return "Error"; }
            finally { con.Close(); }


        }

        internal static DataTable SelectCommand(string p)
        {
            throw new NotImplementedException();
        }
    }
}
