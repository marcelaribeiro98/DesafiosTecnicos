﻿namespace FIAP.GestaoEscolar.Domain.Requests.StudentClass
{
    public class UpdateStudentClassRequest
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public int CurrentClassId { get; set; }
        public bool Active { get; set; }
    }
}
