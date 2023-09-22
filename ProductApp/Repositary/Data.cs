using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProductAdd.Data;
using ProductAdd.Models;
using System.Data;

namespace ProductAdd.Repositary
{
    public class Data : IData
    {
        public string ConnectionString { get; set; }
        public Data(AppDbContext db)
        {
            ConnectionString = db.Database.GetConnectionString();
        }
        
        public Product SaveProductDetails(Product details)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spt_saveProductDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName", details.ProductName);
                cmd.Parameters.AddWithValue("@SKU", details.SKU);
                cmd.Parameters.AddWithValue("@RetailPrice", details.RetailPrice);
                cmd.Parameters.AddWithValue("@SalePrice", details.SalePrice);
                cmd.Parameters.AddWithValue("@LowestPrice", details.LowestPrice);
                cmd.Parameters.AddWithValue("@Status", details.Status);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);
                SqlParameter outputparm = new SqlParameter();
                outputparm.DbType = DbType.Int32;
                outputparm.Direction = ParameterDirection.Output;
                outputparm.ParameterName = "@Id";
                cmd.Parameters.Add(outputparm);
                cmd.ExecuteNonQuery();
                int id = int.Parse(outputparm.Value.ToString());
                details.Id = id;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return details;
        }

        public List<Product> GetAllProductDetails()
        {

            List<Product> list = new List<Product>();
            using  SqlConnection con = new SqlConnection(ConnectionString);
            Product details;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spt_getAllProductDetails", con);
                SqlDataReader reader = cmd.ExecuteReader();
                cmd.CommandType = CommandType.StoredProcedure;
                while (reader.Read())
                {
                    details = new Product();
                    details.Id = int.Parse(reader["id"].ToString());
                    details.ProductName = reader["ProductName"].ToString();
                    details.SKU = reader["SKU"].ToString();
                    details.RetailPrice = int.Parse(reader["RetailPrice"].ToString());
                    details.SalePrice = int.Parse(reader["SalePrice"].ToString());
                    details.LowestPrice = int.Parse(reader["LowestPrice"].ToString());
                    details.Status = int.Parse(reader["Status"].ToString());
                   // details.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate").ToString("dd MM yyyy"));
                    list.Add(details);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return list;
        }
    }
}
