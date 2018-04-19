using System.Collections.Generic;
using BLToolkit.DataAccess;
using StudyBuddy.Models;
using StudyBuddy.DAL.Interfaces;
using System.Security.Principal;

namespace StudyBuddy.DAL.Services
{
    public abstract class StudentService : DataAccessor, IStudent
    {
        [SprocName("Student_GetAll")]
        public abstract List<Student> GetAll();

        [SprocName("Student_GetOne")]
        public abstract Student GetOne(string guid);

        [SprocName("Student_Delete")]
        public abstract void Delete(int id);

        [SprocName("Student_Create")]
        public abstract Student Create(string firstName, string lastName, string email, string password, string guid);

        [SprocName("Student_Update")]
        public abstract Student Update(int id, string firstName, string lastName, long phoneNumber, bool availability, string profilePic);

        [SprocName("Student_GetByCredentials")]
        public abstract Student GetByCredentials(string email, string password);

        [SprocName("Student_DoesEmailExist")]
        public abstract bool DoesEmailExist(string email);

        [SprocName("Student_UpdatePasswordById")]
        public abstract void UpdatePasswordById(int id, string newPassword);

        [SprocName("Student_UpdateEmailById")]
        public abstract void UpdateEmailById(int id, string newEmail);

        [SprocName("Student_VerifyByGuid")]
        public abstract void VerifyByGuid(string guid);

        [SprocName("Student_GetByGuid")]
        public abstract Student GetByGuid(string guid);

        [SprocName("Student_GetGuidById")]
        public abstract string GetGuidById(int id);

        [SprocName("Student_GetByEmail")]
        public abstract Student GetByEmail(string email);
    }
}
