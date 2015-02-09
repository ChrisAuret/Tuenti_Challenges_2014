using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Anonymous_Poll
{
    public class Student
    {
        public string Name { get; set; }
        public char Gender { get; set; }
        public int Age { get; set; }
        public string Subject { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}", Name, Gender, Age, Subject, Year);
        }
    }

    public class Criteria
    {
        public char Gender { get; set; }
        public int Age { get; set; }
        public string Subject { get; set; }
        public int Year { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = @"10
F,18,Mechanical Engineering,1
F,18,Microbiology,3
M,20,Chemical Engineering,5
F,24,Civil Engineering,2
M,22,Foreign Service,5
F,20,Systems Engineering,3
F,20,Marketing,1
M,23,Environmental Science,1
M,25,Clinical Laboratory Science,5
M,25,Chemistry,2";


            //            var input = @"5
            //M,21,Human Resources Management,3
            //F,20,Systems Engineering,2
            //M,20,Manufacturing Engineering,3
            //M,18,Electrical Engineering,4
            //F,25,Construction Engineering,4";

            var criterias = LoadCriteriaFromArgs(input);

            var allStudents = LoadStudentsFromFile();

            var matches = FindMatches(allStudents, criterias);

            foreach (var match in matches)
            {
                var studentNames = "";

                var students = new List<Student>();

                matches.TryGetValue(match.Key, out students);

                foreach (var student in students)
                {
                    studentNames += student.Name + ",";
                }

                if (match.Value.Count > 0)
                    Console.WriteLine("Case #{0}: {1}", match.Key, studentNames.Substring(0, studentNames.Length - 1));
                else
                {
                    Console.WriteLine("Case #{0}: NONE", match.Key);
                }
            }

            Console.Read();
        }

        private static Dictionary<int, Criteria> LoadCriteriaFromArgs(string input)
        {
            var inputArray = input.Split('\n');
            var criterias = new Dictionary<int, Criteria>();

            for (int i = 1; i <= inputArray.Length - 1; i++)
            {
                var criteriaData = inputArray[i].Split(',');

                var criteria = new Criteria
                {
                    Gender = char.Parse(criteriaData[0]),
                    Age = int.Parse(criteriaData[1]),
                    Subject = criteriaData[2],
                    Year = int.Parse(criteriaData[3])
                };

                criterias.Add(i, criteria);
            }

            return criterias;
        }

        private static List<Student> LoadStudentsFromFile()
        {
            var allStudents = new List<Student>();

            var sr = new StreamReader(new FileStream("students.txt", FileMode.Open));
            while (!sr.EndOfStream)
            {
                var studentData = sr.ReadLine().Split(',').ToArray();

                var student = new Student
                {
                    Name = studentData[0],
                    Gender = char.Parse(studentData[1]),
                    Age = int.Parse(studentData[2]),
                    Subject = studentData[3],
                    Year = int.Parse(studentData[4])
                };

                allStudents.Add(student);
            }

            return allStudents;
        }

        private static Dictionary<int, List<Student>> FindMatches(List<Student> students, Dictionary<int, Criteria> criterias)
        {
            var matchingStudents = new Dictionary<int, List<Student>>();

            foreach (var criteria in criterias)
            {
                var matches = students.Where(x => x.Age == criteria.Value.Age
                                                  && x.Gender == criteria.Value.Gender
                                                  && x.Subject == criteria.Value.Subject
                                                  && x.Year == criteria.Value.Year).OrderBy(x => x.Name).ToList();

                matchingStudents.Add(criteria.Key, matches);
            }

            return matchingStudents;
        }
    }
}