using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Assign : IEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int StudentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsReturn { get; set; }
        public DateTime ReturnDateTime { get; set; }
    }
}
