using System;
using System.Collections.Generic;

//collecting data of the Students And Departments
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
//Own it name/id and containt the student in it
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
//just abtract class to make type of student
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
//first type of student
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
//second type
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
//project of the second type student
class Project
{
    public int ProjectId { get; set; }
    public string Status { get; set; }
}
//start of the program
class Program
{
    //Start the list
    static University university;
    //main
    static void Main(string[] args)
    {
         university = new University();
        int choice;
        //get choice and start switch case
        do
        {
            DisplayMenu();
            choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    ManageStudents();
                    break;
                case 2:
                    Console.Clear();
                    ManageDepartments();
                    break;
                case 3:
                    Console.WriteLine("VEBO (VietNam Education and Beyond Outreach) School is the most popular " +
                        "school in Vietnam. We have a long history from the beginning of the school. " +
                        "We will bring your kids the best education in Vietnam for a brighter future for them. TMB " +
                        "<('')\n" +
                        "Vyquaquay, First President of VEBO School.");
                    break;
                case 4:
                    Console.WriteLine("Exiting the program...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        } while (choice != 4);
    }
    //the menu
    static void DisplayMenu()
    {
        Console.WriteLine("Menu:");
        Console.WriteLine("1. Manage Students");
        Console.WriteLine("2. Manage Departments");
        Console.WriteLine("3. About US");
        Console.WriteLine("4. Exit");
    }
    //mane student option
    static void ManageStudents()
    {
        int choiceS;

        do
        {
            Console.WriteLine("Manage Students:");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Update Student Information");
            Console.WriteLine("3. Remove Student");
            Console.WriteLine("4. Show All Students");
            Console.WriteLine("5. Go Back");

            choiceS = GetUserChoiceS();

            switch (choiceS)
            {
                case 1:
                    Console.Clear();
                    AddStudent();
                    break;
                case 2:
                    Console.Clear();
                    UpdateStudent();
                    break;
                case 3:
                    Console.Clear();
                    RemoveStudent();
                    break;
                case 4:
                    Console.Clear();
                    ShowAllStudents();
                    break;
                case 5:
                    Console.WriteLine("Returning to the main menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        } while (choiceS != 5);
    }
    //manage department option
    static void ManageDepartments()
    {
        int choiceD;

        do
        {
            Console.WriteLine("Manage Departments:");
            Console.WriteLine("1. Add Department");
            Console.WriteLine("2. Remove Department");
            Console.WriteLine("3. Show All Departments");
            Console.WriteLine("4. Go Back");

            choiceD = GetUserChoiceD();

            switch (choiceD)
            {
                case 1:
                    Console.Clear();
                    AddDepartment();
                    break;
                case 2:
                    Console.Clear();
                    RemoveDepartment();
                    break;
                case 3:
                    Console.Clear();
                    ShowAllDepartments();
                    break;
                case 4:
                    Console.WriteLine("Returning to the main menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        } while (choiceD != 4);
    }
    //first user choice
    static int GetUserChoice()
    {
        int choice;

        while (true)
        {
            Console.Write("Enter your choice (1-4): ");
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 4)
            {
                return choice;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
    //If user choose manage student
    static int GetUserChoiceS()
    {
        int choiceS;

        while (true)
        {
            Console.Write("Enter your choice (1-5): ");
            if (int.TryParse(Console.ReadLine(), out choiceS) && choiceS >= 1 && choiceS <= 5)
            {
                return choiceS;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
    //If user choose manage department
    static int GetUserChoiceD()
    {
        int choiceD;

        while (true)
        {
            Console.Write("Enter your choice (1-4): ");
            if (int.TryParse(Console.ReadLine(), out choiceD) && choiceD >= 1 && choiceD <= 4)
            {
                return choiceD;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
    // add student information
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
    //for some reason if user input the ID that not have
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
    // check for duplicate ID student
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
    //check duplicate department ID
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

