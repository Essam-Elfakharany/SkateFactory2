using Dapper;
using SkateFactory2.Models;
using SkateFactory2.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SkateFactory2.Data
{
    public static class SkateboardData
    {
        public static Skateboard SearchById(int skateboardId, string cnString)
        {
            Skateboard result = null;
            //var parameters = new DynamicParameters();
            //parameters.Add("@Id", skateboardId, System.Data.DbType.Int32);
            using (var cn = new SqlConnection(cnString))
            {
                result = cn.Query<Skateboard>(
                    sql: "SELECT * FROM Skateboards WHERE Id=@Id",
                    param: new { Id = skateboardId }
                    //param: parameters
                ).FirstOrDefault();
            }
            return result;
        }

        public static List<Skateboard> GetList(ESkateboardCriteria criteria, string cnString)
        {
            var result = new List<Skateboard>();
            using (var cn = new SqlConnection(cnString))
            {
                string query;
                if (criteria == ESkateboardCriteria.All)
                    query = "SELECT * FROM Skateboards";
                else if (criteria == ESkateboardCriteria.Electric)
                    query = "SELECT * FROM Skateboards WHERE SkateType=1";
                else
                    query = "SELECT * FROM Skateboards WHERE SkateType=2";

                result = cn.Query<Skateboard>(query).ToList();
            }
            return result;
        }

        public static void Insert(Skateboard skate, string cnString)
        {
            using (var cn = new SqlConnection(cnString))
            {
                //var parameters = new DynamicParameters();
                //parameters.Add("@Name", skate.Name, System.Data.DbType.String, size: 50);
                //parameters.Add("@Weight", skate.Weight, System.Data.DbType.Single);
                //parameters.Add("@Color", skate.Color, System.Data.DbType.Int32);
                //parameters.Add("@SkateType", skate.SkateType, System.Data.DbType.Int32);
                //parameters.Add("@ShapeType", skate.ShapeType, System.Data.DbType.Int32);
                //parameters.Add("@BrakeType", skate.BrakeType, System.Data.DbType.Int32);

                cn.Execute(
                    sql: "INSERT INTO Skateboards (Name, Color, Weight, SkateType, ShapeType, BrakeType) VALUES (@Name, @Color, @Weight, @SkateType, @ShapeType, @BrakeType)",
                    //param: parameters
                    param: new 
                    { 
                        Name = skate.Name,
                        Weight = skate.Weight,
                        Color = skate.Color,
                        SkateType = skate.SkateType,
                        ShapeType = skate.ShapeType,
                        BrakeType = skate.BrakeType
                    }
                );
            }
        }

        public static void Update(Skateboard skate, string cnString)
        {
            using (var cn = new SqlConnection(cnString))
            {
                cn.Execute(
                    sql: "UPDATE Skateboards SET Name=@Name, Color=@Color, Weight=@Weight, SkateType=@SkateType, ShapeType=@ShapeType, BrakeType=@BrakeType WHERE Id=@Id",
                    //param: parameters
                    param: new
                    {
                        Id = skate.Id,
                        Name = skate.Name,
                        Weight = skate.Weight,
                        Color = skate.Color,
                        SkateType = skate.SkateType,
                        ShapeType = skate.ShapeType,
                        BrakeType = skate.BrakeType
                    }
                );
            }
        }

        public static void Delete(int skateboardId, string cnString)
        {
            using (var cn = new SqlConnection(cnString))
            {
                cn.Execute(
                    sql: "DELETE Skateboards WHERE Id=@Id",
                    param: new { Id = skateboardId}
                );
            }
        }
    }
}
