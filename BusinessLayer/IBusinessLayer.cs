using System.Collections.Generic;
using Wags.DataModel;

namespace Wags.BusinessLayer
{
    public interface IBusinessLayer
    {
        IList<Player> GetAllPlayers();
        //IList<Member> GetAllMembers();
        //Member GetMemberById(int id);
        //Member GetMemberByName(string name);
    }
}
