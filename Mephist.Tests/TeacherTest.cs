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
            
            //Act

            //Assert
        

        }

        private bool Validate(Models.Employee teacher)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(teacher);

            return Validator.TryValidateObject(teacher, context, results, true);
        }

       // private void TestFullName()
    }
}
