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
               
                new Teacher() {FullName="������ ���� ������"},
                new Teacher() {FullName="���2��� ���� ������"},
                new Teacher() {FullName="s ���� ������"},
                new Teacher() {FullName="U����� ���� ������"},
                new Teacher() {FullName="������ ������"},
                new Teacher() {FullName="������  ���� ������" },
                new Teacher() {FullName="123 123 123" },
                new Teacher() {FullName="" },
                new Teacher()
            };

            List<Teacher> uncorrect = new List<Teacher>
            {
                new Teacher() {FullName="������ ���� ������" },
                new Teacher() {FullName="��������� ������ �������������"}
                
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
