using System.Collections.Generic;

namespace WebApplication.Web.Models
{
    public class GenericSearch<T>
    {
        public string SearchStr { get; set; } = "";
        public List<T> Object { get; set; } = new List<T>();
    }
}
