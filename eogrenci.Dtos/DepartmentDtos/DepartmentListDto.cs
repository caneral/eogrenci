using System;
using Core.Entities;

namespace eogrenci.Dtos.DepartmentDtos
{
    public class DepartmentListDto : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
