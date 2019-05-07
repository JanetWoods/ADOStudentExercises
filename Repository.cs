using studentexercises.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace studentexercises.Data
{
    class Repository
    {

        public SqlConnection Connection
        {

            get //get address of database
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }

        }
        public List<Student> GetAllStudents()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, firstName, lastName, slackHandle, cohortId FROM Students";
                    SqlDataReader reader = cmd.ExecuteReader();

                    //Get all students from database and populate list
                    List<Student> students = new List<Student>();

                    while (reader.Read())//reader returns true as long as there is more data to read
                    {
                        //find column # of id
                        int idStudentIdPosition = reader.GetOrdinal("Id");
                        //get value of that id
                        int idValue = reader.GetInt32(idStudentIdPosition);

                        int firstNamePosition = reader.GetOrdinal("firstName");
                        string FirstName = reader.GetString(firstNamePosition);

                        int lastNamePosition = reader.GetOrdinal("lastName");
                        string LastName = reader.GetString(lastNamePosition);

                        int slackNamePosition = reader.GetOrdinal("slackHandle");
                        string SlackHandle = reader.GetString(slackNamePosition);

                        int cohortIdPosition = reader.GetOrdinal("cohortId");
                        int Cohort = reader.GetInt32(cohortIdPosition);

                        Student student = new Student
                        {
                            CohortId = idValue,
                            FirstName = FirstName,
                            LastName = LastName,
                            SlackHandle = SlackHandle,
                        };

                        students.Add(student);
                    }
                    reader.Close();

                    return students;
                }
            }
        }
        public List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, SoftwareLanguage, Title FROM Exercises";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        int idPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idPosition);

                        int positionSoftwareLanguage = reader.GetOrdinal("SoftwareLanguage");
                        string Language = reader.GetString(positionSoftwareLanguage);

                        int positionTitle = reader.GetOrdinal("Title");
                        string Title = reader.GetString(positionTitle);

                        Exercise exercise = new Exercise
                        {
                            Title = Title,
                            Language = Language
                        };
                        exercises.Add(exercise);
                    }
                    reader.Close();
                    return exercises;
                }
            }
        }

        public List<Exercise> GetExercisesByLanguage(string thisLanguage)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT Id, SoftwareLanguage, Title FROM Exercises WHERE SoftwareLanguage = @thisLanguage";
                    cmd.Parameters.Add(new SqlParameter("@thisLanguage", thisLanguage));

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> specificExercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        int idPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idPosition);

                        int positionSoftwareLanguage = reader.GetOrdinal("SoftwareLanguage");
                        string Language = reader.GetString(positionSoftwareLanguage);

                        int positionTitle = reader.GetOrdinal("Title");
                        string Title = reader.GetString(positionTitle);

                        Exercise exercise = new Exercise
                        {
                            Title = Title,
                            Language = Language
                        };
                        specificExercises.Add(exercise);
                    }
                    reader.Close();
                    return specificExercises;
                }
            }
        }
        public void AddExercise(Exercise newExercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO Exercises (Title, SoftwareLanguage) VALUES ('{newExercise.Title}', '{newExercise.Language}')";
                    cmd.ExecuteNonQuery();
                }
            }
         }
        public void AssignExercise(int assignment, int student)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO StudentExercises (ExcerciseId, StudentId) VALUES ('{assignment}', '{student}')";
                    SqlDataReader reader = cmd.ExecuteReader();
                }
            }
        }

        public void AddInstructor(Instructor newInstructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"INSERT INTO Instructors(FirstName, LastName, SlackHandle, CohortId, Specialty) VALUES ('{newInstructor.FirstName}', '{newInstructor.LastName}', '{newInstructor.SlackHandle}','{newInstructor.CohortId}', '{newInstructor.Speciality}')";
                    SqlDataReader reader = cmd.ExecuteReader();
                }
            }
        }
        
        public List<Instructor> GetInstructors()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, CohortId, FirstName, LastName, SlackHandle, Specialty FROM Instructors";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> instructors = new List<Instructor>();

                    while (reader.Read())
                    {
                        int idPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idPosition);

                        int FirstNamePosition = reader.GetOrdinal("FirstName");
                        string FirstName = reader.GetString(FirstNamePosition);

                        int LastNamePosition = reader.GetOrdinal("LastName");
                        string LastName = reader.GetString(LastNamePosition);

                        int SlackPosition = reader.GetOrdinal("SlackHandle");
                        string SlackHandle = reader.GetString(SlackPosition);

                        int SpecialPosition = reader.GetOrdinal("Specialty");
                        string Speciality = reader.GetString(SpecialPosition);

                        int CohortPosition = reader.GetOrdinal("CohortId");
                        int CohortId = reader.GetInt32(CohortPosition);
                                               
                        Instructor instructor = new Instructor
                        {
                            FirstName = FirstName,
                            LastName = LastName,
                            SlackHandle = SlackHandle,
                            Speciality = Speciality,
                            CohortId= CohortId,
                        };
                        instructors.Add(instructor);
                    }
                    reader.Close();
                    return instructors;
                }
            }
        }

        public List<Assignment> GetAllExerciseAssignments()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT s.firstName , s.lastName, e.Title, e.SoftwareLanguage FROM Students s, Exercises e" +
                        "from Students s" +
                        "JOIN StudentExercises x on s.id = x.StudentId" +
                        "JOIN Exercises e on e.id = x.ExerciseId";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Assignment> assignments= new List<Assignment>();

                    while (reader.Read())
                    {
                        int idPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idPosition);

                        int positionExerciseId = reader.GetOrdinal("ExerciseId");
                        int ExerciseId = reader.GetInt32(positionExerciseId);

                        int positionStudentId= reader.GetOrdinal("StudentId");
                        int StudentId = reader.GetInt32(positionStudentId);

                        Assignment assignment= new Assignment
                        {
                            StudentId = StudentId,
                            ExerciseId = ExerciseId,
                        };
                        assignments.Add(assignment);
                    }
                    reader.Close();

                    return assignments;
                }
            }
        }

    }
}
