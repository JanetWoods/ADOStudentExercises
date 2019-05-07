using studentexercises.Data;
using studentexercises.Models;
using System;
using System.Collections.Generic;

namespace studentexercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            Console.WriteLine("We are RUNNING THE PROGRAM");
            Console.WriteLine("About to see the list of students:");

            List<Student> students = repository.GetAllStudents();


            foreach (Student student in students)
            {
                Console.WriteLine($"{student.FirstName}, {student.LastName}, Cohort: {student.CohortId}");
            }
            Console.ReadLine();


            Console.WriteLine("About to see the list of exercises....");

            List<Exercise> exercises = repository.GetAllExercises();
            
            foreach (Exercise exercise in exercises)
            {
                Console.WriteLine($"{exercise.Title}: {exercise.Language}");
            };

            
            Console.WriteLine("--");
            Console.WriteLine("About to see just the Javascript exercises....");

            List<Exercise> specificExercises = repository.GetExercisesByLanguage("JavaScript");

            foreach (Exercise exercise in specificExercises)
            {
                Console.WriteLine($"{exercise.Title}: {exercise.Language}");

            }

            Console.WriteLine("That was the JavaScript exercises.  we'll add another exercise now...");
            Console.ReadLine();


            Exercise newExercise = new Exercise
            {
                Title = "Insert New Things",
                Language = "SQL",
            };

            repository.AddExercise(newExercise);

            List<Exercise> exercises2 = repository.GetAllExercises();

            foreach (Exercise exercise in exercises2)
            {
                Console.WriteLine($"{exercise.Title}: {exercise.Language}");
            };



            Console.WriteLine();
            Console.WriteLine("We just added a new exercise.  Next up, we'll add a new instructor");
            Console.ReadLine();

            Instructor newInstructor = new Instructor
            {
                FirstName = "Gruff",
                LastName = "McGee",
                SlackHandle = "Woof-Woof",
                CohortId = 2,
                Speciality = "Python",
            };

            repository.AddInstructor(newInstructor);

            Console.WriteLine("We added a new instructor. Let's see if she shows up in the list....");

            repository.GetInstructors();

            List<Instructor> theInstructors = repository.GetInstructors();

            foreach(Instructor instruct in theInstructors)
            {
                Console.WriteLine($"{instruct.FirstName} {instruct.LastName}, is with Cohort {instruct.CohortId}. His/Her speciality is {instruct.Speciality} and slackhandle is {instruct.SlackHandle}");
            };

            var assingment = 2;
            var stud = 3;

            repository.AssignExercise(assingment, stud);
   
        }
    }
}

      

    //Find all instructors in the database.Include each instructor's cohort.
    //Insert a new instructor into the database.Assign the instructor to an existing cohort.
    //Assign an existing exercise to an existing student.
