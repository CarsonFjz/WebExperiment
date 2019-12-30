using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebTest.Model
{
    public class Test
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
