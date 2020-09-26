using System;
using System.Collections.Generic;
using System.Net;
using Mephist.Models;
using Xunit;
using System.ComponentModel.DataAnnotations;

namespace Mephist.Tests
{
    public class TeacherTest
    {
       
        [Fact]
        public void InitTest()
        {
            //Arrange
            List<Teacher> correct = new List<Teacher>
            {
               
                new Teacher() {FullName="иванов Иван Иваныч"},
                new Teacher() {FullName="Ива2нов Иван Иваныч"},
                new Teacher() {FullName="s Иван Иваныч"},
                new Teacher() {FullName="Uванов Иван Иваныч"},
                new Teacher() {FullName="Иванов Иваныч"},
                new Teacher() {FullName="Иванов  Иван Иваныч" },
                new Teacher() {FullName="123 123 123" },
                new Teacher() {FullName="" },
                new Teacher()
            };

            List<Teacher> uncorrect = new List<Teacher>
            {
                new Teacher() {FullName="Иванов Иван Иваныч" },
                new Teacher() {FullName="Краевский Никита Алекснадрович"}
                
            };

            //Act

            //Assert
            foreach (var teacher in uncorrect)
            {
                Assert.False(Validate(teacher));
            }

            foreach (var teacher in correct)
            {
                Assert.True(Validate(teacher));
            }


        }

        private bool Validate(Teacher teacher)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(teacher);

            return Validator.TryValidateObject(teacher, context, results, true);
        }

       // private void TestFullName()
    }
}
