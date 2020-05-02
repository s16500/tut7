using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tut7.Models;

namespace Tut7.Services
{
    public class SqlDbService : Tut7.Services.IDbService

    {


        public bool lastname(string lastname)
        {


            using (var con = new SqlConnection("Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = @"select indexNumber,FirstName,LastName from Students;";
                con.Open();
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    return BadRequest(404);
                }
                else
                {
                    return true;
                }



            }
        }

        private bool BadRequest(int v)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudents()
        {
            {
                var listOfStudents = new List<Student>();
                using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
                {
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"select FirstName, LastName from students;";
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var st = new Student
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                
                            };
                            listOfStudents.Add(st);
                        }
                    }
                }

                return listOfStudents;
            }
        }
            


    }
}