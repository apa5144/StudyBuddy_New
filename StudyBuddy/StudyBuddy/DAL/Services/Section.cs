﻿using System.Collections.Generic;
using BLToolkit.DataAccess;
using StudyBuddy.Models;
using StudyBuddy.DAL.Interfaces;

namespace StudyBuddy.DAL.Services
{
    public abstract class SectionService : DataAccessor, ISection
    {
        [SprocName("Section_GetAll")]
        public abstract List<Section> GetAll();

        [SprocName("Section_GetOne")]
        public abstract Section GetOne(int id);

        [SprocName("Section_Delete")]
        public abstract void Delete(int id);

        [SprocName("Section_Create")]
        public abstract Section Create(string section, string instructor);

        [SprocName("Section_Update")]
        public abstract Section Update(int id, string section, string instructor);

        [SprocName("Section_GetSectionByCourseSubjectAndNumber")]
        public abstract List<string> GetSectionByCourseSubjectAndNumber(string subject, string number);

        [SprocName("Section_GetSectionIdByCourseSubjectAndNumberAndSection")]
        public abstract int GetSectionIdByCourseSubjectAndNumber(string subject, string number, string section);
    }
}
