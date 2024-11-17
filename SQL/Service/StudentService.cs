using AdoNET.Helpers;
using AdoNET.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNET.Service
{
    static class StudentService
    {
        public static void Add(STUDENTS std)
        {
            string query = $"INSERT INTO STUDENTS VALUES (N'{std.Name},N'{std.Surname},N'{std.Code}')";
            SqlHelper.Exec(query);

        }

        public static List<STUDENTS> GetAll()
        {
            string query = "SELECT * FROM STUDENTS";
            var dataTable = SqlHelper.Read(query);
            List<STUDENTS> students = new List<STUDENTS>();
            foreach (DataRow dr  in dataTable.Rows)
            {
                students.Add(new STUDENTS
                {
                    Id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Surname = (string)dr["Surname"],
                    Code = (string)dr["Code"],
                    Age = (int)dr["Age"]
                });
            }
            return students;
        }




    }

}
