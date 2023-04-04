using Dapper;
using SkateFactory2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateFactory2.Data
{
    public static class UserData
    {
        public static bool UserAndPasswordAreValid(User user, string cnString)
        {
            bool result = false;
            using (var cn = new SqlConnection(cnString))
            {
                result = UserAndPasswordAreValid(user, cn);
            }
            return result;
        }

        public static bool UserAndPasswordAreValid(User user, SqlConnection cn, SqlTransaction transaction = null)
        {
            bool result = false;
            using (var cmd = new SqlCommand("SELECT * FROM Users WHERE Email=@Email AND Password=@Password", cn, transaction))
            {
                cmd.Parameters.Add(new SqlParameter("Email", System.Data.SqlDbType.VarChar, 50) { Value = user.Email });
                cmd.Parameters.Add(new SqlParameter("Password", System.Data.SqlDbType.VarChar, 50) { Value = user.Password });
                if (cn.State != System.Data.ConnectionState.Open)
                    cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    result = dr.HasRows;
                }
            }
            return result;
        }

        public static bool UserIsUnique(string email, string cnString)
        {
            bool result = false;
            using (var cn = new SqlConnection(cnString))
            {
                result = UserIsUnique(email, cn);
            }
            return result;
        }

        public static bool UserIsUnique(string email, SqlConnection cn, SqlTransaction transaction = null)
        {
            bool result = false;
            using (var cmd = new SqlCommand("SELECT COUNT(Email) FROM Users WHERE Email=@Email", cn, transaction))
            {
                cmd.Parameters.Add(new SqlParameter("Email", System.Data.SqlDbType.VarChar, 50) { Value = email });
                cn.Open();
                var count = (int)cmd.ExecuteScalar();
                result = count == 0;
            }
            return result;
        }

        public static void Insert(User user, string cnString)
        {
            using (var cn = new SqlConnection(cnString))
            {
                Insert(user, cn);
            }
        }

        public static void Insert(User user, SqlConnection cn, SqlTransaction transaction = null)
        {
            using (var cmd = new SqlCommand("INSERT INTO Users (Email, Password) VALUES (@Email, @Password)", cn, transaction))
            {
                cmd.Parameters.Add(new SqlParameter("Email", System.Data.SqlDbType.VarChar, 50) { Value = user.Email.Trim() });
                cmd.Parameters.Add(new SqlParameter("Password", System.Data.SqlDbType.VarChar, 50) { Value = user.Password });
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
