using System;
using System.Collections.Generic;

class University
{
    private List<Department> departments;

    public University()
    {
        departments = new List<Department>();
    }

    public void AddDepartment(Department department)
    {
        departments.Add(department);
        department.SetUniversity(this); // Set the university for the department
    }

    public void RemoveDepartment(Department department)
    {
        departments.Remove(department);
        department.RemoveUniversity(); // Remove the university reference from the department
    }

    public List<Department> GetDepartments()
    {
        return departments;
    }
}

class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    private List<Student> students;
    private University university; // Reference to the university

    public Department()
    {
        students = new List<Student>();
    }

    public void SetUniversity(University university)
    {
        this.university = university;
    }

    public void RemoveUniversity()
    {
        this.university = null;
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        students.Remove(student);
    }

    public List<Student> GetStudents()
    {
        return students;
    }
}

abstract class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

    public abstract void Display();
}

class UndergraduateStudent : Student
{
    public int YearLevel { get; set; }

    public override void Display()
    {
        Console.WriteLine("Undergraduate Student: ID = " + Id + ", Name = " + Name + ", Year Level = " + YearLevel);
    }
}

class GraduateStudent : Student
{
    public string ResearchTopic { get; set; }

    public override void Display()
    {
        Console.WriteLine("Graduate Student: ID = " + Id + ", Name = " + Name + ", Research Topic = " + ResearchTopic);
    }
}

class Program
{
    static University university;

    static void Main(string[] args)
    {
        university = new University();
        int choice;

        do
        {
            DisplayMenu();
            choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    AddStudent();
                    break;
                case 2:
                    Console.Clear();
                    RemoveStudent();
                    break;
                case 3:
                    Console.Clear();
                    AddDepartment();
                    break;
                case 4:
                    Console.Clear();
                    RemoveDepartment();
                    break;
                case 5:
                    Console.Clear();
                    ShowAllStudents();
                    break;
                case 6:
                    Console.Clear();
                    ShowAllDepartments();
                    break;
                case 7:
                    Console.Clear();
                    Console.WriteLine("VEBO ( VietNam Education and Beyond Outreach ) School is the most popular " +
                        "school in Viet Nam. We have a long history from the begining of the school. " +
                        "We will bring your kids the best education in Viet Nam for a brighter future of them. TMB " +
                        "<('')\n" +
                        "Vyquaquay, First president of VEBO school.");
                    break;
                case 8:
                    Console.WriteLine("Exiting the program...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        } while (choice != 8);
    }

    static void DisplayMenu()
    {
        Console.WriteLine("Menu:");
        Console.WriteLine("1. Add Student");
        Console.WriteLine("2. Remove Student");
        Console.WriteLine("3. Add Department");
        Console.WriteLine("4. Remove Department");
        Console.WriteLine("5. Show All Students");
        Console.WriteLine("6. Show All Departments");
        Console.WriteLine("7. About Us");
        Console.WriteLine("8. Exit");
    }

    static int GetUserChoice()
    {
        int choice;

        while (true)
        {
            Console.Write("Enter your choice (1-8): ");
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 8)
            {
                return choice;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }

    static void AddStudent()
    {
        Console.Write("Enter student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID. Please try again.");
            return;
        }

        if (IsStudentIdExists(id))
        {
            Console.WriteLine("Student ID already exists. Please try again.");
            return;
        }

        Console.Write("Enter student name: ");
        string name = Console.ReadLine();

        Student student;
        Console.WriteLine("Select student type:");
        Console.WriteLine("1. Undergraduate Student");
        Console.WriteLine("2. Graduate Student");
        Console.Write("Enter student type (1-2): ");
        if (int.TryParse(Console.ReadLine(), out int studentType) && studentType >= 1 && studentType <= 2)
        {
            if (studentType == 1)
            {
                Console.Write("Enter year level: ");
                if (!int.TryParse(Console.ReadLine(), out int yearLevel))
                {
                    Console.WriteLine("Invalid year level. Please try again.");
                    return;
                }

                student = new UndergraduateStudent { Id = id, Name = name, YearLevel = yearLevel };
            }
            else
            {
                Console.Write("Enter research topic: ");
                string researchTopic = Console.ReadLine();

                student = new GraduateStudent { Id = id, Name = name, ResearchTopic = researchTopic };
            }

            Console.WriteLine("Student added successfully!");

            Department department = SelectDepartment();
            if (department != null)
            {
                department.AddStudent(student);
                Console.WriteLine("Student added to the department.");
            }
            else
            {
                Console.WriteLine("Department not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid student type. Please try again.");
        }
    }

    static void RemoveStudent()
    {
        Department department = SelectDepartment();
        if (department != null)
        {
            Console.Write("Enter student ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid student ID. Please try again.");
                return;
            }

            Student student = FindStudent(department, studentId);
            if (student != null)
            {
                department.RemoveStudent(student);
                Console.WriteLine("Student removed from the department.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
        else
        {
            Console.WriteLine("Department not found.");
        }
    }

    static Department SelectDepartment()
    {
        Console.Write("Enter department ID: ");
        if (!int.TryParse(Console.ReadLine(), out int departmentId))
        {
            Console.WriteLine("Invalid department ID. Please try again.");
            return null;
        }

        Department department = FindDepartment(departmentId);
        return department;
    }

    static void AddDepartment()
    {
        Console.Write("Enter department ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid department ID. Please try again.");
            return;
        }

        if (IsDepartmentIdExists(id))
        {
            Console.WriteLine("Department ID already exists. Please try again.");
            return;
        }

        Console.Write("Enter department name: ");
        string name = Console.ReadLine();

        Department department = new Department { Id = id, Name = name };
        university.AddDepartment(department);
        Console.WriteLine("Department added successfully!");
    }

    static void RemoveDepartment()
    {
        Console.Write("Enter department ID to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int departmentId))
        {
            Console.WriteLine("Invalid department ID. Please try again.");
            return;
        }

        Department department = FindDepartment(departmentId);
        if (department != null)
        {
            university.RemoveDepartment(department);
            Console.WriteLine("Department removed successfully!");
        }
        else
        {
            Console.WriteLine("Department not found.");
        }
    }

    static void ShowAllStudents()
    {
        Console.WriteLine("All Students:");
        foreach (Department department in university.GetDepartments())
        {
            Console.WriteLine("Department: " + department.Name);
            foreach (Student student in department.GetStudents())
            {
                student.Display();
            }
            Console.WriteLine();
        }
    }

    static void ShowAllDepartments()
    {
        Console.WriteLine("All Departments:");
        foreach (Department department in university.GetDepartments())
        {
            Console.WriteLine("Department ID: " + department.Id + ", Name: " + department.Name);
        }
    }

    static Department FindDepartment(int departmentId)
    {
        foreach (Department department in university.GetDepartments())
        {
            if (department.Id == departmentId)
            {
                return department;
            }
        }
        return null;
    }

    static Student FindStudent(Department department, int studentId)
    {
        foreach (Student student in department.GetStudents())
        {
            if (student.Id == studentId)
            {
                return student;
            }
        }
        return null;
    }

    static bool IsDepartmentIdExists(int departmentId)
    {
        foreach (Department department in university.GetDepartments())
        {
            if (department.Id == departmentId)
            {
                return true;
            }
        }
        return false;
    }

    static bool IsStudentIdExists(int studentId)
    {
        foreach (Department department in university.GetDepartments())
        {
            foreach (Student student in department.GetStudents())
            {
                if (student.Id == studentId)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
