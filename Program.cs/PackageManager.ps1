using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_DbFirst_Template
{
    class PackageManager
    {
        Scaffold-DbContext -Connection "Data Source=LAPTOP-F8VLDA40;Initial Catalog=BookStoreForLabb2;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False" -Provider "Microsoft.EntityFrameworkCore.SqlServer" -outputDir "Entities"

    }
}
