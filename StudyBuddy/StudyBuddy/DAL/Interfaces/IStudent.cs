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

        Student Update(string guid, string firstName, string lastName, long phoneNumber, bool availability, string profilePic);

        Student GetByCredentials(string email, string password);

        bool DoesEmailExist(string email);

        void UpdatePasswordByGuid(string guid, string newPassword);

        void UpdateEmailByGuid(string guid, string newEmail);

        void VerifyByGuid(string guid);

        Student GetByGuid(string guid);

        string GetGuidById(int id);

        Student GetByEmail(string email);

        void Deactivate(string guid);

        SecurityViewModel GetSecurityInformationByGuid(string guid);

        void RemoveProfilePictureByGuid(string guid);
    }
}
