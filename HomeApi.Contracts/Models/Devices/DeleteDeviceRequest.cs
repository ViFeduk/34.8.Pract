using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Models.Devices
{
    public class DeleteDeviceRequest
    {
        [Required]
        public string NameDel { get; set; }
       
    }
}
