﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Concrete
{//çoka çok ilişkiyi temsil edecek
    public class UserRole:IdentityUserRole<int>
    {
    }
}
