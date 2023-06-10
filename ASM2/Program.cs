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
        student.SetDepartment(this); // Set the department for the student
    }

    public void RemoveStudent(Student student)
    {
        students.Remove(student);
        student.RemoveDepartment(); // Remove the department reference from the student
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
    private Department department; // Reference to the department

    public void SetDepartment(Department department)
    {
        this.department = department;
    }

    public void RemoveDepartment()
    {
        this.department = null;
    }

    public abstract void Display();
    public abstract void UpdateInformation();
}

class UndergraduateStudent : Student
{
    public int YearLevel { get; set; }

    public override void Display()
    {
        Console.WriteLine("Undergraduate Student: ID = " + Id + ", Name = " + Name + ", Year Level = " + YearLevel);
    }

    public override void UpdateInformation()
    {
        Console.Write("Enter updated year level: ");
        if (!int.TryParse(Console.ReadLine(), out int yearLevel))
        {
            Console.WriteLine("Invalid year level. Update failed.");
            return;
        }

        YearLevel = yearLevel;
        Console.WriteLine("Student information updated successfully!");
    }
}

class GraduateStudent : Student
{
    public string ResearchTopic { get; set; }
    public Project Project { get; set; }

    public override void Display()
    {
        Console.WriteLine("Graduate Student: ID = " + Id + ", Name = " + Name + ", Research Topic = " + ResearchTopic + ", Project Status = " + Project.Status);
    }

    public override void UpdateInformation()
    {
        Console.Write("Enter updated research topic: ");
        string researchTopic = Console.ReadLine();

        ResearchTopic = researchTopic;
        Console.WriteLine("Student information updated successfully!");
    }
}

class Project
{
    public int ProjectId { get; set; }
    public string Status { get; set; }
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
                    UpdateStudent();
                    break;
                case 8:
                    Console.WriteLine("VEBO ( VietNam Education and Beyond Outreach ) School is the most popular " +
                        "school in Viet Nam. We have a long history from the begining of the school. " +
                        "We will bring your kids the best education in Viet Nam for a brighter future of them. TMB " +
                        "<('')\n" +
                        "Vyquaquay, First president of VEBO school.");
                    break;
                case 9:
                    Console.WriteLine("Exiting the program...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        } while (choice != 9);
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
        Console.WriteLine("7. Update Student Information");
        Console.WriteLine("8. About US");
        Console.WriteLine("9. Exit");
    }

    static int GetUserChoice()
    {
        int choice;

        while (true)
        {
            Console.Write("Enter your choice (1-9): ");
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 9)
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

                Project project = new Project();
                Console.Write("Enter project ID: ");
                if (!int.TryParse(Console.ReadLine(), out int projectId))
                {
                    Console.WriteLine("Invalid project ID. Please try again.");
                    return;
                }
                project.ProjectId = projectId;

                Console.Write("Enter project status: ");
                string projectStatus = Console.ReadLine();
                project.Status = projectStatus;

                student = new GraduateStudent { Id = id, Name = name, ResearchTopic = researchTopic, Project = project };
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

    static void UpdateStudent()
    {
        Department department = SelectDepartment();
        if (department != null)
        {
            Console.Write("Enter student ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid student ID. Please try again.");
                return;
            }

            Student student = FindStudent(department, studentId);
            if (student != null)
            {
                Console.WriteLine("Select attribute to update:");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. Year Level (for Undergraduate Student)");
                Console.WriteLine("3. Research Topic (for Graduate Student)");
                Console.WriteLine("4. Project ID (for Graduate Student)");
                Console.WriteLine("5. Project Status (for Graduate Student)");
                Console.Write("Enter attribute to update (1-5): ");

                if (int.TryParse(Console.ReadLine(), out int attributeChoice) && attributeChoice >= 1 && attributeChoice <= 5)
                {
                    switch (attributeChoice)
                    {
                        case 1:
                            Console.Write("Enter new name: ");
                            string newName = Console.ReadLine();
                            student.Name = newName;
                            Console.WriteLine("Student name updated successfully!");
                            break;
                        case 2:
                            if (student is UndergraduateStudent)
                            {
                                Console.Write("Enter new year level: ");
                                if (!int.TryParse(Console.ReadLine(), out int newYearLevel))
                                {
                                    Console.WriteLine("Invalid year level. Please try again.");
                                    return;
                                }
                                ((UndergraduateStudent)student).YearLevel = newYearLevel;
                                Console.WriteLine("Undergraduate student year level updated successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid attribute choice for the selected student. Please try again.");
                            }
                            break;
                        case 3:
                            if (student is GraduateStudent)
                            {
                                Console.Write("Enter new research topic: ");
                                string newResearchTopic = Console.ReadLine();
                                ((GraduateStudent)student).ResearchTopic = newResearchTopic;
                                Console.WriteLine("Graduate student research topic updated successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid attribute choice for the selected student. Please try again.");
                            }
                            break;
                        case 4:
                            if (student is GraduateStudent)
                            {
                                Console.Write("Enter new project ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int newProjectId))
                                {
                                    Console.WriteLine("Invalid project ID. Please try again.");
                                    return;
                                }
                                ((GraduateStudent)student).Project.ProjectId = newProjectId;
                                Console.WriteLine("Graduate student project ID updated successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid attribute choice for the selected student. Please try again.");
                            }
                            break;
                        case 5:
                            if (student is GraduateStudent)
                            {
                                Console.Write("Enter new project status: ");
                                string newProjectStatus = Console.ReadLine();
                                ((GraduateStudent)student).Project.Status = newProjectStatus;
                                Console.WriteLine("Graduate student project status updated successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid attribute choice for the selected student. Please try again.");
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid attribute choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid attribute choice. Please try again.");
                }
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

    static void AddDepartment()
    {
        Console.Write("Enter department ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID. Please try again.");
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
        foreach (Department department in university.GetDepartments())
        {
            Console.WriteLine("Department: ID = " + department.Id + ", Name = " + department.Name);

            foreach (Student student in department.GetStudents())
            {
                student.Display();
            }

            Console.WriteLine();
        }
    }

    static void ShowAllDepartments()
    {
        List<Department> departments = university.GetDepartments();
        if (departments.Count > 0)
        {
            Console.WriteLine("Departments:");
            foreach (Department department in departments)
            {
                Console.WriteLine("ID = " + department.Id + ", Name = " + department.Name);
            }
        }
        else
        {
            Console.WriteLine("No departments found.");
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

    static bool IsStudentIdExists(int id)
    {
        foreach (Department department in university.GetDepartments())
        {
            foreach (Student student in department.GetStudents())
            {
                if (student.Id == id)
                {
                    return true;
                }
            }
        }

        return false;
    }

    static Student FindStudent(Department department, int id)
    {
        foreach (Student student in department.GetStudents())
        {
            if (student.Id == id)
            {
                return student;
            }
        }

        return null;
    }

    static bool IsDepartmentIdExists(int id)
    {
        foreach (Department department in university.GetDepartments())
        {
            if (department.Id == id)
            {
                return true;
            }
        }

        return false;
    }

    static Department FindDepartment(int id)
    {
        foreach (Department department in university.GetDepartments())
        {
            if (department.Id == id)
            {
                return department;
            }
        }

        return null;
    }
}

