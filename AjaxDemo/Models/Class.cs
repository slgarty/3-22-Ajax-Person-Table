using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AjaxDemo.Models
{
   
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PersonDb
    {
        private readonly string _connectionString;

        public PersonDb(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void Add(Person person)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {

                cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) VALUES " +
                                  "(@firstName, @lastName, @age) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                conn.Open();
                person.Id = (int)(decimal)cmd.ExecuteScalar();
            }
        }
        public void Edit(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = ("UPDATE People SET FirstName = @firstName, LastName = @lastName, Age = @age WHERE Id = @Id");
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                cmd.Parameters.AddWithValue("@id", person.Id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = ("delete  from people where Id= @id");
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<Person> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            List<Person> ppl = new List<Person>();
            conn.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ppl.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }

            return ppl;
        }
        public Person GetPersonById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM People where id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            var reader = cmd.ExecuteReader();
            reader.Read();
            return new Person
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Age = (int)reader["Age"]
            };

        }
    }
}
