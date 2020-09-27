using Mephist.Models.Intefaces;
using System;

namespace Mephist.Models
{
    public class Review : IUploadInfo
    {
        public Review(string text, bool anonymously, User user, DateTime createdDate)
        {
            Text = text;
            Anonymously = anonymously;
            User = user;
            CreatedDate = createdDate;
        }      
        public string Text { get; }
        public bool Anonymously { get; }
        public User User { get; }
        public DateTime CreatedDate { get; }

    }
}