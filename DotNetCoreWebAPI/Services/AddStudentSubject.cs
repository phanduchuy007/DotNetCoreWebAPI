using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository;
using DotNetCoreWebAPI.Dal.Repository.SubjectjRepository;
using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.Services
{
    public class AddStudentSubject
    {
        StudentDataContext _studentData;
        IStudentRepository _studentRepository;
        ISubjectsRepository _subjectsRepository;

        public AddStudentSubject(IStudentRepository repositoryStudent, ISubjectsRepository subjectsRepository, StudentDataContext studentData)
        {
            _studentData = studentData;
            _studentRepository = repositoryStudent;
            _subjectsRepository = subjectsRepository;
        }

        public void AddTable(Operation operation)
        {
            using (var dbStudentTransaction = _studentData.Database.BeginTransaction())
            {
                try
                {
                    var student = (from st in _studentRepository.Gets() where st.Email == operation.Email select st).FirstOrDefault();

                    if (student == null)
                    {
                        _studentRepository.Add(new Student()
                        {
                            Name = operation.Name,
                            Address = operation.Address,
                            Email = operation.Email
                        });
                        _studentRepository.SaveChanges();

                        var checkStudent = (from st in _studentRepository.Gets() where st.Email == operation.Email select st).FirstOrDefault();

                        _subjectsRepository.Add(new Subjects()
                        {
                            IDStudent = checkStudent.ID,
                            Subject = operation.Subject,
                            Teacher = operation.Teacher,
                            Classroom = operation.Classroom,
                            Mark = operation.Mark
                        });
                        _subjectsRepository.SaveChanges();
                    }

                    if (student != null)
                    {
                        _subjectsRepository.Add(new Subjects()
                        {
                            IDStudent = student.ID,
                            Subject = operation.Subject,
                            Teacher = operation.Teacher,
                            Classroom = operation.Classroom,
                            Mark = operation.Mark
                        });
                        _subjectsRepository.SaveChanges();
                    }     

                    dbStudentTransaction.Commit();
                }
                catch (Exception)
                {
                    dbStudentTransaction.Rollback();
                }
            }
        }

        public void DeleteDataStudent(int id)
        {
            using (var dbStudentTransaction = _studentData.Database.BeginTransaction())
            {
                try
                {
                    var student = (from st in _studentRepository.Gets() where st.ID == id select st).FirstOrDefault();
                    
                    if (student != null)
                    {
                        var subject = (from st in _subjectsRepository.Gets() where st.IDStudent == student.ID select st).ToList();

                        if (subject != null)
                        {
                            foreach (var sub in subject)
                            {
                                _subjectsRepository.Delete(sub);
                            }                         
                            _subjectsRepository.SaveChanges();

                            _studentRepository.Delete(student);
                            _studentRepository.SaveChanges();
                        }
                        else
                        {
                            _studentRepository.Delete(student);
                            _studentRepository.SaveChanges();
                        }
                    }
                    dbStudentTransaction.Commit();
                }
                catch (Exception)
                {
                    dbStudentTransaction.Rollback();
                    throw;
                }
            }
        }
    }
}
