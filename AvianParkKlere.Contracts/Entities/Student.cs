﻿namespace AvainParkKlere.Api.EntityFrameworkCore.Entities
{
    public class Student
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string Surname { get; set; }
        public int?  Age { get; set; }
        public int? Grade { get; set; }
        
    }
}
