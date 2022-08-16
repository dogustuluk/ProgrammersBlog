﻿using ProgrammersBlog.Shared.Entities.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class ArticleListDto:DtoGetBase
    {
        public IList<ArticleAddDto> Articles { get; set; }
    }
}
