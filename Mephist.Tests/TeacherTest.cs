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
            List<Teacher> teachers = new List<Teacher>
            {
                new Teacher() {FullName="Иванов Иван Иваныч" },
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

            //Act

            //Assert
        

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
