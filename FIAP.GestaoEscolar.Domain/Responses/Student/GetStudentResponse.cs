﻿namespace FIAP.GestaoEscolar.Domain.Responses.Student
{
    public class GetStudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
