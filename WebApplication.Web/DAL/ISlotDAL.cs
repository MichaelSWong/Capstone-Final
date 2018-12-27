using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public interface ISlotDAL
    {
        Slot GetSlot(int id);
        List<Slot> GetSlots();
        List<Slot> GetSlots(int tourneyID);

        int CreateSlot(Slot slot);

        Slot AssignInternalObjs(Slot slot);
        List<Slot> AssignInternalObjs(List<Slot> slots);
    }
}
