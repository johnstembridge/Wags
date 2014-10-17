using System.Collections.Generic;
using Wags.DataModel;

namespace Wags.BusinessLayer
{
    public interface IBusinessLayer
    {
        IList<Member> GetAllMembers();
        IList<Player> GetAllPlayers();
        Member GetMemberById(int id);
        Member GetMemberByName(string name);
    }
}
