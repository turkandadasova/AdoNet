using AdoNET.Helpers;
using AdoNET.Models;
using AdoNET.Service;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SQL
{
    internal class Program
    {
        const string conString = "Server=.\\SQLEXPRESS;Database=BP215;Trusted_Connection=True;TrustServerCertificate=True";
        static void Main(string[] args)
        {
           // CreateStudent("Ayxan", "Veliyev", "7");
            //DeleteStudent(8);
            // ReadDataTable();
            //StudentService.GetAll().ForEach(student => Console.WriteLine(student.Name + " " + student.Surname + " " + student.Code + " " + student.Age));
            using (SqlConnection conn = new (conString))
            {
                var students = conn.Query<STUDENTS>("SELECT * FROM STUDENTS");
                foreach(var item in students)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

        static void CreateStudent(string name,string surname,string code)
        {
            SqlHelper.Exec($"INSERT INTO STUDENTS VALUES (N'{name}',N'{surname}','{code}',0)");
            
        }

         static void DeleteStudent(int id)
         {
            int result = 0;
            using (SqlConnection conn = new(conString))
            {
                using (SqlCommand cmd = new($"DELETE STUDENTS WHERE Id ={id}",conn))
                {
                    conn.Open ();
                    result = cmd.ExecuteNonQuery();
                }
            }
            if (result > 0)
            {
                Console.WriteLine("Student deleted succesfully");
            }
            else
            {
                Console.WriteLine("Student doesnt exist in database");
            }
         }

    static void ReadDataTable()
        {
            string query = "SELECT * FROM STUDENTS";
            DataTable dt = new DataTable();
            using (SqlConnection connectoin = new(conString))
            {
                using (SqlDataAdapter sda = new(query, connectoin))
                {
                    connectoin.Open();
                    sda.Fill(dt);
                }
            }
            foreach (DataRow item in dt.Rows)
            {
                Console.WriteLine(item[0] + " " + item[1] + " " + item[2]);
            }
        }

        static void ReadWithReader()
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Books", conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0] + " " + reader[1] + " " + reader[2]);
                    }
                    // conn.Close();
                    //cmd.Dispose();
                }
            }
           // GC.Collect();

        }
    }
}
