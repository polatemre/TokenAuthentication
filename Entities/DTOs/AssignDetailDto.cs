using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class AssignDetailDto : IDto
    {
        public int AssignId { get; set; }
        public string BookName { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsReturn { get; set; }
        public DateTime ReturnDateTime { get; set; }
    }
}
