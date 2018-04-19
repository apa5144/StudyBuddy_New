using System.Collections.Generic;
using StudyBuddy.Models;
using System.Security.Principal;

namespace StudyBuddy.DAL.Interfaces
{
    public interface IStudent
    {
        List<Student> GetAll();

        Student GetOne(string guid);

        Student Create(string firstName, string lastName, string email, string password, string guid);

        void Delete(int id);

        Student Update(int id, string firstName, string lastName, long phoneNumber, bool availability, string profilePic);

        Student GetByCredentials(string email, string password);

        bool DoesEmailExist(string email);

        void UpdatePasswordByGuid(string guid, string newPassword);

        void UpdateEmailById(int id, string newEmail);

        void VerifyByGuid(string guid);

        Student GetByGuid(string guid);

        string GetGuidById(int id);

        Student GetByEmail(string email);

        Student Deactivate(string guid);
    }
}
