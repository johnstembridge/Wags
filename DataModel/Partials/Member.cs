using System;

namespace Wags.DataModel
{
    public partial class Member
    {
        public Player ToPlayer()
        {
            return new Player()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Histories = Histories,
                Scores=Scores
            };
        }
    }
}
