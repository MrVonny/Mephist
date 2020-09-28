using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models
{
    public struct Rating
    {

        private int character;
        private int teaching;
        private int exams;

        private int characterVotes;
        private int teachingVotes;
        private int examsVotes;

        public Rating(int character, int teaching, int exams, int characterVotes, int teachingVotes, int examsVotes)
        {
            this.character = character;
            this.teaching = teaching;
            this.exams = exams;
            this.characterVotes = characterVotes;
            this.teachingVotes = teachingVotes;
            this.examsVotes = examsVotes;
        }

        public void CharacterVote(int rating)
        {
            if (rating < -2 || rating > 2) throw new ArgumentOutOfRangeException();
            character += rating;
            characterVotes++;
        }
        public void TeachingVote(int rating)
        {
            if(rating < -2 || rating > 2) throw new ArgumentOutOfRangeException();
            teaching += rating;
            teachingVotes++;
        }
        public void ExamsVote(int rating)
        {
            if(rating < -2 || rating > 2) throw new ArgumentOutOfRangeException();
            exams += rating;
            examsVotes++;

        }

        public (double, int) GetCharacter()
        {
            return ((double)character / characterVotes, characterVotes);
        }

        public (double, int) GetTeaching()
        {
            return ((double)teaching / teachingVotes, teachingVotes);
        }

        public (double, int) GetExmas()
        {
            return ((double)exams / examsVotes, examsVotes);
        }
    }
}
