using System.Collections.Generic;
using StudyBuddy.Models;

namespace StudyBuddy.DAL.Interfaces
{
    public interface IStudent
    {
        List<Student> GetAll();

        Student GetOne(int id);

        Student Create(string firstName, string lastName, string email, string password, string guid);

        void Delete(int id);

        Student Update(int id, string firstName, string lastName, long phoneNumber, bool availability, string profilePic);

        Student GetByCredentials(string email, string password);

        bool DoesEmailExist(string email);

        void UpdatePasswordById(int id, string newPassword);

        void UpdateEmailById(int id, string newEmail);

        void VerifyByGuid(string guid);

        Student GetByGuid(string guid);

        string GetGuidById(int id);

        Student GetByEmail(string email);
    }
}
