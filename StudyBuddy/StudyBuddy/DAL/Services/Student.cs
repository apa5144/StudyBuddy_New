﻿using System.Collections.Generic;
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
        public abstract Student Update(string guid, string firstName, string lastName, long phoneNumber, string profilePic);

        [SprocName("Student_GetByCredentials")]
        public abstract Student GetByCredentials(string email, string password);

        [SprocName("Student_DoesEmailExist")]
        public abstract bool DoesEmailExist(string email);

        [SprocName("Student_UpdatePasswordByGuid")]
        public abstract void UpdatePasswordByGuid(string guid, string newPassword);

        [SprocName("Student_UpdateEmailByGuid")]
        public abstract void UpdateEmailByGuid(string guid, string newEmail);

        [SprocName("Student_VerifyByGuid")]
        public abstract void VerifyByGuid(string guid);

        [SprocName("Student_GetByGuid")]
        public abstract Student GetByGuid(string guid);

        [SprocName("Student_GetGuidById")]
        public abstract string GetGuidById(int id);

        [SprocName("Student_GetByEmail")]
        public abstract Student GetByEmail(string email);

        [SprocName("Student_Deactivate")]
        public abstract void Deactivate(string guid);

        [SprocName("Student_GetSecurityInformationByGuid")]
        public abstract SecurityViewModel GetSecurityInformationByGuid(string guid);

        [SprocName("Student_RemoveProfilePictureByGuid")]
        public abstract void RemoveProfilePictureByGuid(string guid);

        [SprocName("Student_UpdateAvailabilityByGuid")]
        public abstract void UpdateAvailabilityByGuid(string guid);
    }
}
